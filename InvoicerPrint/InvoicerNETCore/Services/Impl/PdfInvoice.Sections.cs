﻿using InvoicePrintFormat.Helpers;
using InvoicePrintFormat.Models;
//using InvoicerNETCore.Models;
using MigraDocCore.DocumentObjectModel;
using MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes;
using MigraDocCore.DocumentObjectModel.Shapes;
using MigraDocCore.DocumentObjectModel.Tables;
using PdfSharpCore.Utils;
using SixLabors.ImageSharp.PixelFormats;

namespace InvoicerNETCore.Services.Impl
{
    public partial class PdfInvoice
    {
        //backup
        //private void HeaderSection()
        //{
        //    HeaderFooter header = Pdf.LastSection.Headers.Primary;

        //    Table table = header.AddTable();
        //    double thirdWidth = Pdf.PageWidth() / 3;

        //    table.AddColumn(ParagraphAlignment.Left, thirdWidth * 2);
        //    table.AddColumn();

        //    Row row = table.AddRow();

        //    if (!string.IsNullOrEmpty(Invoice.Image))
        //    {
        //        if (ImageSource.ImageSourceImpl == null)
        //            ImageSource.ImageSourceImpl = new ImageSharpImageSource<Rgba32>();

        //        Image image = row.Cells[0].AddImage(ImageSource.FromFile(Invoice.Image));
        //        row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

        //        image.Height = Invoice.ImageSize.Height;
        //        image.Width = Invoice.ImageSize.Width;
        //    }

        //    TextFrame frame = row.Cells[1].AddTextFrame();

        //    Table subTable = frame.AddTable();
        //    subTable.AddColumn(thirdWidth / 2);
        //    subTable.AddColumn(thirdWidth / 2);

        //    row = subTable.AddRow();
        //    row.Cells[0].MergeRight = 1;
        //    row.Cells[0].AddParagraph(Invoice.Title, ParagraphAlignment.Right, "H1-20");

        //    row = subTable.AddRow();
        //    row.Cells[0].AddParagraph("REFERENCE:", ParagraphAlignment.Left, "H2-9B-Color");
        //    row.Cells[1].AddParagraph(Invoice.Reference, ParagraphAlignment.Right, "H2-9");
        //    row.Cells[0].AddParagraph("BILLING DATE:", ParagraphAlignment.Left, "H2-9B-Color");
        //    row.Cells[1].AddParagraph(Invoice.BillingDate.ToShortDateString(), ParagraphAlignment.Right, "H2-9");
        //    row.Cells[0].AddParagraph("DUE DATE:", ParagraphAlignment.Left, "H2-9B-Color");
        //    row.Cells[1].AddParagraph(Invoice.DueDate.ToShortDateString(), ParagraphAlignment.Right, "H2-9");
        //}

