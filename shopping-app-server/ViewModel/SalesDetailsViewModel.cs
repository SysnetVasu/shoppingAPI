using System;
using System.Collections.Generic;
using System.Text;


namespace API.ViewModel
{
    public class SalesDetailsViewModel
    {
        public SalesDetailsViewModel(string productItemId, string productName, double price, double quantity, string unitId,
          double TotalAmount, double DiscountAmount, double taxAmount, double NetTotalAmount)
        {
            ProductId = productItemId;
            ProductName = productName;
            UnitPrice = price;
            Quantity = quantity;
            UnitId = UnitId;
            Total = TotalAmount;
            Discount = DiscountAmount;
            Tax = taxAmount;
            NetTotal = NetTotalAmount;
            PurchasePrice = 0;

            //Id
            //ProductId
            //Quantity
            //PurchasePrice
            //UnitPrice
            //Discount
            //Total
            //SalesHeaderId
            //IsDeleted
        }

        public long Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public double Quantity { get; set; }
        public double PurchasePrice { get; set; }
        public double UnitPrice { get; set; }
        public double Discount { get; set; }
        public double Total { get; set; }
        public double Tax { get; set; }
        public double NetTotal { get; set; }
        public long SalesHeaderId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int IsDeleted { get; set; }
    
        //public decimal TotalExcludingTax => Quantity * UnitPrice;

        //public decimal TotalIncludingTax => TotalExcludingTax + TotalTaxAmount;

        //private decimal TotalTaxAmount => TotalExcludingTax / 100 * TaxPercentage;
    }
}
