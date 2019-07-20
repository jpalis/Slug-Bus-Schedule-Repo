using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlugBusSchedule.Models
{
    public class BusCapacityModel
    {
        public int Low { get; set; }
        public int Medium { get; set; }
        public int High { get; set; }
        public string CurrentStatus { get; set; }
    }
}
