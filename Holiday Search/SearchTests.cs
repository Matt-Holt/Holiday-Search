using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

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
            Flight flight = search.Result.First().Flight;
            Assert.IsTrue(flight.Id == 2);
            //Check Hotel
            Hotel hotel = search.Result.First().Hotel;
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
            Flight flight = search.Result.First().Flight;
            Assert.IsTrue(flight.Id == 6);
            //Check Hotel
            Hotel hotel = search.Result.First().Hotel;
            Assert.IsTrue(hotel.Id == 5);
        }

        /*
         * #### Customer #3
         * ##### Input
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
            Flight flight = search.Result.First().Flight;
            Assert.IsTrue(flight.Id == 7);
            //Check Hotel
            Hotel hotel = search.Result.First().Hotel;
            Assert.IsTrue(hotel.Id == 6);
        }

        /*
         * ##### Input
         * Departing from: Invalid location (N/A)
         * Traveling to: Malaga Airport-Costa de Sol (AGP)
         * Departure Date: 2022/11/10
         * Duration: 14 nights
         * 
         * ##### Expected result
         * Flight and Hotel both null
         */
        [TestMethod]
        public void Test4_IncorrectDepartureLocation()
        {
            HolidaySearch search = new HolidaySearch(new string[] { "N/A" }, "AGP", new DateTime(2022, 11, 10), 14);
            search.Search();

            //Check flight
            Flight flight = search.Result.First().Flight;
            Assert.IsNull(flight);
            //Check Hotel
            Hotel hotel = search.Result.First().Hotel;
            Assert.IsNull(hotel);
        }

        /*
         * ##### Input
         * Departing from: Manchester Airport (MAN)
         * Traveling to: Amsterdam Airport Schiphol (AMS)
         * Departure Date: 2023/07/01
         * Duration: 7 nights
         * 
         * ##### Expected result
         * Flight and Hotel both null
         */
        [TestMethod]
        public void Test5_IncorrectDestination()
        {
            HolidaySearch search = new HolidaySearch(new string[] { "MAN" }, "AMS", new DateTime(2023, 7, 1), 7);
            search.Search();

            //Check flight
            Flight flight = search.Result.First().Flight;
            Assert.IsNull(flight);
            //Check Hotel
            Hotel hotel = search.Result.First().Hotel;
            Assert.IsNull(hotel);
        }
        /*
         * ##### Input
         * Departing from: Manchester Airport (MAN)
         * Traveling to: Malaga Airport (AGP)
         * Departure Date: 2024/07/01
         * Duration: 7 nights
         * 
         * ##### Expected result
         * Flight and Hotel both null
         */
        [TestMethod]
        public void Test6_IncorrectDate()
        {
            HolidaySearch search = new HolidaySearch(new string[] { "MAN" }, "AGP", new DateTime(2024, 7, 1), 7);
            search.Search();

            //Check flight
            Flight flight = search.Result.First().Flight;
            Assert.IsNull(flight);
            //Check Hotel
            Hotel hotel = search.Result.First().Hotel;
            Assert.IsNull(hotel);
        }

        /*
         * ##### Input
         * Departing from: Manchester Airport (MAN)
         * Traveling to: Mallorca Airport (PMI)
         * Departure Date: 2023/06/15
         * Duration: 14 nights
         * 
         * ##### Expected result
         * Flights 3, 4, 5 and Hotels 3, 4
         */
        [TestMethod]
        public void Test7_MultipleResults()
        {
            HolidaySearch search = new HolidaySearch(new string[] { "MAN", "LTN" }, "PMI", new DateTime(2023, 6, 15), 14);
            search.Search();

            //Check flights
            Flight flight1 = search.Result.First().Flight;
            Assert.IsTrue(flight1.Id == 3);
            Flight flight2 = search.Result.ElementAt(1).Flight;
            Assert.IsTrue(flight2.Id == 4);
            Flight flight3 = search.Result.ElementAt(2).Flight;
            Assert.IsTrue(flight3.Id == 5);


            //Check hotels
            Hotel hotel1 = search.Result.First().Hotel;
            Assert.IsTrue(hotel1.Id == 3);
            Hotel hotel2 = search.Result.ElementAt(1).Hotel;
            Assert.IsTrue(hotel2.Id == 4);
        }
    }
}
