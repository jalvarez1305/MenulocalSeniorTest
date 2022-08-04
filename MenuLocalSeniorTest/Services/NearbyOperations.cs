using System;
using System.Linq;
using System.Collections.Generic;
using Rest.Models;
using Rest.Services;

namespace MenuLocalSeniorTest.Services
{
    public class NearbyOperations
    {
        int MINUTES_TO_DRINK = 20;
        public List<NearbyResult> ListOfBars;
        public NearbyOperations(List<NearbyResult> ListOfBars)
        {
            this.ListOfBars = ListOfBars;
        }

        public void OrderList(Location startLocation)
        {
            foreach (var item in ListOfBars)
            {
                item.Distance = Math.Abs(Math.Abs(item.geometry.location.lat) - Math.Abs(startLocation.lat));
                item.Distance += Math.Abs(Math.Abs(item.geometry.location.lng) - Math.Abs(startLocation.lng));
            }
            ListOfBars.Sort(new NearbyResultComparer());
        }
        public void GetTimeToArrive(Location startLocation,DateTime StartTime)
        {
            Location lastLocation = startLocation;
            DateTime lastTime = StartTime;
            DistanceService distanceService = new DistanceService();

            foreach (var item in ListOfBars)
            {
                int Minutes = distanceService.GetMinutes(lastLocation, item.geometry.location, lastTime);
                item.ArrivedTime = lastTime.AddMinutes(Minutes);
                lastTime = lastTime.AddMinutes(Minutes).AddMinutes(MINUTES_TO_DRINK);
                lastLocation = item.geometry.location;
            }
        }
    }
}
