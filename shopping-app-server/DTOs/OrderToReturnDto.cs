using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class OrderToReturnDto
    {
        public string Id { get; set; }

        //public DateTimeOffset OrderDate { get; set; }

        //public IReadOnlyList<OrderDetailDto> OrderDetails { get; set; }
        //public double Subtotal { get; set; }
        //public double Total { get; set; }
        public string OrderNo { get; set; }
        public long OrderStatusId { get; set; }
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal RefundedAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public string Notes { get; set; }
        public CustomerDto Customer { get; set; }
        public ICollection<OrderDetailDto> OrderDetails { get; set; }
    }
}
