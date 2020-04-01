using Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib.Models
{
    public class Revealed
    {
        public double Percentage { get; set; }
        public Group Group { get; set; }
        public Entity Source { get; set; }
    }
}
