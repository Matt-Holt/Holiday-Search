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
                Result.Hotel = FilterHotels(hotels);
                Result.Flight = FilterFlights(flights);
            }
        }

        private Hotel FilterHotels(List<Hotel> hotels)
        {
            int cheapestPrice = int.MaxValue;
            Hotel bestMatch = null;
            foreach (Hotel hotel in hotels)
            {
                if (!hotel.Local_Airports.Contains(TravelingTo))
                    continue;
                if (hotel.Nights != Duration)
                    continue;
                if (hotel.Arrival_Date != DepartureDate)
                    continue;
                if (!IsCheaper(hotel.Price_Per_Night * hotel.Nights, cheapestPrice))
                    continue;

                bestMatch = hotel;
                cheapestPrice = hotel.Price_Per_Night * hotel.Nights;
            }
            return bestMatch;
        }

        private Flight FilterFlights(List<Flight> flights)
        {
            int cheapestPrice = int.MaxValue;
            Flight bestMatch = null;
            foreach (Flight flight in flights)
            {
                if (!DepartingFrom.Contains(flight.From))
                    continue;
                if (!flight.To.Equals(TravelingTo))
                    continue;
                if (flight.Departure_Date != DepartureDate)
                    continue;
                if (!IsCheaper(flight.price, cheapestPrice))
                    continue;

                bestMatch = flight;
                cheapestPrice = flight.price;
            }
            return bestMatch;
        }

        private bool IsCheaper(int valueA, int valueB)
        {
            return valueA < valueB;
        }
    }
}
