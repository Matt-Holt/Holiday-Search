using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Holiday_Search
{
    class HolidaySearch
    {
        /*
         * #### Customer #1
         * ##### Input
         * Departing from: Manchester Airport (MAN)
         * Traveling to: Malaga Airport (AGP)
         * Departure Date: 2023/07/01
         * Duration: 7 nights
         * */
        public string[] DepartingFrom;
        public string TravelingTo;
        public DateTime DepartureDate;
        public int Duration;
        public SearchResult Result;

        public HolidaySearch(string[] DepartingFrom, string TravelingTo, DateTime DepartureDate, int Duration)
        {
            this.DepartingFrom = DepartingFrom;
            this.TravelingTo = TravelingTo;
            this.DepartureDate = DepartureDate;
            this.Duration = Duration;
        }

        public void Search()
        {
            Result = new SearchResult();
            using (StreamReader reader = new StreamReader("Data.json"))
            {
                string jsonStr = reader.ReadToEnd();
                JObject json = JObject.Parse(jsonStr);
                List<Hotel> hotels = json["Hotels"].ToObject<List<Hotel>>();
                List<Flight> flights = json["Flights"].ToObject<List<Flight>>();
                foreach (Hotel hotel in hotels)
                {
                    if (hotel.Id == 9)
                    {
                        Result.Hotel = hotel;
                    }
                }
                foreach (Flight flight in flights)
                {
                    if (flight.Id == 2)
                    {
                        Result.Flight = flight;
                    }
                }
            }
        }
    }
}
