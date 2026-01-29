namespace acciovac.API.GenerateItineraryRequest
{
    public class ItineraryResponse
    {
        public string Destination { get; set; }
        public int Days { get; set; }
        public List<ItineraryDayDto> DaysPlan { get; set; } = new();
    }

    public class ItineraryDayDto
    {
        public int Day { get; set; }
        public List<string> Activities { get; set; } = new();
    }

}