        private void HeaderSection()
        {
            HeaderFooter header = Pdf.LastSection.Headers.Primary;
            Address companyAddress = Invoice.Company;
            Table table = header.AddTable();
            Section section = Pdf.LastSection;
            double thirdWidth = Pdf.PageWidth() / 3;

            Address leftAddress = Invoice.Client;
            Address rightAddress = Invoice.ClientDelivery;
            table.AddColumn(ParagraphAlignment.Center, 75);// section.Document.PageWidth() / 2 - 42);
            table.AddColumn(ParagraphAlignment.Center, section.Document.PageWidth() -150);      
            table.AddColumn(ParagraphAlignment.Center, section.Document.PageWidth()-75); //section.Document.PageWidth() / 7);
            table.AddColumn(ParagraphAlignment.Left, section.Document.PageWidth() / 15 - 14);

            Row row = table.AddRow();
            row.Style = "H2-10B-Color";
            row.Shading.Color = Colors.White;
            ////company logo -Left
            if (!string.IsNullOrEmpty(Invoice.Image1))
            {
                if (ImageSource.ImageSourceImpl == null)
                    ImageSource.ImageSourceImpl = new ImageSharpImageSource<Rgba32>();

                Image image = row.Cells[0].AddImage(ImageSource.FromFile(Invoice.Image1));
                row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

                image.Height = Invoice.ImageSize.Height;
                image.Width = Invoice.ImageSize.Width;
            }
            
            row.Shading.Color = Colors.White;
            ////company Name
            row.Cells[1].AddParagraph(Invoice.Company.Title, ParagraphAlignment.Center, "H2-18A-Color");
            ////company address1
           if (Invoice.Company.AddressLines[0] != "") row.Cells[1].AddParagraph(Invoice.Company.AddressLines[0],ParagraphAlignment.Center);
            row.Cells[1].AddParagraph(Invoice.Company.AddressLines[1], ParagraphAlignment.Center);
            row.Cells[1].AddParagraph(Invoice.Company.AddressLines[2], ParagraphAlignment.Center);
            row.Cells[1].AddParagraph(Invoice.Company.AddressLines[3], ParagraphAlignment.Center);
            row.Cells[1].AddParagraph(Invoice.Company.AddressLines[4], ParagraphAlignment.Center);
            // AddressCell(row.Cells[1], companyAddress.AddressLines);

            ////company logo -right
            if (!string.IsNullOrEmpty(Invoice.Image2))
            {
                if (ImageSource.ImageSourceImpl == null)
                    ImageSource.ImageSourceImpl = new ImageSharpImageSource<Rgba32>();

                Image image = row.Cells[2].AddImage(ImageSource.FromFile(Invoice.Image2));
                row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

                image.Height = Invoice.ImageSize.Height;
                image.Width = Invoice.ImageSize.Width;
            }           

            //HeaderFooter header = Pdf.LastSection.Headers.Primary;
            //Address companyAddress = Invoice.Company;
            //Table table = header.AddTable();
            //Section section = Pdf.LastSection;
            //double thirdWidth = Pdf.PageWidth() / 3;

      
            //table.AddColumn(ParagraphAlignment.Left, 75);
            //table.AddColumn();

            ////table.AddColumn(ParagraphAlignment.Right, 75);

            //Row row = table.AddRow();

            //if (!string.IsNullOrEmpty(Invoice.Image))
            //{
            //    if (ImageSource.ImageSourceImpl == null)
            //        ImageSource.ImageSourceImpl = new ImageSharpImageSource<Rgba32>();

            //    Image image = row.Cells[0].AddImage(ImageSource.FromFile(Invoice.Image));
            //    row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            //    image.Height = Invoice.ImageSize.Height;
            //    image.Width = Invoice.ImageSize.Width;
            //}
            //Address leftAddress = Invoice.Company;           
            //TextFrame frame = row.Cells[1].AddTextFrame();

            //Table subTable = frame.AddTable();
            ////  subTable.AddColumn(ParagraphAlignment.Left, Unit.FromPoint(1));
           
            //subTable.AddColumn(ParagraphAlignment.Left, section.Document.PageWidth() /2);
            //subTable.AddColumn(ParagraphAlignment.Left, 5);
            //subTable.AddColumn(ParagraphAlignment.Left, section.Document.PageWidth() / 2);
            //subTable.AddColumn(ParagraphAlignment.Left, (section.Document.PageWidth() / 2)+5);
            ////subTable.AddColumn(ParagraphAlignment.Left, section.Document.PageWidth() / 2);

            //subTable.AddColumn(thirdWidth / 2);

            ////row = subTable.AddRow();
            
            ////if (!string.IsNullOrEmpty(Invoice.Image))
            ////{
            ////    if (ImageSource.ImageSourceImpl == null)
            ////        ImageSource.ImageSourceImpl = new ImageSharpImageSource<Rgba32>();

            ////    Image image = row.Cells[1].AddImage(ImageSource.FromFile(Invoice.Image));
            ////    row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            ////    image.Height = Invoice.ImageSize.Height;
            ////    image.Width = Invoice.ImageSize.Width;
            ////}

            //row = subTable.AddRow();
            //row.Cells[0].MergeRight = 1;
            //row.Style = "H2-10B-Color";
            
            

            //if (!string.IsNullOrEmpty(Invoice.Image))
            //{
            //    if (ImageSource.ImageSourceImpl == null)
            //        ImageSource.ImageSourceImpl = new ImageSharpImageSource<Rgba32>();

            //    Image image = row.Cells[0].AddImage(ImageSource.FromFile(Invoice.Image));
            //    row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            //    image.Height = Invoice.ImageSize.Height;
            //    image.Width = Invoice.ImageSize.Width;
            //}
            //row.Shading.Color = Colors.White;
            //row.Cells[0].AddParagraph(leftAddress.Title, ParagraphAlignment.Center, "H2-18A-Color");
            ////   row.Cells[0].AddParagraph(leftAddress, ParagraphAlignment.Left);
            ////  AddressCell(row.Cells[0],leftAddress.AddressLines);
            //if (!string.IsNullOrEmpty(Invoice.Image))
            //{
            //    if (ImageSource.ImageSourceImpl == null)
            //        ImageSource.ImageSourceImpl = new ImageSharpImageSource<Rgba32>();

            //    Image image = row.Cells[1].AddImage(ImageSource.FromFile(Invoice.Image));
            //    row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            //    image.Height = Invoice.ImageSize.Height;
            //    image.Width = Invoice.ImageSize.Width;
            //}


            //foreach (var item in leftAddress.AddressLines)
            //{
            //    row.Cells[0].AddParagraph(item, ParagraphAlignment.Center, "H2-10B-Color");
            //}
            
            ////row = subTable.AddRow();
            //////   row.Cells[0].AddParagraph("REFERENCE:", ParagraphAlignment.Left, "H2-9B-Color");
            ////AddressCell(row.Cells[0], companyAddress.AddressLines);


            ////row.Cells[0].AddParagraph(Invoice.Company.CompanyNumber, ParagraphAlignment.Right, "H2-9");
            ////row.Cells[0].AddParagraph(Invoice.Company.VatNumber, ParagraphAlignment.Right, "H2-9");
            //// row.Cells[0].AddParagraph("BILLING DATE:", ParagraphAlignment.Left, "H2-9B-Color");
            ////row.Cells[1].AddParagraph(Invoice.BillingDate.ToShortDateString(), ParagraphAlignment.Right, "H2-9");
            ////row.Cells[0].AddParagraph("DUE DATE:", ParagraphAlignment.Left, "H2-9B-Color");
            ////row.Cells[1].AddParagraph(Invoice.DueDate.ToShortDateString(), ParagraphAlignment.Right, "H2-9");
        }
        public void FooterSection()
        {
            HeaderFooter footer = Pdf.LastSection.Footers.Primary;

            Table table = footer.AddTable();
            table.AddColumn(footer.Section.PageWidth() / 2);
            table.AddColumn(footer.Section.PageWidth() / 2);
            Row row = table.AddRow();
            if (!string.IsNullOrEmpty(Invoice.Footer))
            {
                Paragraph paragraph = row.Cells[0].AddParagraph(Invoice.Footer, ParagraphAlignment.Left, "H2-8-Blue");
                _ = paragraph.AddHyperlink(Invoice.Footer, HyperlinkType.Web);
            }

            Paragraph info = row.Cells[1].AddParagraph();
            info.Format.Alignment = ParagraphAlignment.Right;
            info.Style = "H2-8";
            info.AddText("Page ");
            info.AddPageField();
            info.AddText(" of ");
            info.AddNumPagesField();
        }

