using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class CartItem
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Uom { get; set; }
        public string thumbnailUrl { get; set; }
        public string CategoryId { get; set; }
        public string UnitId { get; set; }
     
    }
}
