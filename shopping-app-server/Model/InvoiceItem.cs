using System;
using System.Collections.Generic;
using System.Text;

namespace API.Model
{
    public class InvoiceItem
    {
 
        public int Number { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TaxPercentage { get; set; }
    }
}
