using API.Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace API.ViewModel
{
    public class InvoiceViewModel
    {
        public InvoiceViewModel(string number,
                       DateTime date,
                       DateTime? dueDate,
                       //SenderViewModel sender,
                       //RecipientViewModel recipient,
                       //string message,
                       //string paymentRemarks,
                       //string displayCulture,
                       IReadOnlyList<InvoiceItemViewModel> items)
            //,
            //           PaymentDetailsViewModel paymentDetails)
        {
            Number = number;
            Date = date;
            DueDate = dueDate;
            //Sender = sender;
            //Recipient = recipient;
            //Message = message;
            //PaymentRemarks = paymentRemarks;
            //DisplayCulture = displayCulture;
            Items = items;
            //PaymentDetails = paymentDetails;
        }

        /// <summary>
        /// The invoice number
        /// </summary>
        public string Number { get; }

        /// <summary>
        /// The invoice date.
        /// </summary>
        public DateTime Date { get; }

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
        public IReadOnlyList<InvoiceItemViewModel> Items { get; }

        public PaymentDetailsViewModel PaymentDetails { get; }

        public decimal TotalWithoutTaxes => Items.Sum(m  => m.TotalExcludingTax);

        public decimal TotalWithTaxes => Items.Sum(m => m.TotalIncludingTax);

        public IReadOnlyList<(decimal, decimal)> TaxPrices => Items.GroupBy(x => x.TaxPercentage).Select(x => (x.Key, x.Sum(y => y.TotalIncludingTax))).ToList();
    }
}
