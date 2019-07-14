using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlugBusSchedule.Models
{
    public class BusDisplayViewModel
    {
        public string UserLocation { get; set; }
        public string ClosestBusStop { get; set; }
        public List<Schedule> BusData { get; set; }
    }
}
