using API.Model;
using System;
using System.Collections.Generic;
using System.Text;


namespace API.ViewModel
{
    public class SenderViewModel
    {
        public SenderViewModel(string name, string vatNumber, string registrationNumber, string email, string website, AddressViewModel address)
        {
            Name = name;
            VatNumber = vatNumber;
            RegistrationNumber = registrationNumber;
            Email = email;
            Website = website;
            Address = address;
        }

        public string Name { get; }
        public string VatNumber { get; }
        public string RegistrationNumber { get; }
        public string Email { get; set; }
        public string Website { get; set; }
        public AddressViewModel Address { get;  }
    }
}
