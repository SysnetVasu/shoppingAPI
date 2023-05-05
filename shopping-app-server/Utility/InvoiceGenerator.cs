using API.ViewModel;
using System.Text;

namespace API.Utility
{

    public static class InvoiceGenerator
    {
        public static string GetHTMLString(SalesHeaderViewModel invoice)
        {
            var employees = DataStorage.GetAllEmployees();

            var sb = new StringBuilder();
            sb.Append(@"
            <div class='row'>
                <div class='col-xs-12'>
                    <h2 class='page-header'>
                        Invoice              
                    </h2>
                </div>
               </div>
                <div class='row'>
                        <div class='col-xs-12 text-center'>
                            <h4> Amman House Hold pte ltd </h4>
                        </div>
                  </div>");

            sb.Append(@"
                        <html>
                            <head> InvoiceNo: 
                                     {invoice.Number}");
            sb.Append(@"
  </head>
  <body>
  <div class='invoice-box'>
          <table cellpadding='0' cellspacing='0'>
            <tr class='top'>
                <td>
                    <table>
                        <tr>
                            <td class='title'>
                                <!-- <img src = '/images/logo.png' style='width:100%; max-width:300px;'> -->
                            </td>

                            <td>
                                Invoice #: {{Number}}<br>
                                Created: {{FormatDate Date}}<br>
                                Due: {{FormatDate DueDate}}
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr class='information'>
                <td>
                    <table>
                        <tr>
                            <td>
                                {{#Recipient}}
                                {{ Name}} <br/>
                                {{ Email}}<br/>
                                {{#Address}}
                                {{ AddressLine1}}<br/>
                                {{ AddressLine2}}<br/>
                                {{ PostalCode}}
                {{ City}}<br/>
                                {{Country}}<br/>
                                {{/Address}}
                {{/Recipient}}
                            </td>
                            <td>
                                {{#Sender}}
                                {{ Name}} <br/>
                                {{#Address}}
                                {{ AddressLine1}}<br/>
                                {{ AddressLine2}}<br/>
                                {{ PostalCode}}
                                {{ City}}<br/>
                                {{ Country}}<br/>
                                {{/ Address }
                                {{ Website}}<br/>
                                {{ Email}}<br/>
                                {{/Sender}}
                            </td>
                        </tr>
                        <tr>
                            <td colspan ='2'>{{Message}}</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>");
 sb.Append(@"
                     <table>
                        <tr class= 'heading'>
                            <td> Description</td>
                            <td> Quantity</td>
                            <td> Tax</td>
                            <td> Price</td>
                        </tr>

                        {
                {#Items}}

                        <tr class='item'>
                            <td>
                                {{Description}}
                            </td>
                            <td>{{Quantity}}</td>
                            <td>{{TaxPercentage}} %</td>
                            <td>{{FormatCurrency TotalIncludingTax}}</td>
                        </t>

                        {{/Items}}

                        <tr class='total'>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>
                                Total:{{FormatCurrency TotalWithTaxes}}
                            </td>
                        </tr>
                    </table>");
 sb.Append(@"
             <p>{{PaymentRemarks}}</p>
                    <p> {{#PaymentDetails}}
                        {{ Recipient}}
                    {{BankAccountNumber}}
                    {{/PaymentDetails}}
                    </p>
                </div>
            </body>          
             </html>
            ");




                    sb.Append(@" </h1></div>
            <table align='center'>
            <tr class= 'heading'>
                <td> Description </td >
                <td> Quantity </td >
                <td> Tax </td >
                <td> Price </td >
                <td> Total </td >
            </tr>");

            foreach (var item in invoice.salesdetailsVM)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                     <td>{4}</td>
                                  </tr>", item.Description, item.Quantity, item.UnitPrice, item.Tax, item.Quantity * item.UnitPrice);
            }


            sb.Append(@"
<tr class= 'total'>
                <td></td >
                <td></td >
                <td ></td >
                <td>
                    Total: " + invoice.TotalTax);
 //           sb.Append(@"
 //               </td>
 //                </tr>
 //                   </table>
 //</div>
 //                   </body>
 //                       </html>");



            //            sb.Append(@"
            //<!doctype html>
            // <html>
            //<head>
            //    <title> Invoice No:" + invoice.Number);

            //            sb.Append(@"</title>    
            //</head>");

            //            sb.Append(@"
            //<body>
            //    <div class='invoice-box'>
            //        < table cellpadding = '0'cellspacing = '0' >
            //            < tr class='top'>
            //                <td>
            //                    <table>
            //                        <tr>
            //                            <td class='title'>
            //                                <!-- <img src = '/images/logo.png' style='width:100%; max-width:300px;'> -->
            //                            </td>

            //                            <td>
            //                                Invoice #:invoice.Number  <br>
            //                                Created: invoice.date <br>

            //                            </ td >
            //                        </ tr >
            //                    </ table >
            //                </ td >
            //            </ tr >

            //            < tr class= 'information' >
            //                < td >
            //                    < table >
            //                        < tr >
            //                            < td >
            //                                {
            //    {#Recipient}}
            //                                { { Name} } < br />
            //                                { { Email} }< br />
            //                                {
            //            {#Address}}
            //                                { { AddressLine1} }< br />
            //                                { { AddressLine2} }< br />
            //                                { { PostalCode} }
            //                { { City} }< br />
            //                                { { Country} }< br />
            //                                { {/ Address} }
            //                { {/ Recipient} }
            //                            </ td >

            //                            < td >
            //                                {
            //                    {#Sender}}
            //                                { { Name} } < br />
            //                                {
            //                            {#Address}}
            //                                { { AddressLine1} }< br />
            //                                { { AddressLine2} }< br />
            //                                { { PostalCode} }
            //                                { { City} }< br />
            //                                { { Country} }< br />
            //                                { {/ Address} }
            //                                { { Website} }< br />
            //                                { { Email} }< br />
            //                                { {/ Sender} }
            //                            </ td >
            //                        </ tr >
            //                        < tr >
            //                            < td colspan = '2' >{ { Message} }</ td >
            //                        </ tr >
            //                    </ table >
            //                </ td >
            //            </ tr >
            //        </ table >
            //        < table >
            //            < tr class= 'heading' >
            //                < td > Description </ td >
            //                < td > Quantity </ td >
            //                < td > Tax </ td >
            //                < td > Price </ td >
            //            </ tr >

            //            {
            //    {#Items}}

            //            < tr class= 'item' >
            //                < td >
            //                    { { Description} }
            //                </ td >
            //                < td >{ { Quantity} }</ td >
            //                < td >{ { TaxPercentage} } %</ td >
            //                < td >{ { FormatCurrency TotalIncludingTax} }</ td >
            //            </ tr >

            //            { {/ Items} }

            //            < tr class= 'total' >
            //                < td ></ td >
            //                < td ></ td >
            //                < td ></ td >
            //                < td >
            //                    Total:{ { FormatCurrency TotalWithTaxes} }
            //                </ td >
            //            </ tr >
            //        </ table >
            //        < p >{ { PaymentRemarks} }</ p >
            //        < p >
            //            {
            //    {#PaymentDetails}}
            //            { { Recipient} }
            //        { { BankAccountNumber} }
            //        { {/ PaymentDetails} }
            //        </ p >
            //    </ div >
            //</ body >
            //sb.Append(@"
            //</ html >
            //");

            //foreach (var emp in employees)
            //{
            //    sb.AppendFormat(@"<tr>
            //                            <td>{0}</td>
            //                            <td>{1}</td>
            //                            <td>{2}</td>
            //                            <td>{3}</td>
            //                          </tr>", emp.Name, emp.LastName, emp.Age, emp.Gender);
            //}
            //sb.Append(@"
            //                        </table>
            //                    </body>
            //                </html>");

            return sb.ToString();
        }
    }
}