        //backup
        //private void AddressSection()
        //{
        //    Section section = Pdf.LastSection;

        //    Address leftAddress = Invoice.Company;
        //    Address rightAddress = Invoice.Client;

        //    if (Invoice.CompanyOrientation == PositionOption.Right)
        //        Utils.Swap<Address>(ref leftAddress, ref rightAddress);

        //    Table table = section.AddTable();
        //    table.AddColumn(ParagraphAlignment.Left, section.Document.PageWidth() / 2 - 10);
        //    table.AddColumn(ParagraphAlignment.Center, Unit.FromPoint(20));
        //    table.AddColumn(ParagraphAlignment.Left, section.Document.PageWidth() / 2 - 10);

        //    Row row = table.AddRow();
        //    row.Style = "H2-10B-Color";
        //    row.Shading.Color = Colors.White;

        //    row.Cells[0].AddParagraph(leftAddress.Title, ParagraphAlignment.Left);
        //    // row.Cells[0].Format.Borders.Bottom = BorderLine;
        //    row.Cells[2].AddParagraph(rightAddress.Title, ParagraphAlignment.Left);
        //    //row.Cells[2].Format.Borders.Bottom = BorderLine;

        //    row = table.AddRow();
        //    AddressCell(row.Cells[0], leftAddress.AddressLines);
        //    AddressCell(row.Cells[2], rightAddress.AddressLines);

