using System;
using System.Collections.Generic;
using System.Text;


namespace API.ViewModel
{
    public class PaymentDetailsViewModel
    {
        public PaymentDetailsViewModel(string recipient, string bankAccountNumber)
        {
            Recipient = recipient;
            BankAccountNumber = bankAccountNumber;
        }


        public string Recipient { get; }

        public string BankAccountNumber { get; }
    }
}
