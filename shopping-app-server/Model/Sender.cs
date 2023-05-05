using System;
using System.Collections.Generic;
using System.Text;

namespace API.Model
{
    public class Sender
    {
        public string Name { get; set; } = string.Empty;

        public string VatNumber { get; set; } = string.Empty;

        public string RegistrationNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Website { get; set; } = string.Empty;

        public Address Address { get; set; } = new Address();
    }
}