        //    _ = table.AddRow();
        //}

        private void AddressSection()
        {
            
            Section section = Pdf.LastSection;

            Address leftAddress = Invoice.Client;
            Address rightAddress = null;

            //Address rightAddress = Invoice.ClientDelivery;

            //if (Invoice.CompanyOrientation == PositionOption.Right)
            //    Utils.Swap<Address>(ref leftAddress, ref rightAddress);

            Table table = section.AddTable();
            table.AddColumn(ParagraphAlignment.Left, section.Document.PageWidth() / 2-42);
            table.AddColumn(ParagraphAlignment.Left,section.Document.PageWidth() / 4);
           // table.AddColumn(ParagraphAlignment.Left, section.Document.PageWidth() / 10);
            table.AddColumn(ParagraphAlignment.Left, section.Document.PageWidth()/7);
            table.AddColumn(ParagraphAlignment.Left, (section.Document.PageWidth()/15)- 15-15);

            Row row = table.AddRow();
            row.Style = "H2-12B-Color";
            row.Shading.Color = Colors.White;
            if (leftAddress != null) row.Cells[0].AddParagraph(leftAddress.Title, ParagraphAlignment.Left);
            if (rightAddress != null) row.Cells[1].AddParagraph(rightAddress.Title, ParagraphAlignment.Left);
            row.Cells[2].AddParagraph("TAX  ", ParagraphAlignment.Left, "H2-16A-Color");
            row.Cells[3].AddParagraph("INVOICE", ParagraphAlignment.Center, "H2-16A-Color");
            // row.Cells[0].Format.Borders.Bottom = BorderLine;
            //   row.Cells[2].AddParagraph(rightAddress.Title, ParagraphAlignment.Left);
            //row.Cells[2].Format.Borders.Bottom = BorderLine;

            row = table.AddRow();
            //billing to
            row.Style = "H2-12B-Color";
            AddressCell(row.Cells[0], leftAddress.AddressLines);
            ////Delivery To
            if (rightAddress!=null)
            {
           
                AddressCell(row.Cells[1], rightAddress.AddressLines);
            }
                     

            row.Cells[2].AddParagraph("Invoice No.:", ParagraphAlignment.Left, "H2-9B-Color");
            row.Cells[3].AddParagraph(Invoice.Reference, ParagraphAlignment.Left, "H2-9");
            row.Cells[2].AddParagraph("Invoice Date:", ParagraphAlignment.Left, "H2-9B-Color");
            row.Cells[3].AddParagraph(Invoice.BillingDate.ToShortDateString(), ParagraphAlignment.Left, "H2-9");
            row.Cells[2].AddParagraph("Due Date:", ParagraphAlignment.Left, "H2-9B-Color");
            row.Cells[3].AddParagraph(Invoice.DueDate.ToShortDateString(), ParagraphAlignment.Left, "H2-9");
          //  AddressCell(row.Cells[2], leftAddress.AddressLines);
            _ = table.AddRow();
        }

        private void AddressCell(Cell cell, string[] address)
        {
            foreach (string line in address)
            {
                Paragraph name = cell.AddParagraph();
                if (line == address[0])
                    name.AddFormattedText(line, "H2-9-Grey");
                else
                    name.AddFormattedText(line, "H2-9-Grey");
                //if (line == address[0])
                //    name.AddFormattedText(line, "H2-10B");
                //else
                //    name.AddFormattedText(line, "H2-9-Grey");
            }
        }

