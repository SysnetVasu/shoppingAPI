using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class CustomerDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
       public string CustomerId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string TaxNumber { get; set; }
        public string Descriptions { get; set; }
        public bool? IsDefault { get; set; }
        public int? OrganizationUnitId { get; set; }
        public int? GenderId { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? DiscountRate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int TaxType { get; set; }
        public string Code { get; set; }

    }
}
