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
        public void Test1_ToAGPFromMAN7Nights()
        {
            HolidaySearch search = new HolidaySearch(new string[] { "MAN" }, "AGP", new DateTime(2023, 7, 1), 7);
            search.Search();

            //Check flight
            Flight flight = search.Result.Flight;
            Assert.IsTrue(flight.Id == 2);
            //Check Hotel
            Hotel hotel = search.Result.Hotel;
            Assert.IsTrue(hotel.Id == 9);
        }

        /*
         * #### Customer #2
         * ##### Input
         * Departing from: Any London Airport
         * Traveling to: Mallorca Airport (PMI)
         * Departure Date: 2023/06/15
         * Duration: 10 nights
         * 
         * ##### Expected result
         * Flight 6 and Hotel 5
         */
        [TestMethod]
        public void Test2_ToPMIFromLON10Nights()
        {
            HolidaySearch search = new HolidaySearch(new string[]{"LTN", "LGW"}, "PMI", new DateTime(2023, 6, 15), 10);
            search.Search();

            //Check flight
            Flight flight = search.Result.Flight;
            Assert.IsTrue(flight.Id == 6);
            //Check Hotel
            Hotel hotel = search.Result.Hotel;
            Assert.IsTrue(hotel.Id == 5);
        }

        /*##### Input
         * Departing from: Any Airport
         * Traveling to: Gran Canaria Airport (LPA)
         * Departure Date: 2022/11/10
         * Duration: 14 nights
         * 
         * ##### Expected result
         * Flight 7 and Hotel 6
         */
        [TestMethod]
        public void Test3_ToLPAFromANY14Nights()
        {
            HolidaySearch search = new HolidaySearch(new string[] { "ANY" }, "LPA", new DateTime(2022, 11, 10), 14);
            search.Search();

            //Check flight
            Flight flight = search.Result.Flight;
            Assert.IsTrue(flight.Id == 7);
            //Check Hotel
            Hotel hotel = search.Result.Hotel;
            Assert.IsTrue(hotel.Id == 6);
        }
    }
}
