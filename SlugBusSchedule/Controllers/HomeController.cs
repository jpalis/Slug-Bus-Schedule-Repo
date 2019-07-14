using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SlugBusSchedule.Models;

namespace SlugBusSchedule.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromBody] Position position)
        {
            //get string {latitude:"",longitude""} and parse into lat and lon values
            double lat = position.latitude;
            double lon = position.longitude;
            //using latitude and longitute, find closest bus stop, and populate new BusDisplayViewModel to show user
            string nearestBusStop = Map.findBusStop(lat, lon);

            //reroute user to bus card page
            TempData["UserLocation"] = "populate this with the name of the user's location";
            TempData["ClosestBusStop"] = "populate this with the name of the closest bus stop";
            return RedirectToAction("BusDisplay");
        }

        [HttpGet]
        public IActionResult BusDisplay()
        {
            BusDisplayViewModel model = new BusDisplayViewModel();
            model.UserLocation = TempData["UserLocation"].ToString();
            model.ClosestBusStop = TempData["ClosestBusStop"].ToString();

            string currentTime = DateTime.Now.ToString("hh:mm:ss");

            //get list of all available buses
            List<string> busList = new List<string>();
            busList.Add("10");
            busList.Add("15");
            busList.Add("16");
            busList.Add("19");
            busList.Add("20");

            //from SQL DB for each bus stop, get next arrival time
            using (var db = new SlugContext())
            {
                foreach(var item in busList)
                {
                    //create new Schedule instance for each bus line
                    //for each bustop in that line, find nearest time
                    //add each Schedule to model.BusData list

                }
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
