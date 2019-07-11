using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SlugBusSchedule.Models
{
    public class SlugContext : DbContext
    {
        public SlugContext(DbContextOptions<SlugContext> options)
            : base(options)
        { }

        public DbSet<Schedule> Schedules { get; set; }
    }

    public class Schedule
    {
        [Key]
        public int ID { get; set; }
        public int BusNumber { get; set; }
        public string Street { get; set; }

        //arrival times for each bus stop
        public DateTime Base { get; set; }
        public DateTime Village { get; set; }
        public DateTime EastRemote { get; set; }
        public DateTime BookStore { get; set; }
        public DateTime CrownMerill { get; set; }
        public DateTime NineTen { get; set; }
        public DateTime ScienceHill { get; set; }
        public DateTime Kresge { get; set; }
        public DateTime PorterRachelCarson { get; set; }
        public DateTime Oakes { get; set; }


    }
}
