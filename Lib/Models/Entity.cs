using Lib.Enums;
using Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib.Models
{
    public class Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Permissions Permissions { get; set; }
        public IEnumerable<Revealed> RevealedEntities { get; set; }
        public IEnumerable<Revealed> RevealedDetails { get; set; }
        public IEnumerable<Property> Properties { get; set; }
        public IEnumerable<Detail> Details { get; set; }
        public IEnumerable<Group> Groups { get; set; }
    }
}
