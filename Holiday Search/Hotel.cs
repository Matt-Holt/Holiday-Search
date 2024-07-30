using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holiday_Search
{
    class Hotel
    {
        public int Id;
        public string Name;
        public DateTime ArrivalDate;
        public int PricePerNight;
        public string[] LocalAirports;
        public int nights;

        public Hotel(int Id)
        {
            this.Id = Id;
        }
    }
}
