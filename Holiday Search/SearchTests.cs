using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Holiday_Search
{
    [TestClass]
    public class SearchTests
    {
        /*
         * #### Customer #1
         * ##### Input
         * Departing from: Manchester Airport (MAN)
         * Traveling to: Malaga Airport (AGP)
         * Departure Date: 2023/07/01
         * Duration: 7 nights
         * 
         * ##### Expected result
         * Flight 2 and Hotel 9
         */
        [TestMethod]
        public void Test1_ToAGPFromMan7Nights()
        {
            HolidaySearch search = new HolidaySearch("MAN", "AGP", new DateTime(2023, 7, 1), 7);
            search.Search();

            //Check flight
            Flight flight = search.Result.Flight;
            Assert.IsTrue(flight.Id == 2);
            //Check Hotel
            Hotel hotel = search.Result.Hotel;
            Assert.IsTrue(hotel.Id == 9);
        }
    }
}
