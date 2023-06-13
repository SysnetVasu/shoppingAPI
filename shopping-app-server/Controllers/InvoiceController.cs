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
        public async Task<ActionResult> CreatInvoice(Int64 OrderNo)
        {

            try
            {
                double TaxPer = 0;
                var company = _context.Companies.SingleOrDefault();
                if (company != null)
                {
                    TaxPer = await _context.Tax.Where(y => y.Id == company.TaxPerId).Select(x => x.TaxPer).SingleOrDefaultAsync();
                }
                var orders = await _context.Orders.Where(x => x.Id == OrderNo)
               .Include(x => x.Customer)
               .Include(o => o.OrderDetails).ThenInclude(y => y.Unit)
               //.OrderByDescending(x => x.CreatedDate)
               .SingleOrDefaultAsync();
                if (orders == null) return null;

                var result = _mapper.Map<Entities.Order, OrderToReturnDto>(orders);

                //  Guid orderId = Guid.NewGuid();
                //  var data = await _context.Orders.FindAsync(invoiceDto.OrderNo);

                //  var cart = data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<UserCart>(data);
                //var SalesHeaderId = "IN-" + GetNextNumber();
                //var SalesHeaderId = GetNextNumber();
                var vouchersetting = await _context.VoucherSettings.Where(x => x.VoucherName == "SALESINVOICE").SingleOrDefaultAsync();
                var SalesHeaderId = vouchersetting.VoucherPreFix + DateTime.Now.ToString("yyyyMM")
                            + String.Format("{0, 0:D5}", vouchersetting.VoucherNextNumber);

                var items = new List<SalesDetail>();
                foreach (var item in orders.OrderDetails)
                {

                    //  orderId = Guid.NewGuid();
                    var productItem = await _context.Products.FindAsync(item.ProductId);
                    double itemTotal = (double)(item.Quantity * item.Price);
                    itemTotal = ((double)itemTotal - (double)item.Discount);
                    double itemTaxAmt = (TaxPer * itemTotal)/100;
                    double itemNetTotal = (itemTotal) + itemTaxAmt;
                    var salesItem = new SalesDetail(productItem.Id, productItem.Name, productItem.ThumbnailUrl, 
                        (double)item.Price, item.Quantity, item.UnitId,item.Unit.Name,
                        itemTotal, (double)item.Discount, itemTaxAmt, itemNetTotal);

                    //orderItem.Id = orderId.ToString();
                    // salesItem.SalesHeaderId = SalesHeaderId;
                    //salesItem.UnitId = productItem.UnitId;
                   // salesItem.Total = Convert.ToDouble(item.Price * item.Quantity) - salesItem.Discount;
                   if (item.Description==null)
                    {
                        salesItem.Description = "";
                    }
                    else
                    {
                        salesItem.Description = item.Description;
                    }                       
                    salesItem.CreatedBy = "admin";
                    salesItem.CreatedDate = DateTime.Today;
                    salesItem.UpdatedDate = DateTime.Today;
                    salesItem.UpdatedBy = "admin";
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
                invoice.InvoiceNo = SalesHeaderId.ToString();
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

                vouchersetting.VoucherNextNumber = vouchersetting.VoucherNextNumber + 1;
                _context.Update(vouchersetting);
                var updateNumber = await _context.SaveChangesAsync();

                return Ok(invoice.Id);


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

        public async Task<IActionResult> PrintInvoice(long InvoiceNo)
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
                 .Where(x => x.Id == InvoiceNo)
                 .SingleOrDefaultAsync();
                if (sale == null) return null;
                SalesHeader salesheader = new SalesHeader();

                var client = await _context.Customers.SingleOrDefaultAsync(x => x.Id == sale.CustomerId);

                //invoice.Number = sale.InvoiceNo;
                //invoice.Date = sale.Date;
                //invoice.Recipient.Name = sale.customer.Name;
                //invoice.Recipient.Email = sale.customer.Email;
                //invoice.Recipient.Address.AddressLine1 = sale.customer.Address;

                // Invoice invoice = Invoice();
                var model = sale.MapToModel();
                
                PrintProcess.PrintingProcess(model, company, client, tax);
              
                string folderPath = "Content\\Invoice\\";
                string fileName = model.InvoiceNo;
                string filePath = Path.Combine(folderPath, $"{fileName}.pdf");

                if (Directory.Exists(folderPath))
                {
                    // Read the file bytes
                    byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                    // Set the content type for the response
                    string contentType = "application/pdf";

                    return File(fileBytes, contentType, $"{fileName}.pdf");
                }
                return NotFound("The specified PDF document was not found.");
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
            try
            {
                var vouchersetting =  _context.VoucherSettings.Where(x => x.VoucherName == "SALESINVOICE").SingleOrDefault();
                var InvoiceNo = vouchersetting.VoucherPreFix + DateTime.Now.ToString("yyyyMM")
                            + String.Format("{0, 0:D5}", vouchersetting.VoucherNextNumber);
                // long n = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
                return InvoiceNo;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
           
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
        public async Task DeleteOrder(Int64 id)
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
