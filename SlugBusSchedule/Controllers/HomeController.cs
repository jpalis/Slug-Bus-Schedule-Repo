using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SlugBusSchedule.Models;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SlugBusSchedule.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public IActionResult Landing()
        {
            return View("Home", new Position());
        }

        [HttpPost]
        public IActionResult Landing(Position position)
        {
            //get string {latitude:"",longitude""} and parse into lat and lon values
            double lat = position.latitude;
            double lon = position.longitude;
            //using latitude and longitute, find closest bus stop, and populate new BusDisplayViewModel to show user
            string nearestBusStop = Map.findBusStop(lat, lon);

            //reroute user to bus card page
            return BusDisplay(lat.ToString(), lon.ToString(), nearestBusStop);
        }

        [HttpGet]
        public IActionResult Index()
        {
            //about page
            return View("Index");
        }

        [HttpGet]
        public IActionResult BusDisplay(string lat, string lon, string nearestBusStop)
        {
            BusDisplayViewModel model = new BusDisplayViewModel();
            model.UserLatitude = lat;
            model.UserLongitude = lon;
            model.ClosestBusStop = nearestBusStop;
            model.BusData = new List<ArrivalData>();
            

            string currentTime = DateTime.Now.ToString("hh:mm:ss");

            //get list of all available buses
            List<int> busList = new List<int>();
            busList.Add(10);
            busList.Add(15);
            busList.Add(16);
            busList.Add(19);
            busList.Add(20);

            //from SQL DB for each bus stop, get next arrival time
            using (var db = new SlugContext())
            {
                foreach(var item in busList)
                {
                    ArrivalData data = new ArrivalData();
                    //Dynamically Choose Fields
                    string fields = "BusNumber,Street," + model.ClosestBusStop;

                    //select 
                        
                    var e = db.Schedules
                            .Where(m => m.BusNumber == item && TimeSpan.Parse(m.Base) >= TimeSpan.Parse(currentTime))
                            .Select(DynamicSelectGenerator<Schedule>(fields))
                            .FirstOrDefault();

                    Type type = typeof(Schedule);
                    PropertyInfo[] pList = type.GetProperties();
                    foreach(PropertyInfo p in pList)
                    {
                        if(p.GetValue(e) != null)
                        {
                            if(p.Name == "Street")
                            {
                                data.Street = e.Street;
                            }
                            else if(p.Name == "BusNumber")
                            {
                                data.BusNumber = item.ToString();
                            }
                            else
                            {
                                data.ArrivalTime = p.GetValue(e).ToString();
                            }
                        }
                    }

                    if(data.ArrivalTime != "0")
                    {
                        model.BusData.Add(data);
                    }
                    
                }
            }

            return View("BusDisplay", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public static Func<T, T> DynamicSelectGenerator<T>(string Fields = "")
        {
            string[] EntityFields;
            if (Fields == "")
                // get Properties of the T
                EntityFields = typeof(T).GetProperties().Select(propertyInfo => propertyInfo.Name).ToArray();
            else
                EntityFields = Fields.Split(',');

            // input parameter "o"
            var xParameter = Expression.Parameter(typeof(T), "o");

            // new statement "new Data()"
            var xNew = Expression.New(typeof(T));

            // create initializers
            var bindings = EntityFields.Select(o => o.Trim())
                .Select(o =>
                {

                // property "Field1"
                var mi = typeof(T).GetProperty(o);

                // original value "o.Field1"
                var xOriginal = Expression.Property(xParameter, mi);

                // set value "Field1 = o.Field1"
                return Expression.Bind(mi, xOriginal);
                }
            );

            // initialization "new Data { Field1 = o.Field1, Field2 = o.Field2 }"
            var xInit = Expression.MemberInit(xNew, bindings);

            // expression "o => new Data { Field1 = o.Field1, Field2 = o.Field2 }"
            var lambda = Expression.Lambda<Func<T, T>>(xInit, xParameter);

            // compile to Func<Data, Data>
            return lambda.Compile();
        }


    }
}
