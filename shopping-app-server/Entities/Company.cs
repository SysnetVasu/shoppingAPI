using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Company : BaseEntity
    {

        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string TaxPerId { get; set; }
        public string Logo { get; set; }
        public string SaleLogo { get; set; }
        public string CurrencyId { get; set; }
        public int CurrencyPosition { get; set; }
        public string FooterText { get; set; }
        public string DBName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int IsDeleted { get; set; }
    }
}
