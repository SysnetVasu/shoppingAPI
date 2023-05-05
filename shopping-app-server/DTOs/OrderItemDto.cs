using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class OrderItemDto
    {
        public string ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string thumbnailUrl { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string UnitId { get; set; }
        public UnitDto unit { get; set; }
        
    }
}
