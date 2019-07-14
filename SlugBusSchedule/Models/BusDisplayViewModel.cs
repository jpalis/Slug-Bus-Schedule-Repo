using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlugBusSchedule.Models
{
    public class BusDisplayViewModel
    {
        public string ClosestBusStop { get; set; }
        public string UserLatitude { get; set; }
        public string UserLongitude { get; set; }
        public List<Schedule> BusData { get; set; }
    }
}
