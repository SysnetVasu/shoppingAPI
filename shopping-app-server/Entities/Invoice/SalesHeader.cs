using API.Entities.Orders;
using System;
using System.Collections.Generic;


namespace API.Entities
{
    public class SalesHeader 
    {
        public SalesHeader()
        {
        }
        public SalesHeader(IReadOnlyList<SalesDetail> invoiceItems,
            double dTotalAmout, double dTotalDiscount,
            double dTotalTaxAmount, double dNetTotal)
        { 

            SalesDetails = invoiceItems;
            TotalDiscount = dTotalDiscount;
            TotalTax = dTotalTaxAmount;
            GrandTotal = dTotalAmout;
            NetTotal = dNetTotal;
            // Subtotal = subtotal;
            // TotalPrice = subtotal;
        }
        public long Id { get; set; }
        public string CustomerId { get; set; }
        public string InvoiceNo { get; set; }
        public string OrderNo { get; set; }
        public DateTime Date { get; set; }
        public double Discount { get; set; }
        public double TotalDiscount { get; set; }
        public string TaxId { get; set; }
        public double TotalTax { get; set; }
        public double ShippingCost { get; set; }
        public double GrandTotal { get; set; }
        public double NetTotal { get; set; }
        public double PaidAmount { get; set; }
        public double Due { get; set; }
        public double Change { get; set; }
        public string Details { get; set; }
        public string PaymentAccount { get; set; }
        public string VoucherNo { get; set; }
        public string SalesManId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int IsDeleted { get; set; }

        public Customer customer { get; set; }
        public virtual IReadOnlyList<SalesDetail> SalesDetails { get; set; }

       
    }
}
