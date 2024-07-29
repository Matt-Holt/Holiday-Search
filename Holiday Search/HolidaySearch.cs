using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string DepartingFrom;
        public string TravelingTo;
        public DateTime DepartureDate;
        public int Duration;
        public SearchResult Result;

        public HolidaySearch(string DepartingFrom, string TravelingTo, DateTime DepartureDate, int Duration)
        {
            this.DepartingFrom = DepartingFrom;
            this.TravelingTo = TravelingTo;
            this.DepartureDate = DepartureDate;
            this.Duration = Duration;
        }

        public void Search()
        {

        }
    }
}
