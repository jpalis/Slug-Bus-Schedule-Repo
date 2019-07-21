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
        public static string g_userLat { get; set; }
        public static string g_userLon { get; set; }
        public static string g_userBusStop { get; set; }

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

            g_userLat = lat.ToString();
            g_userLon = lon.ToString();
            g_userBusStop = nearestBusStop;

            //reroute user to bus card page
            return BusDisplay(lat.ToString(), lon.ToString(), nearestBusStop);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult ReRouteData(string myvar)
        {
            if(myvar == "Index")
            {
                return Index();
            }
            else
            {
                return BusDisplay(g_userLat, g_userLon, g_userBusStop);
            }
        }

        [HttpGet]
        public IActionResult BusDisplay(string lat, string lon, string nearestBusStop)
        {
            BusDisplayViewModel model = new BusDisplayViewModel();
            model.UserLatitude = lat;
            model.UserLongitude = lon;
            model.ClosestBusStop = nearestBusStop;
            model.BusData = new List<ArrivalData>();

            
            var utcTime = DateTime.UtcNow;
            TimeZoneInfo pacificZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            DateTime pacificTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, pacificZone);
            var currentTime = pacificTime.ToString("HH:mm:ss");

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
                    string fields = "BusNumber,Street,ID," + model.ClosestBusStop;

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
                            else if(p.Name == "ID")
                            {
                                data.ID = e.ID;
                            }
                            else
                            {
                                if(p.Name != "LowCapacity" && p.Name != "MediumCapacity" && p.Name != "HighCapacity" && p.Name != "CurrentStatus")
                                {

                                    data.ArrivalTime1 = p.GetValue(e).ToString();

                                    string timed = p.GetValue(e).ToString();
                                    timed = timed.Remove(timed.Length - 3); //removed seconds

                                    
                                    string timed1 = timed.Substring(0,2);
                                    int h1 = Int32.Parse(timed1);
                                
                                    if (h1 > 12) {
                                        h1 = h1 - 12;
                                        data.ArrivalTime = h1.ToString() + timed.Substring(2) +(" PM");
                                    }
                                    else 
                                    {
                                        data.ArrivalTime = timed + (" AM");
                                    }
                                }
                            }
                        }
                    }

                    if(data.ArrivalTime != "0" && !string.IsNullOrEmpty(data.ArrivalTime))
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

        public string UpdateBusCapacity(BusCapacityModel model)
        {
            string currenStatus;

            int busCapacityUpdateAmount;
            if(model.UserStatusInput == "low")
            {
                busCapacityUpdateAmount = 1;
            }
            else if(model.UserStatusInput == "medium")
            {
                busCapacityUpdateAmount = 3;
            }
            else
            {
                busCapacityUpdateAmount = 5;
            }

            using (var db = new SlugContext())
            {
                Schedule result = (Schedule)db.Schedules.Where(j => j.ID == model.ID).FirstOrDefault();
                if(result != null)
                {

                    if (model.UserStatusInput == "low")
                    {
                        result.LowCapacity +=  busCapacityUpdateAmount;
                    }
                    else if (model.UserStatusInput == "medium")
                    {
                        result.MediumCapacity += busCapacityUpdateAmount;
                    }
                    else
                    {
                        result.HighCapacity += busCapacityUpdateAmount;
                    }

                    if(result.LowCapacity > result.MediumCapacity)
                    {
                        if(result.LowCapacity > result.HighCapacity)
                        {
                            result.CurrentStatus = "low";
                        }
                        else
                        {
                            result.CurrentStatus = "high";
                        }
                    }
                    else
                    {
                        if (result.MediumCapacity > result.HighCapacity)
                        {
                            result.CurrentStatus = "medium";
                        }
                        else
                        {
                            result.CurrentStatus = "high";
                        }
                    }
                }

                currenStatus = result.CurrentStatus;
                db.SaveChanges();
            }

            return currenStatus;
        }
    }
}
