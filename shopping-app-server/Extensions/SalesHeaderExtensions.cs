using API.Model;
using System.Linq;
using API.ViewModel;
using API.Entities;

namespace Api.Extensions
{

    public static class SalesHeaderExtensions
    {
        public static SalesHeaderViewModel MapToModel(this SalesHeader source) => 
            new SalesHeaderViewModel(
                source.Id,
                source.CustomerId,
                source.InvoiceNo,
                source.OrderNo,
                source.Date,
                source.Discount,
                source.TotalDiscount,
                source.TaxId,
                source.TotalTax,                   
                //source.Sender.MapToModel(),
                //source.Recipient.MapToModel(),
                //source.Message,
                //source.PaymentRemarks,
                //source.DisplayCulture,
                source.SalesDetails.Select(x => x.MapToModel()).ToList()
               );

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

        public static SalesDetailsViewModel MapToModel(this SalesDetail source) =>
            new SalesDetailsViewModel(
                 source.ProductId,
                 source.ProductName,                
                 source.UnitPrice,
                 source.Quantity,
                 source.UnitId,
                 source.Total,
                 source.Discount,
                 source.Tax,
                 source.NetTotal
                 );

        public static PaymentDetailsViewModel MapToModel(this PaymentDetails source) =>
            new PaymentDetailsViewModel(
                source.Recipient,
                source.BankAccountNumber);
    }
}
