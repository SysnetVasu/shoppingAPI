using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{

    public class Tax : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public double TaxPer { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
