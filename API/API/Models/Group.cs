using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Group> MemberOf { get; set; }
    }
}
