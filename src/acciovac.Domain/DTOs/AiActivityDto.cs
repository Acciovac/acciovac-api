using System.Collections.Generic;

namespace acciovac.Domain.DTOs
{
    public class AiDayDto
    {
        public string Date { get; set; } = string.Empty;
        public string BaseLocation { get; set; } = string.Empty;
        public List<AiActivityDto> Activities { get; set; } = new();
        public string OvernightStay { get; set; } = string.Empty;
        public string BookingLink { get; set; } = string.Empty;
    }

    public class AiActivityDto
    {
        public string Time { get; set; } = string.Empty;
        public string Activity { get; set; } = string.Empty;
        public string PlaceName { get; set; } = string.Empty;
        public string TravelTimeFromPrevious { get; set; } = string.Empty;
        public string Weather { get; set; } = string.Empty;
    }
}
