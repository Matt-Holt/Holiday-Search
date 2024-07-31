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
                hotels = FilterHotels(hotels);
                hotels = hotels.OrderBy(h => h.Price_Per_Night * h.Nights).ToList(); //Filters hotels and orders by price per night
                List<Flight> flights = json["Flights"].ToObject<List<Flight>>();
                flights = FilterFlights(flights);
                flights = flights.OrderBy(f => f.price).ToList(); //Filters flights and orders by price

                //Adds each flight and hotel to a search result
                int listSize = (flights.Count > hotels.Count) ? flights.Count : hotels.Count;
                for(int i = 0; i < listSize; i++)
                {
                    SearchResult r = new SearchResult();
                    if (i < flights.Count)
                        r.Flight = flights.ElementAt(i);
                    if (i < hotels.Count)
                        r.Hotel = hotels.ElementAt(i);

                    Result.Add(r);
                }
            }
        }

        private List<Hotel> FilterHotels(List<Hotel> hotels)
        {
            List<Hotel> hotelList = new List<Hotel>();
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

                hotelList.Add(hotel);
            }
            return hotelList;
        }

        private List<Flight> FilterFlights(List<Flight> flights)
        {
            List<Flight> flightList = new List<Flight>();
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

                flightList.Add(flight);
            }
            return flightList;
        }
    }
}
