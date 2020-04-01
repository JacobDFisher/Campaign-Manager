using Lib.Enums;
using Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib.Models
{
    public class Identity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Group> Groups { get; set; }
        public IEnumerable<Permission> EntityGrants { get; set; }
        public IEnumerable<Permission> DetailGrants { get; set; }
        public IEnumerable<Permissions> EntityAuthorships { get; set; }
        public IEnumerable<Permissions> DetailAuthorships { get; set; }
    }
}
