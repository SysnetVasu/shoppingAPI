using API.Entities.Orders;
using System;
using System.Collections.Generic;


namespace API.Entities
{
    public class InvoiceDto 
    {      
        public long Id { get; set; }
        public string CustomerId { get; set; }
        public string InvoiceNo { get; set; }
        public string OrderNo { get; set; }
        public DateTime Date { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalDiscount { get; set; }
        public string TaxId { get; set; }
        public decimal TotalTax { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal NetTotal { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal Due { get; set; }
        public decimal Change { get; set; }
        public string Details { get; set; }
        public string PaymentAccount { get; set; }
        public string VoucherNo { get; set; }
        public string SalesManId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Boolean IsDeleted { get; set; }

        public Customer customer { get; set; }
        public virtual IReadOnlyList<SalesDetail> SalesDetails { get; set; }

       
    }
}
