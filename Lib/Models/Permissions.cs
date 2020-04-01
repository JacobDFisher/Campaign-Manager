using System;
using System.Collections.Generic;
using System.Text;

namespace Lib.Models
{
    public class Permissions<T>
    {
        public Identity Author { get; set; }
        public T Endpoint { get; set; }
        public IEnumerable<Permission> Perms { get; set; }
        public IEnumerable<Revealed> Revealed { get; set; }
    }
}
