using API.Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace API.ViewModel
{
    public class SalesHeaderViewModel
    {
        
        public SalesHeaderViewModel(long id,
                       string  customerid,
                       string invoiceno,
                       string orderno,
                       DateTime date,
                       double discount,
                       double totaldiscount,
                       string taxid,
                       double totaltax,
                       //SenderViewModel sender,
                       //RecipientViewModel recipient,
                       //string message,
                       //string paymentRemarks,
                       //string displayCulture,
                       IReadOnlyList<SalesDetailsViewModel> salesdetails)
                       //PaymentDetailsViewModel paymentDetails)
        {
           Id = id;
            CustomerId = customerid;
            InvoiceNo = invoiceno;
            OrderNo = orderno;
            Date = date;
            Discount = discount;
            TotalDiscount = totaldiscount;
            TaxId = taxid;
            TotalTax = totaltax;
            //Sender = sender;
            //Recipient = recipient;
            //Message = message;
            //PaymentRemarks = paymentRemarks;
            //DisplayCulture = displayCulture;
            salesdetailsVM = salesdetails;
            //PaymentDetails = paymentDetails;
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


        /// <summary>
        /// The invoice number
        /// </summary>
        public string Number { get; }

        /// <summary>
      

        /// <summary>
        /// The date the invoice is due to be paid.
        /// </summary>
        public DateTime? DueDate { get; }

        /// <summary>
        /// Sender of the invoice.
        /// </summary>
        public SenderViewModel Sender { get; }

        /// <summary>
        /// Recipient of the invoice.
        /// </summary>
        public RecipientViewModel Recipient { get; }

        /// <summary>
        /// A custom message to be displayed on the invoice.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Remarks next to the payment details.
        /// </summary>
        public string PaymentRemarks { get; }

        /// <summary>
        /// The format to display currencies and dates. For example: en-US, nl-NL.
        /// </summary>
        public string DisplayCulture { get; }

        /// <summary>
        /// A list of invoice items.
        /// </summary>
        public IReadOnlyList<SalesDetailsViewModel> salesdetailsVM { get; }

        public PaymentDetailsViewModel PaymentDetails { get; }

        //public decimal TotalWithoutTaxes => Items.Sum(m  => m.TotalExcludingTax);

        //public decimal TotalWithTaxes => Items.Sum(m => m.TotalIncludingTax);

        //public IReadOnlyList<(decimal, decimal)> TaxPrices => Items.GroupBy(x => x.TaxPercentage).Select(x => (x.Key, x.Sum(y => y.TotalIncludingTax))).ToList();
    }
}
