using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models
{
    public class Entity: EntityHeader
    {
        public IEnumerable<Property> Properties { get; set; }
        public IEnumerable<Detail> Details { get; set; }
    }
}
