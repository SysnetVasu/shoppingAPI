using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class ProductDto
    {
        public string Id { get; set; }

        //public string Name { get; set; }

        //public string Description { get; set; }

        //public string ImageUrl { get; set; }

        //public double Price { get; set; }

        //public int CategoryId { get; set; }
        public string Code { get; set; }
        public string CategoryId { get; set; }
        public string UnitId { get; set; }
        public string SupplierId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? Price { get; set; }
        public int? MinQty { get; set; }
        public string ThumbnailUrl { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string BrandId { get; set; }
        public string CartonId { get; set; }
        public string DepartmentId { get; set; }
        public CartItemDto Cagegory { get; set; }
        public UnitDto Unit { get; set; }

    }
}
