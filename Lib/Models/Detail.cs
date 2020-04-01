using System;
using System.Collections.Generic;

namespace Lib.Models
{
    public class Detail
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Permissions<Detail> Permissions { get; set; }
        public Entity Entity { get; set; }
    }
}
