namespace InvoicePrintFormat.Models
{
    public class FooterRow
    {
        public string TextValue { get; set; }
        public static FooterRow Make(string textvalue)
        {
            return new FooterRow()
            {
                TextValue = textvalue,                
            };
        }
    }
}