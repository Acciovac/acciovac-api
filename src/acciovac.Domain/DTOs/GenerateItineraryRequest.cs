using System;
using System.Collections.Generic;
using System.Text;

namespace acciovac.Domain.DTOs
{
    public class GenerateItineraryRequest
    {
        public DateRangeRequest? DateRange { get; set; }
        public string? Budget { get; set; }
        public string? TravelType { get; set; }
        public List<string>? Interests { get; set; }
        public LocationRequest? StartLocation { get; set; }
        public LocationRequest? EndLocation { get; set; }
        public Dictionary<string, DailyFreeTimeRequest>? DailyFreeTimes { get; set; }
    }

    public class DateRangeRequest
    {
        public string? Start { get; set; }
        public string? End { get; set; }
    }

    public class LocationRequest
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class DailyFreeTimeRequest
    {
        public string? Start { get; set; }
        public string? End { get; set; }
    }
}
