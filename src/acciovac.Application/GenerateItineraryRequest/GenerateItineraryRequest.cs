namespace acciovac.API.GenerateItineraryRequest
{
    public class GenerateItineraryRequest
    {
        public string Destination { get; set; }
        public int Days { get; set; }
        public List<string> Interests { get; set; } = new();
    }

}
