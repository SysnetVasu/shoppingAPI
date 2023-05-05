using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace API.Model
{
    public class Invoice
    {
        /// <summary>
        /// The invoice number
        /// </summary>
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// The invoice date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The date the invoice is due to be paid.
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Sender of the invoice.
        /// </summary>
        public Sender Sender { get; set; } = new Sender();

        /// <summary>
        /// Recipient of the invoice.
        /// </summary>
        public Recipient Recipient { get; set; } = new Recipient();

        /// <summary>
        /// A custom message to be displayed on the invoice.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Remarks next to the payment details.
        /// </summary>
        public string PaymentRemarks { get; set; } = string.Empty;

        /// <summary>
        /// The format to display currencies and dates. For example: en-US, nl-NL.
        /// </summary>
        public string DisplayCulture { get; set; } = "en-US";

        /// <summary>
        /// A list of invoice items.
        /// </summary>
        public List<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();

        public PaymentDetails PaymentDetails { get; set; } = new PaymentDetails();
    }
}
