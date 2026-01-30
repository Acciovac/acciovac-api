using System;
using System.Collections.Generic;
using System.Text;

namespace acciovac.Domain.Itinerary
{
    public class Itinerary
    {
        public string Destination { get; set; }
        public int Days { get; set; }
        public List<ItineraryDay> DaysPlan { get; set; } = new();
    }

    public class ItineraryDay
    {
        public int Day { get; set; }
        public List<string> Activities { get; set; } = new();
    }

}
