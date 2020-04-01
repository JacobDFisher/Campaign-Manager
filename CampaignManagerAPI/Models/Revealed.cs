using Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models
{
    public class Revealed
    {
        public int DestinationId { get; set; }
        public double Percentage { get; set; }
        public EntityHeader Source { get; set; }
    }
}
