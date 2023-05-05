using System;

namespace API.Entities
{
    public class Category : BaseEntity
    {
        //public string Name { get; set; }

        //public string PhotoUrl { get; set; }       
        public string Name { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
