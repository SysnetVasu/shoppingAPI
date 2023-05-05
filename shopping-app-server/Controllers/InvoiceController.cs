using Api.Extensions;
using API.Abstractions;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Entities.Orders;
using API.Model;
using API.Utility;
using API.ViewModel;
using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace API.Controllers
{

    //[ApiController]
    //[Route("[controller]")]
    //[EnableCors("AllowOrigin")]   
    public class InvoiceController : BaseApiController
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        private readonly IDatabase _database;
        private readonly ITemplateProvider _templateProvider;
        private IConverter _converter;
        private int value = 0;
        public string Prefix;

        public InvoiceController(StoreContext context, IMapper mapper, IConnectionMultiplexer redis,
            ITemplateProvider templateProvider, IConverter converter)
        {
            _database = redis.GetDatabase();
            _context = context;
            _mapper = mapper;
            _templateProvider = templateProvider;
            _converter = converter;
        }

        [HttpGet("CreateInvoce")]
        public async Task<ActionResult> CreatInvoice(string OrderNo)
        {

            try
            {
                double TaxPer = 0;
                var company = _context.Companies.SingleOrDefault();
                if (company != null)
                {
                    TaxPer = _context.Tax.Where(y => y.Id == company.TaxPerId).Select(x => x.TaxPer).SingleOrDefault();
                }
                var orders = await _context.Orders.Where(x => x.Id == OrderNo)
               .Include(x => x.Customer)
               .Include(o => o.OrderDetails).ThenInclude(y => y.Unit)
               .OrderByDescending(x => x.CreatedDate)
               .SingleOrDefaultAsync();
                if (orders == null) return null;

                var result = _mapper.Map<Entities.Order, OrderToReturnDto>(orders);

                //  Guid orderId = Guid.NewGuid();
                //  var data = await _context.Orders.FindAsync(invoiceDto.OrderNo);

                //  var cart = data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<UserCart>(data);
                var SalesHeaderId = "IN-" + GetNextNumber();

                var items = new List<SalesDetail>();
                foreach (var item in result.OrderDetails)
                {

                    //  orderId = Guid.NewGuid();
                    var productItem = await _context.Products.FindAsync(item.ProductId);
                    double itemTaxAmt = ((double)TaxPer * item.Total);
                    double itemNetTotal = (item.Total - item.Discount) + itemTaxAmt;
                    var salesItem = new SalesDetail(productItem.Id, productItem.Name, productItem.ThumbnailUrl, item.Price, item.Quantity, productItem.UnitId,
                        item.Total, item.Discount, itemTaxAmt, itemNetTotal);

                    //orderItem.Id = orderId.ToString();
                    // salesItem.SalesHeaderId = SalesHeaderId;
                    salesItem.UnitId = productItem.UnitId;
                    salesItem.Total = Convert.ToDouble(item.Price * item.Quantity) - salesItem.Discount;
                    salesItem.Description = productItem.Description;
                    salesItem.CreatedDate = DateTime.Today;
                    salesItem.UpdatedDate = DateTime.Today;
                    items.Add(salesItem);
                }

                // calc subtotal
                double subtotal = items.Sum(item => item.UnitPrice * item.Quantity);
                var TotalTax = items.Sum(item => item.Tax);
                var TotalNetTotal = items.Sum(item => item.NetTotal);
                var TotalDiscount = items.Sum(item => item.Discount);

                // create order
                var invoice = new Entities.SalesHeader(items, subtotal, TotalDiscount, TotalTax, TotalNetTotal);

                // save to db
                this.Prefix = "ORD";
                invoice.InvoiceNo = SalesHeaderId;
                invoice.TaxId = company.TaxPerId;
                invoice.OrderNo = orders.OrderNo;
                invoice.VoucherNo = orders.OrderNo;
                //orderId = Guid.NewGuid();
                //  order.Id = orderId.ToString();
                invoice.CustomerId = orders.CustomerId;
                invoice.Date = DateTime.Today;
                invoice.CreatedDate = DateTime.Now;
                invoice.UpdatedDate = DateTime.Now;

                _context.SalesHeader.Add(invoice);

                var saveInvoice = await _context.SaveChangesAsync();
                orders.OrderStatusId = 1;
                _context.Update(orders);
                var statusUpdate = await _context.SaveChangesAsync();
                return Ok(saveInvoice);


            }
            catch (Exception ex)
            {
                ex.ToString();
                return (null);
            }

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        //[Produces("application/octet-stream", Type = typeof(FileResult))]
        [HttpGet("PrintInvoice/{InvoiceNo}")]

        public async Task<IActionResult> PrintInvoice(string InvoiceNo)
        //  public async Task<IActionResult> PrintInvoice(string templateKey, string InvoiceNo)
        {
            try
            {
                string templateKey = "Invoice";

                var company = await _context.Companies.SingleOrDefaultAsync();
                var tax = await _context.Tax.Where(x => x.Id == company.TaxPerId).SingleOrDefaultAsync();


                var sale = await _context.SalesHeader
                 .Include(x => x.customer)
                 .Include(x => x.SalesDetails).ThenInclude(y => y.Product).ThenInclude(z => z.Unit)
                 .Where(x => x.InvoiceNo == InvoiceNo && x.IsDeleted == 0)
                 .SingleOrDefaultAsync();
                if (sale == null) return null;
                SalesHeader salesheader = new SalesHeader();

                var client = _context.Customers.SingleOrDefault(x => x.Id == sale.CustomerId);

                //invoice.Number = sale.InvoiceNo;
                //invoice.Date = sale.Date;
                //invoice.Recipient.Name = sale.customer.Name;
                //invoice.Recipient.Email = sale.customer.Email;
                //invoice.Recipient.Address.AddressLine1 = sale.customer.Address;

                // Invoice invoice = Invoice();
                var model = sale.MapToModel();
                PrintProcess.PrintingProcess(model, company, client, tax);


                //var globalSettings = new GlobalSettings
                //{
                //    ColorMode = ColorMode.Color,
                //    Orientation = Orientation.Portrait,
                //    PaperSize = PaperKind.A4,
                //    Margins = new MarginSettings { Top = 10 },
                //    DocumentTitle = "PDF Report",
                //    Out = @"D:\temp\PDFCreator\Invoice_" + sale.InvoiceNo.ToString() + ".pdf"
                //};
                //var objectSettings = new ObjectSettings
                //{
                //    PagesCount = true,
                //    // HtmlContent = html,
                //    HtmlContent = InvoiceGenerator.GetHTMLString(model),
                //    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                //    HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                //    FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
                //};
                //var pdf = new HtmlToPdfDocument()
                //{
                //    GlobalSettings = globalSettings,
                //    Objects = { objectSettings }
                //};

                //var file = _converter.Convert(pdf);
                return File("", "application/pdf", "Invoice.pdf");
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return File("", "application/pdf", "Invoice.pdf");

        }

        private long GetNextValue()
        {
            long n = long.Parse(DateTime.Today.ToString("yyyyMMddHHmmss"));
            return n;
            //return System.Threading.Interlocked.Increment(ref this.value);
        }

        private string GetNextNumber()
        {
            long n = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
            return n.ToString();
            //return $"{this.Prefix}{this.GetNextValue():000000}";
        }

        [HttpGet()]
        public async Task<ActionResult<IReadOnlyList<InvoiceDto>>> GetInvoiceList()
        {
            var sales = await _context.SalesHeader.Where(x => x.IsDeleted == 0)
                .Include(x => x.customer)
                .Include(o => o.SalesDetails).ThenInclude(x => x.Product).ThenInclude(y => y.Unit)
                .ToListAsync();
            var result = _mapper.Map<IReadOnlyList<Entities.SalesHeader>, IReadOnlyList<InvoiceDto>>(sales);
            return Ok(result);
        }



        //[HttpGet("{id}")]
        //public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(string id)
        //{

        //    var order = await _context.Orders.Where(x => x.IsDeleted == false)
        //      .Include(o => o.OrderDetails)
        //      .FirstOrDefaultAsync(o => o.Id == id);

        //    if (order == null) return NotFound();

        //    return Ok(_mapper.Map<Entities.Orders.Order, OrderToReturnDto>(order));
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceDto>> GetInvoiceById(long id)
        {


            var Sales = await _context.SalesHeader
              .Include(x => x.customer)
              .Include(x => x.SalesDetails).ThenInclude(y => y.Product).ThenInclude(z => z.Unit)
              .Where(x => x.Id == id && x.IsDeleted == 0)
              .SingleOrDefaultAsync();

            if (Sales == null) return NotFound();

            return Ok(_mapper.Map<Entities.SalesHeader, InvoiceDto>(Sales));
        }

        [HttpDelete("{id}")]
        public async Task DeleteOrder(string id)
        {
            //var order = _context.Orders.Include(o => o.OrderDetails).SingleOrDefaultAsync(o => o.Id== id);
            //_context.Orders.Remove(await order);
            //await _context.SaveChangesAsync();
            var order = new Entities.Order();
            order = await _context.Orders.Include(o => o.OrderDetails).SingleOrDefaultAsync(o => o.Id == id);
            order.IsDeleted = true;
            foreach (var item in order.OrderDetails)
            {
                item.IsDeleted = true;
            }
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

        }

    }
}
