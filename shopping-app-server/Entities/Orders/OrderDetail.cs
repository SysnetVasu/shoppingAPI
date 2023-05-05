

using System;

namespace API.Entities.Orders
{
    public class OrderDetail : BaseEntity
    {
        public OrderDetail()
        {
        }
        //public OrderItem(string productItemId, string productName, string imageUrl, decimal price, int quantity)
        //{
        //    ProductItemId = productItemId;
        //    ProductName = productName;
        //    thumbnailUrl = imageUrl; ;
        //    Price = price;
        //    Quantity = quantity;
        //}

        //public string ProductItemId { get; set; }
        //public string ProductName { get; set; }
        //public string thumbnailUrl { get; set; }
        //public decimal Price { get; set; }
        //public int Quantity { get; set; }

        public OrderDetail(string productItemId,  string productName, string imageUrl, decimal price, int quantity,string unitId)
        {
            ProductId = productItemId;
            ProductName = productName;
            Price = price;
            Quantity = quantity;
            UnitId = UnitId;            
        }

        public string Description { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public string UnitId { get; set; }
       
        public string OrderId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
         public Boolean IsDeleted { get; set; }
        public Unit Unit { get; set; }

        public virtual Order Order { get; set; }
        //public decimal? Total { get; set; }
    }
}