        private void BillingSection()
        {
            Section section = Pdf.LastSection;

            Table table = section.AddTable();

            double width = section.PageWidth();
            double productWidth = Unit.FromPoint(150);
            double numericWidth = (width - productWidth) / (Invoice.HasDiscount ? 5 : 4);
            table.AddColumn(productWidth);
            table.AddColumn(ParagraphAlignment.Center, numericWidth);
            table.AddColumn(ParagraphAlignment.Center, numericWidth);
            table.AddColumn(ParagraphAlignment.Center, numericWidth);
            if (Invoice.HasDiscount)
                table.AddColumn(ParagraphAlignment.Center, numericWidth);
            table.AddColumn(ParagraphAlignment.Center, numericWidth);
            table.AddColumn(ParagraphAlignment.Center, numericWidth);

            BillingHeader(table);

            foreach (ItemRow item in Invoice.Items)
            {
                BillingRow(table, item);
            }

            if (Invoice.Totals != null)
            {
                foreach (TotalRow total in Invoice.Totals)
                {
                    BillingTotal(table, total);
                }
            }
            table.AddRow();
        }

        private void BillingHeader(Table table)
        {
            Row row = table.AddRow();
            row.BottomPadding = 0;
            row.Borders.Bottom = BorderLine;
            row = table.AddRow();
            row.HeadingFormat = true;
            row.Style = "H2-10B-Color";
            row.Shading.Color = Colors.White;
            row.TopPadding = 5;
            //row.Borders.Bottom = BorderLine;

            row.Cells[0].AddParagraph("Product Description", ParagraphAlignment.Left);
            row.Cells[1].AddParagraph("Qty", ParagraphAlignment.Center);
            row.Cells[2].AddParagraph("Uom", ParagraphAlignment.Center);
            row.Cells[3].AddParagraph("UnitPrice", ParagraphAlignment.Center);
            if (Invoice.HasDiscount)
            {
                row.Cells[4].AddParagraph("Discount", ParagraphAlignment.Center);
                row.Cells[5].AddParagraph("Total", ParagraphAlignment.Center);
            }
            else
            {
                row.Cells[4].AddParagraph("Total", ParagraphAlignment.Center);
            }
            row = table.AddRow();
            row.Borders.Bottom = BorderLine;
        }

        private void BillingRow(Table table, ItemRow item)
        {
            Row row = table.AddRow();
            row.Style = "TableRow";
            // row.Shading.Color = MigraDocHelpers.BackColorFromHtml(Invoice.BackColor);

            Cell cell = row.Cells[0];
            cell.AddParagraph(item.Name, ParagraphAlignment.Left, "H2-9B");
            //cell.AddParagraph(item.Description, ParagraphAlignment.Left, "H2-9-Grey");

            cell = row.Cells[1];
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph(item.Qty.ToString(), ParagraphAlignment.Right, "H2-9");

            cell = row.Cells[2];
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph(item.Uom, ParagraphAlignment.Center, "H2-9");

            cell = row.Cells[3];
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph(item.Price.ToCurrency(Invoice.Currency), ParagraphAlignment.Center, "H2-9");

            if (Invoice.HasDiscount)
            {
                cell = row.Cells[4];
                cell.VerticalAlignment = VerticalAlignment.Center;
                cell.AddParagraph(item.Discount, ParagraphAlignment.Center, "H2-9");

                cell = row.Cells[5];
                cell.VerticalAlignment = VerticalAlignment.Center;
                cell.AddParagraph(item.Total.ToCurrency(Invoice.Currency), ParagraphAlignment.Center, "H2-9");
            }
            else
            {
                cell = row.Cells[4];
                cell.VerticalAlignment = VerticalAlignment.Center;
                cell.AddParagraph(item.Total.ToCurrency(Invoice.Currency), ParagraphAlignment.Center, "H2-9");
            }
            
        }

