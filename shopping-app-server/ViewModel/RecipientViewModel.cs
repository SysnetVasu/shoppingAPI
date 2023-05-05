using API.Model;
using System;
using System.Collections.Generic;
using System.Text;


namespace API.ViewModel
{
    public class RecipientViewModel
    {
        public RecipientViewModel(string name, string email, AddressViewModel address)
        {
            Name = name;
            Email = email;
            Address = address;
        }

        public string Name { get; }

        public string Email { get; }

        public AddressViewModel Address { get; }
    }
}
