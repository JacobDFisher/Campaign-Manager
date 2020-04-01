using Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models
{
    public class Identity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<int> Groups { get; set; }
    }
}
