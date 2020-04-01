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
        public Permissions<Entity> Permissions { get; set; }
        public IEnumerable<Revealed<Entity>> RevealedEntities { get; set; }
        public IEnumerable<Revealed<Detail>> RevealedDetails { get; set; }
        public IEnumerable<Property> Properties { get; set; }
        public IEnumerable<Detail> Details { get; set; }
        public IEnumerable<Group> Groups { get; set; }
    }
}
