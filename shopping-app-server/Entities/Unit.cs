using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Unit: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Units { get; set; }
    }
}
