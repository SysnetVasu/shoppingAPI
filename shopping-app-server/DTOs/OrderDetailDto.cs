using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{

    public class OrderDetailDto
    {
        //public string ProductItemId { get; set; }
        //public string ProductName { get; set; }
        //public string thumbnailUrl { get; set; }
        //public double Price { get; set; }
        //public int Quantity { get; set; }
        public string Description { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
        public double Total { get; set; }
        public string ProductId { get; set; }
        public string UnitId { get; set; }
        public string OrderId { get; set; }
        public UnitDto Unit {get;set;}
        public ProductDto Product { get; set; }


    }
}