        private void BillingTotal(Table table, TotalRow total)
        {
            if (Invoice.HasDiscount)
            {
                table.Columns[4].Format.Alignment = ParagraphAlignment.Left;
                table.Columns[5].Format.Alignment = ParagraphAlignment.Left;
            }
            else
            {
                table.Columns[4].Format.Alignment = ParagraphAlignment.Left;
            }

            Row row = table.AddRow();
            row.Style = "TableRow";

            string font; Color shading;
            if (total.Inverse == true)
            {
                font = "H2-9B-Inverse";
                shading = MigraDocHelpers.TextColorFromHtml(Invoice.TextColor);
            }
            else
            {
                font = "H2-9B";
                // shading = MigraDocHelpers.BackColorFromHtml(Invoice.BackColor);
            }

            if (Invoice.HasDiscount)
            {
                Cell cell = row.Cells[4];
                // cell.Shading.Color = shading;
                cell.AddParagraph(total.Name, ParagraphAlignment.Left, font);

                cell = row.Cells[5];
                //  cell.Shading.Color = shading;
                cell.AddParagraph(total.Value.ToCurrency(Invoice.Currency), ParagraphAlignment.Center, font);


            }
            else
            {
                Cell cell = row.Cells[3];
                // cell.Shading.Color = shading;
                cell.AddParagraph(total.Name, ParagraphAlignment.Left, font);

                cell = row.Cells[4];
                //cell.Shading.Color = shading;
                cell.AddParagraph(total.Value.ToCurrency(Invoice.Currency), ParagraphAlignment.Center, font);
            }
        }

        private void PaymentSection()
        {
            Section section = Pdf.LastSection;

            Table table = section.AddTable();
            table.AddColumn(Unit.FromPoint(section.Document.PageWidth()));
            Row row = table.AddRow();

            if (Invoice.Details != null && Invoice.Details.Count > 0)
            {
                foreach (DetailRow detail in Invoice.Details)
                {
                    row.Cells[0].AddParagraph(detail.Title, ParagraphAlignment.Left, "H2-9B-Color");
                    row.Cells[0].Borders.Bottom = BorderLine;

                    row = table.AddRow();
                    TextFrame frame = null;
                    foreach (string line in detail.Paragraphs)
                    {
                        if (line == detail.Paragraphs[0])
                        {
                            frame = row.Cells[0].AddTextFrame();
                            frame.Width = section.Document.PageWidth();
                        }
                        frame.AddParagraph(line, ParagraphAlignment.Left, "H2-9");
                    }
                }
            }

            //if (Invoice.Company.HasCompanyNumber || Invoice.Company.HasVatNumber)
            //{
            //    row = table.AddRow();

            //    Color shading = MigraDocHelpers.TextColorFromHtml(Invoice.TextColor);

            //    if (Invoice.Company.HasCompanyNumber && Invoice.Company.HasVatNumber)
            //    {
            //        row.Cells[0].AddParagraph(string.Format("Company Number: {0}, VAT Number: {1}",
            //            Invoice.Company.CompanyNumber, Invoice.Company.VatNumber),
            //            ParagraphAlignment.Center, "H2-9B-Inverse")
            //            .Format.Shading.Color = shading;
            //    }
            //    else
            //    {
            //        if (Invoice.Company.HasCompanyNumber)
            //            row.Cells[0].AddParagraph(string.Format("Company Number: {0}", Invoice.Company.CompanyNumber),
            //            ParagraphAlignment.Center, "H2-9B-Inverse")
            //            .Format.Shading.Color = shading;
            //        else
            //            row.Cells[0].AddParagraph(string.Format("VAT Number: {0}", Invoice.Company.VatNumber),
            //            ParagraphAlignment.Center, "H2-9B-Inverse")
            //            .Format.Shading.Color = shading;
            //    }
            //}


        }

        private void FooterTextSection()
        {
            Section section = Pdf.LastSection;

            Table table = section.AddTable();
            table.AddColumn(Unit.FromPoint(section.Document.PageWidth()));
            Row row = table.AddRow();

            if (Invoice.FooterText != null && Invoice.FooterText.Count > 0)
            {
                foreach (FooterRow detail in Invoice.FooterText)
                {
                    row.Cells[0].AddParagraph(detail.TextValue, ParagraphAlignment.Left, "H2-9B-Color");
                    //  row.Cells[0].Borders.Bottom = BorderLine;

                    //row = table.AddRow();
                    //TextFrame frame = null;
                    //foreach (string line in detail.Paragraphs)
                    //{
                    //    if (line == detail.Paragraphs[0])
                    //    {
                    //        frame = row.Cells[0].AddTextFrame();
                    //        frame.Width = section.Document.PageWidth();
                    //    }
                    //    frame.AddParagraph(line, ParagraphAlignment.Left, "H2-9");
                    //}
                }
            }
        }
    }
}
