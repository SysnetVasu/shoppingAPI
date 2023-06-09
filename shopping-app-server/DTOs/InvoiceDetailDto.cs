﻿using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class InvoiceDetailDto
    {
        //public string ProductItemId { get; set; }
        //public string ProductName { get; set; }
        //public string thumbnailUrl { get; set; }
        //public double Price { get; set; }
        //public int Quantity { get; set; }
        public string Description { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public string ProductId { get; set; }
        public string UnitId { get; set; }
        public Int64 OrdersId { get; set; }
        public UnitDto Unit {get;set;}
        public ProductDto Product { get; set; }


    }
}
