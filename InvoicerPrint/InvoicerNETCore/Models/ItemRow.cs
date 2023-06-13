namespace InvoicePrintFormat.Models
{
    public class ItemRow
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Qty { get; set; }
        public string Uom { get; set; }
        public decimal Price { get; set; }       
        public string Discount { get; set; }     
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public decimal NetTotal { get; set; }
        public bool HasDiscount
        {
            get
            {
                return !string.IsNullOrEmpty(Discount);
            }
        }

        //public static ItemRow Make(string name, string description, decimal qty, string uom, decimal price, decimal discount, decimal total, decimal tax, decimal nettotal)
        //{
        //    return Make(name, description, qty, uom, price, discount,total, tax, nettotal);
        //}

        public static ItemRow Make(string name, string description, decimal qty, string uom, decimal price, decimal discount, decimal total, decimal tax, decimal nettotal)
        {
            return new ItemRow()
            {
                Name = name,
                Description = description,
                Qty = qty,
                Uom = uom,
                Price = price,
                Discount = discount.ToString(),
                Total = total,
                Tax = tax,
                NetTotal=nettotal
            };
        }
    }
}