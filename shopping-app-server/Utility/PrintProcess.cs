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
        public static void PrintingProcess(SalesHeaderViewModel invoice, Company companies, Customer client, Tax tax)
        {

            if (client != null)
            {
                if (client.Address == null) client.Address = "";
                if (client.Email == null) client.Email = "";
                if (client.PhoneNumber == null) client.PhoneNumber = "";
                if (client.TaxNumber == null) client.TaxNumber = "";
            }
            if (companies != null)
            {
                if (companies.Address1 == null) companies.Address1 = "";
                if (companies.Address2 == null) companies.Address2 = "";
                if (companies.Address3 == null) companies.Address3 = "";
                if (companies.Country == null) companies.Country = "";
                if (companies.Email == null)
                    companies.Email = "";
                else
                    companies.Email = "EMAIL: " + companies.Email ;

                if (companies.Phone == null)
                    companies.Phone = "";
                else
                    companies.Phone = "PH: " + companies.Phone;

                if (companies.Country == null) companies.Country = "";
            }
            var itemRowlist = new List<ItemRow>();
            List<SalesDetailsViewModel> itemInvoicedetails = new List<SalesDetailsViewModel>();
            itemInvoicedetails = invoice.salesdetailsVM.ToList();
            itemRowlist = itemInvoicedetails.Select(x => new ItemRow
            {

                Name = x.ProductName,
                Description = "",
                Qty = (decimal)x.Quantity,
                Uom = x.UnitName,
                Price = (decimal)x.UnitPrice,
                Total = (decimal)x.Total,
                Discount = "0",
                Tax = (decimal)x.Tax,

            }).ToList();
            try
            {
                new InvoicerApi(SizeOption.A4, OrientationOption.Portrait, "$")
                              .Reference(invoice.InvoiceNo)
                              .BillingDate(invoice.Date)
                              .TextColor("#039956")
                              //.BackColor("#FFD6CC")
                              .Image1(@".\Content\images\logo1.jpeg", 150, 50)
                              .Image2(@".\Content\images\logo2.png", 150, 50)
                              .Image3(@".\Content\images\logo2.png", 150, 50)
                              .Company(Address.Make(companies.Name, new string[] { "", companies.Address1, companies.Address2, companies.Address3, "", companies.Phone, companies.Email }))
                              .Client(Address.Make("BILLING TO", new string[] { client.Name, client.Address, client.Email, client.PhoneNumber, "" }))
                              //     .ClientDelivery(Address.Make("DELIVERY TO", new string[] { "Isabella Marsh KDHGLDAJGL LJDJGLDJGFDS", "Overton Circle", "Little Welland", "Worcester", "WR## 2DJ" }))
                              .Items(itemRowlist)

                              // .Items(new List<ItemRow> {

                              //    ItemRow.Make("Nexus 6", "Midnight Blue,for testing", (decimal)1, 20, (decimal)166.66, (decimal)199.99),
                              //})
                              .Totals(new List<TotalRow> {
                                    TotalRow.Make("",-1),                          
                                TotalRow.Make("Sub Total",(decimal)invoice.GrandTotal),
                                TotalRow.Make(tax.Name, (decimal)invoice.TotalTax),
                                TotalRow.Make("NetTotal", (decimal)invoice.NetTotal),
                              })
                              .FooterText(new List<FooterRow>
                              {
                                FooterRow.Make("Received and Accepted In Good Condition"),
                                FooterRow.Make(" "),
                                FooterRow.Make(" "),
                                FooterRow.Make(" "),
                                FooterRow.Make(" --------------------- "),
                                FooterRow.Make("Customer Signature"),
                                   FooterRow.Make(" "),
                                      FooterRow.Make(" "),
                            
                            })
                               .Details(new List<DetailRow> {
                         DetailRow.Make("PAYMENT INFORMATION", "Make all cheques payable to " + companies.Name, "", "If you have any questions concerning this invoice, contact our sales department at " + companies.Email, "", "Thank you for your business.")
                               })
                              .Footer("")
                              .Save("Content\\Invoice\\" + invoice.InvoiceNo);
            }
            catch (System.Exception ex)
            {

                ex.ToString();
            }

        }
    }
}
