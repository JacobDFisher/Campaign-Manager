using System;
using System.Collections.Generic;
using System.Text;

namespace Lib.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Group> MemberOf { get; set; }
        public IEnumerable<Group> MemberGroups { get; set; }
        public IEnumerable<Identity> MemberIdentities { get; set; }
        public IEnumerable<Entity> MemberEntities { get; set; }
    }
}
