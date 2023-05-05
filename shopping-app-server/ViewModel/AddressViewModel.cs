using System;
using System.Collections.Generic;
using System.Text;

namespace API.ViewModel
{
    public class AddressViewModel
    {
        public AddressViewModel(string addressLine1, string addressLine2, string postalCode, string city, string country)
        {
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            PostalCode = postalCode;
            City = city;
            Country = country;
        }

        public string AddressLine1 { get; }
        public string AddressLine2 { get; }
        public string PostalCode { get;  }
        public string City { get; }
        public string Country { get; }
    }
}
