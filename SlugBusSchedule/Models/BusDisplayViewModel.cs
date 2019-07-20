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
        public List<ArrivalData> BusData { get; set; }
    }

    public class ArrivalData
    {
        public int ID { get; set; }
        public string BusNumber { get; set; }
        public string ArrivalTime { get; set; }
        public string ArrivalTime1 { get; set; }
        public string Street { get; set; }
    }
}
