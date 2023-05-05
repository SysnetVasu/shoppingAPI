using API.Model;
using System.Linq;
using API.ViewModel;


namespace Api.Extensions
{
    public static class InvoiceExtensions
    {
        public static InvoiceViewModel MapToModel(this Invoice source) =>
            new InvoiceViewModel(
                source.Number,
                source.Date,
                source.DueDate,
                //source.Sender.MapToModel(),
                //source.Recipient.MapToModel(),
                //source.Message,
                //source.PaymentRemarks,
                //source.DisplayCulture,
                source.Items.Select(x => x.MapToModel()).ToList());
                //source.PaymentDetails.MapToModel());

        //public static SenderViewModel MapToModel(this Sender source) => 
        //    new SenderViewModel(
        //        source.Name,
        //        source.VatNumber,
        //        source.RegistrationNumber,
        //        source.Email,
        //        source.Website,
        //        source.Address.MapToModel());

        public static AddressViewModel  MapToModel(this Address source) => 
            new AddressViewModel(
                source.AddressLine1,
                source.AddressLine2,
                source.PostalCode,
                source.City,
                source.Country);

        //public static RecipientViewModel MapToModel(this Recipient source) =>
        //    new RecipientViewModel(
        //        source.Name,
        //        source.Email,
        //        source.Address.MapToModel());

        public static InvoiceItemViewModel MapToModel(this InvoiceItem source) =>
            new InvoiceItemViewModel(
                source.Number,
                source.Description,
                source.Quantity,
                source.UnitPrice,
                source.TaxPercentage);

        public static PaymentDetailsViewModel MapToModel(this PaymentDetails source) =>
            new PaymentDetailsViewModel(
                source.Recipient,
                source.BankAccountNumber);
    }
}
