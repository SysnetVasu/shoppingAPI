using API.Entities;
using API.ViewModel;
using InvoicePrintFormat.Models;
using InvoicePrintFormat.Services;
using InvoicerNETCore.Services;
using System.Collections.Generic;
using System.Linq;

namespace API.Utility
{
    public static class PrintProcess
    {
        public static void PrintingProcess(SalesHeaderViewModel invoice, Company companies,Customer client,Tax tax)
        {

            var itemRowlist = new List<ItemRow>();
            List<SalesDetailsViewModel> itemInvoicedetails = new List<SalesDetailsViewModel>();
            itemInvoicedetails = invoice.salesdetailsVM.ToList();
            itemRowlist= itemInvoicedetails.Select(x=> new ItemRow
            {
                Name = x.ProductName,
                Description = "",
                Price = (decimal)x.UnitPrice,
                Total = (decimal)x.Total,
                Discount = "",
                Amount = (decimal)x.NetTotal,
                VAT = (decimal)x.Tax
            }).ToList();
            
            new InvoicerApi(SizeOption.A4, OrientationOption.Portrait, "$")
                .Reference(invoice.InvoiceNo)
                .BillingDate(invoice.Date)
                .TextColor("#039956")
                //.BackColor("#FFD6CC")
                .Image1(@".\Content\images\logo1.jpeg", 150, 50)
                .Image2(@".\Content\images\logo2.png", 150, 50)
                .Company(Address.Make(companies.Name, new string[] { "", companies.Address1, companies.Address2, companies.Address3, "" }, companies.Phone, companies.Email))
                .Client(Address.Make("BILLING TO", new string[] { client.Name, client.Address, client.Email, client.PhoneNumber, "" }))
            //     .ClientDelivery(Address.Make("DELIVERY TO", new string[] { "Isabella Marsh KDHGLDAJGL LJDJGLDJGFDS", "Overton Circle", "Little Welland", "Worcester", "WR## 2DJ" }))
                .Items(itemRowlist)
                
                // .Items(new List<ItemRow> {

                //    ItemRow.Make("Nexus 6", "Midnight Blue,for testing", (decimal)1, 20, (decimal)166.66, (decimal)199.99),
                //})
                .Totals(new List<TotalRow> {
                    TotalRow.Make("Sub Total",(decimal)invoice.NetTotal),
                    TotalRow.Make(tax.Name, (decimal)invoice.TotalTax),
                    TotalRow.Make("Total", (decimal)invoice.GrandTotal, true),
                })
                .Details(new List<DetailRow> {
                    DetailRow.Make("PAYMENT INFORMATION", "Make all cheques payable to " + companies.Name, "", "If you have any questions concerning this invoice, contact our sales department at "+ companies.Email, "", "Thank you for your business.")
                })
                .Footer("Thank you")
                .Save("Content\\Invoice\\" + invoice.InvoiceNo);
        }
    }
}
