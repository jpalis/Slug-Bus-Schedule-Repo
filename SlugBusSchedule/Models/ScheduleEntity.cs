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
        public SlugContext()
        {

        }

        public DbSet<Schedule> Schedules { get; set; }
    }

    public class Schedule
    {
        [Key]
        public int ID { get; set; }
        public string BusNumber { get; set; }
        public string Street { get; set; }

        //arrival times for each bus stop
        public string Base { get; set; }
        public string Village { get; set; }
        public string EastRemote { get; set; }
        public string BookStore { get; set; }
        public string CrownMerill { get; set; }
        public string NineTen { get; set; }
        public string ScienceHill { get; set; }
        public string Kresge { get; set; }
        public string PorterRachelCarson { get; set; }
        public string Oakes { get; set; }
        public string WesternDr { get; set; }


    }
}
