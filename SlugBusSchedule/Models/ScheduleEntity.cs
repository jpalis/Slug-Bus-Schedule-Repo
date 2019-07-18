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
        public SlugContext()
        {
        }

        public SlugContext(DbContextOptions<SlugContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            var connection = @"Server=tcp:sbs-sql-db.database.windows.net,1433;Initial Catalog=SBS-SQL-DB;Persist Security Info=False;User ID=jpalis;Password=JP62195!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            builder.UseSqlServer(connection, b =>
            {
                b.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
            base.OnConfiguring(builder);
        }

        
        public DbSet<Schedule> Schedules { get; set; }
    }

    public class Schedule
    {
        [Key]
        public int ID { get; set; }
        public int BusNumber { get; set; }
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
