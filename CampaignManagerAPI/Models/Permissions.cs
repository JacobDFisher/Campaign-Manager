using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models
{
    public class Permissions
    {
        public int Id { get; set; }
        public Identity Author { get; set; }
        public IEnumerable<Permission> Perms { get; set; }
        public IEnumerable<Revealed> Revealed { get; set; }
    }
}
