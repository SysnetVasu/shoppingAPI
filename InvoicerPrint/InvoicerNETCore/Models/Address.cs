namespace InvoicePrintFormat.Models
{
    public class Address
    {
        public string Title { get; set; }
        public string[] AddressLines { get; set; }
        public string VatNumber { get; set; }
        public string CompanyNumber { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyEmail { get; set; }
        public bool HasCompanyNumber { get { return !string.IsNullOrEmpty(CompanyNumber); } }
        public bool HasVatNumber { get { return !string.IsNullOrEmpty(VatNumber); } }

        public static Address Make(string title, string[] address, string companyPhone=null,string companyemail=null, string company = null, string vat = null)
        {
            return new Address
            {
                Title = title,
               AddressLines = address,
               CompanyPhone=companyPhone,
               CompanyEmail=companyemail,
                CompanyNumber = company,
                VatNumber = vat,
            };
        }
    }
}