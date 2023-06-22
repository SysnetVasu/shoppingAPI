using API.Data;
using API.DTOs;
using API.Entities;
using API.Entities.Orders;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


namespace API.Controllers
{
    //[EnableCors("AllowOrigin")]
    public class OrdersController : BaseApiController
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        private readonly IDatabase _database;
        private int value = 0;
        public string Prefix;
        public OrdersController(StoreContext context, IMapper mapper, IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
            _context = context;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<Entities.Order>> CreateOrder(OrderDto orderDto)
        {
            
            try
            {
                //Guid orderId = Guid.NewGuid();
                var data = await _database.StringGetAsync(orderDto.CartId);

                var cart = data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<UserCart>(data);

                decimal TaxPer = 0;
                var company = _context.Companies.SingleOrDefault();
                if (company != null)
                {
                    TaxPer =(decimal) _context.Tax.Where(y => y.Id == company.TaxPerId).Select(x => x.TaxPer).SingleOrDefault();
                }
                int TaxType = 0;
                var customer = _context.Customers.SingleOrDefault(x=>x.CustomerId == cart.CustomerId);
                if (customer != null)
                {
                    TaxType = customer.TaxType;
                }
                var items = new List<OrderDetail>();
                foreach (var item in cart.Items)
                {
                   

                    //orderId = Guid.NewGuid();
                    var productItem = await _context.Products.FindAsync(item.ProductId);
                    if (productItem == null) return null;
                    var orderItem = new OrderDetail(productItem.Id, item.ProductName, productItem.ThumbnailUrl,(decimal) item.Price, item.Quantity, item.UnitId);
                    //  orderItem.OrderId = orderId.ToString();
                    //  orderItem.Id = Guid.NewGuid().ToString();
                   

                    orderItem.UnitId = productItem.UnitId;
                    orderItem.Total = Convert.ToDecimal(item.Price * item.Quantity) - orderItem.Discount ;
                    orderItem.Description = productItem.Description;
                    orderItem.CreatedDate = DateTime.Today;
                    orderItem.UpdatedDate = DateTime.Today;

                    decimal itemTaxAmt = (TaxPer * orderItem.Total)/100;
                    decimal itemNetTotal = (orderItem.Total) + itemTaxAmt;
                    orderItem.TaxAmount = itemTaxAmt;
                    orderItem.NetTotal = itemNetTotal;
                    items.Add(orderItem);
                }


                // calc total
                var subtotal = items.Sum(item => item.Price * item.Quantity);
                var taxamount = items.Sum(item => item.TaxAmount);
                var nettotal = items.Sum(item => item.NetTotal);
                // create order
                var order = new Entities.Order(items, subtotal,taxamount,nettotal);
               

                // save to db
                //this.Prefix = "ORD";
                //order.OrderNo = GetNextNumber();// "ORD-0002";

                var vouchersetting = _context.VoucherSettings.Where(x => x.VoucherName == "SALESORDER").SingleOrDefault();
                order.OrderNo = vouchersetting.VoucherPreFix + DateTime.Now.ToString("yyyyMM")
                            + String.Format("{0, 0:D5}", vouchersetting.VoucherNextNumber);

                order.CustomerId = cart.CustomerId;
                order.OrderDate = DateTime.Today;
                order.CreatedDate = DateTime.Now;
                order.CreatedBy = "Apps";
                order.UpdatedDate = DateTime.Now;
                order.UpdatedBy = "Apps";
                _context.Orders.Add(order);

              var  result = await _context.SaveChangesAsync();

                vouchersetting.VoucherNextNumber = vouchersetting.VoucherNextNumber + 1;
                _context.Update(vouchersetting);
                var updateNextNumber = await _context.SaveChangesAsync();

                return Ok (result);
            }
            catch (Exception ex)
            {
                return (null);
            }
                       
            
        }
        [HttpPost("UpdateOrder")]

        public async Task<ActionResult<Entities.Order>> UpdateOrder(OrderToReturnDto editOrder)
        {

            try
            {
                ////Guid orderId = Guid.NewGuid();
                //var data = await _database.StringGetAsync(orderDto.CartId);

                //var cart = data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<UserCart>(data);
                decimal TaxPer = 0;
                var company = _context.Companies.SingleOrDefault();
                if (company != null)
                {
                    TaxPer = (decimal)_context.Tax.Where(y => y.Id == company.TaxPerId).Select(x => x.TaxPer).SingleOrDefault();
                }

                var order = await _context.Orders
                  .Where(x => x.Id == editOrder.Id)
                  .Include(x=>x.OrderDetails)
                  .SingleOrDefaultAsync();
                //if (order == null) return Ok(false) ;
                var items = new List<OrderDetail>();
                foreach (var item in editOrder.OrderDetails)
                {
                    //orderId = Guid.NewGuid();
                    var productItem = await _context.Products.FindAsync(item.ProductId);
                    if (productItem == null) return null;
                    var orderItem = new OrderDetail(productItem.Id, item.ProductName, productItem.ThumbnailUrl, (decimal)item.Price, item.Quantity, item.UnitId);
                    //  orderItem.OrderId = orderId.ToString();
                    //  orderItem.Id = Guid.NewGuid().ToString();
                    orderItem.UnitId = productItem.UnitId;
                    orderItem.Total = Convert.ToDecimal(item.Price * item.Quantity) - orderItem.Discount;
                    orderItem.Description = productItem.Description;
                    orderItem.CreatedDate = DateTime.Today;
                    orderItem.UpdatedDate = DateTime.Today;

                    decimal itemTaxAmt = (TaxPer * orderItem.Total) / 100;
                    decimal itemNetTotal = (orderItem.Total) + itemTaxAmt;
                    orderItem.TaxAmount = itemTaxAmt;
                    orderItem.NetTotal = itemNetTotal;
                    items.Add(orderItem);
                }

                // calc total
                var subtotal = items.Sum(item => item.Price * item.Quantity);
                var taxamount = items.Sum(item => item.TaxAmount);
                var nettotal = items.Sum(item => item.NetTotal);
                // create order
               //  order = new Entities.Order(items, subtotal, taxamount, nettotal);
                order.OrderDetails = items;
                order.TotalPrice = subtotal;
                order.TaxAmount=taxamount;
                order.NetTotal = nettotal;
                // save to db
                this.Prefix = "ORD";
                //order.OrderNo = GetNextNumber();// "ORD-0002";
                //                                //orderId = Guid.NewGuid();
                //                                //  order.Id = orderId.ToString();
                order.CustomerId = editOrder.CustomerId;
                
                //editOrder.OrderDate = order.OrderDate;

                //  editOrder.UpdatedDate = DateTime.Now;
                //  var order = _mapper.Map<Entities.Order, OrderToReturnDto>(editOrder);

                _context.Orders.Update(order);

                var result = await _context.SaveChangesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {   
                return BadRequest(ex.InnerException);
            }

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

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrdersForUser()
        {
            var orders = await _context.Orders.Where(x => x.IsDeleted == false && x.OrderStatusId ==0)
                .Include(x => x.Customer)
                .Include(o => o.OrderDetails).ThenInclude(y => y.Unit)
                .OrderByDescending(x=>x.CreatedDate)
                .ToListAsync();
            var result = _mapper.Map<IReadOnlyList<Entities.Order>, IReadOnlyList<OrderToReturnDto>>(orders);
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
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(Int64 id)
        {

            var order = await _context.Orders
              .Include(x => x.Customer)
              .Include(x => x.OrderDetails).ThenInclude(y => y.Product).ThenInclude(z => z.Unit)
              .Where(x => x.Id == id && x.IsDeleted == false)
              .SingleOrDefaultAsync();

            if (order == null) return NotFound();

            return Ok(_mapper.Map<Entities.Order, OrderToReturnDto>(order));
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
