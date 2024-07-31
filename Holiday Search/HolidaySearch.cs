using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Holiday_Search
{
    class HolidaySearch
    {
        public string[] DepartingFrom;
        public string TravelingTo;
        public DateTime DepartureDate;
        public int Duration;
        public List<SearchResult> Result = new List<SearchResult>();

        public HolidaySearch(string[] DepartingFrom, string TravelingTo, DateTime DepartureDate, int Duration)
        {
            this.DepartingFrom = DepartingFrom;
            this.TravelingTo = TravelingTo;
            this.DepartureDate = DepartureDate;
            this.Duration = Duration;
        }

        public void Search()
        {
            using (StreamReader reader = new StreamReader("Data.json"))
            {
                //Reads json file for flight and hotel data
                string jsonStr = reader.ReadToEnd();
                JObject json = JObject.Parse(jsonStr);
                List<Hotel> hotels = json["Hotels"].ToObject<List<Hotel>>();
                List<Flight> flights = json["Flights"].ToObject<List<Flight>>();

                //Filters hotel and flights to customer details
                //Result.Hotel = FilterHotels(hotels);
                //Result.Flight = FilterFlights(flights);
            }
        }

        private Hotel FilterHotels(List<Hotel> hotels)
        {
            int cheapestPrice = int.MaxValue;
            Hotel bestMatch = null;
            foreach (Hotel hotel in hotels)
            {
                //Skip if flight not traveling to customer desination
                if (!hotel.Local_Airports.Contains(TravelingTo))
                    continue;
                //Skip if hotel doesn't match duration of stay
                if (hotel.Nights != Duration)
                    continue;
                //Skip if arrival date doesn't match departure date
                if (hotel.Arrival_Date != DepartureDate)
                    continue;
                //Skip if current hotel is more expensive than current cheapest
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
                //Skip if flight has incorrect departure airport and not departing from any airport
                if (!DepartingFrom.Contains(flight.From) && !DepartingFrom.Contains("ANY"))
                    continue;
                //Skip if flight not traveling to customer desination
                if (!flight.To.Equals(TravelingTo))
                    continue;
                //Skip if departure date doesn't match
                if (flight.Departure_Date != DepartureDate)
                    continue;
                //Skip if current flight is more expensive than current cheapest
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
