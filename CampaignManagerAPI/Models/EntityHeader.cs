using Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class EntityHeader
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Permissions Permissions { get; set; }
    }
}
