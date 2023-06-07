using API.Entities.Orders;
using System;
using System.Collections.Generic;


namespace API.Entities
{
    public class Order 
    {
        public Order()
        {
        }

        public Order(IReadOnlyList<OrderDetail> orderItems, decimal subtotal,decimal taxamount,decimal nettotal)
        {
            
            OrderDetails = orderItems;
            // Subtotal = subtotal;
            TotalPrice = subtotal;
            TaxAmount = taxamount;
            NetTotal = nettotal;
        }
        public Int64 Id { get; set; }
        public string OrderNo { get; set; }
        public long OrderStatusId { get; set; }
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal RefundedAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetTotal { get; set; }
        public int TaxType { get; set; }
        public string Notes { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Boolean IsDeleted { get; set; }
        public Customer Customer { get; set; }
        public virtual IReadOnlyList<OrderDetail> OrderDetails { get; set; }

       
    }
}
