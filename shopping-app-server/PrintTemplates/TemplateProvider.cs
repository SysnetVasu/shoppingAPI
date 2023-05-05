using API.Abstractions;
using API.ViewModel;
using Stubble.Core;
using Stubble.Core.Builders;
using Stubble.Core.Settings;
using Stubble.Helpers;
using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace API.PrintTemplates

{
    public class TemplateProvider : ITemplateProvider
    {
        public async Task<string> RenderTemplate(string templateKey, InvoiceViewModel invoice)
        {
            //var culture = new CultureInfo(invoice.DisplayCulture);
            var culture = new CultureInfo("en-SA");

            var template = GetTemplateHtmlByKey(GetPath(templateKey));

            if (string.IsNullOrWhiteSpace(template))
            {
                throw new InvalidOperationException($"Invoice template with template key: {templateKey} not found");
            }
            return template;

            
          //  return await GetRenderer(culture).RenderAsync(template, invoice, new RenderSettings { CultureInfo = culture, ThrowOnDataMiss = true });
            //return template;
        }

        //private static StubbleVisitorRenderer GetRenderer(CultureInfo culture) => new StubbleBuilder()
        //        .Configure(conf => conf.AddHelpers(GetHelpers(culture)))            
        //        .Build();


        private static string GetTemplateHtmlByKey(string path) => !File.Exists(path) ? string.Empty : File.ReadAllText(path);

        private static string GetPath(string templateKey) => Path.Combine(Directory.GetCurrentDirectory(), "PrintTemplates", templateKey, "invoice.html");

        //private static Helpers GetHelpers(CultureInfo culture) => new Helpers()
        //        .Register("FormatCurrency", (HelperContext context, decimal amount) => amount.ToString("C", culture))
        //        .Register("FormatDate", (HelperContext context, DateTime date) => date.ToString("d", culture));

    }
}