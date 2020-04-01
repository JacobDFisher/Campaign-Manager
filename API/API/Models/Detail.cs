using System;
using System.Collections.Generic;

namespace API.Models
{
    public class Detail
    {
        public string Description { get; set; }
        public int Author { get; set; }
        public Permissions Permissions { get; set; }
    }
}
