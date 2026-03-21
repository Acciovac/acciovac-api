using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace acciovac.Domain.DTOs
{
    public class AiItineraryResponseDto
    {
        [JsonPropertyName("itineraryPlan")]
        public AiItineraryPlanDto ItineraryPlan { get; set; } = new();
    }

    public class AiItineraryPlanDto
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("tripOverview")]
        public TripOverviewDto TripOverview { get; set; } = new();

        [JsonPropertyName("dailySchedule")]
        public List<AiDayDto> DailySchedule { get; set; } = new();

        [JsonPropertyName("budgetBreakdown")]
        public BudgetBreakdownDto BudgetBreakdown { get; set; } = new();
    }

    public class TripOverviewDto
    {
        [JsonPropertyName("traveler")]
        public string Traveler { get; set; } = string.Empty;

        [JsonPropertyName("budget")]
        public decimal Budget { get; set; }

        [JsonPropertyName("theme")]
        public string Theme { get; set; } = string.Empty;

        [JsonPropertyName("locations")]
        public List<string> Locations { get; set; } = new();
    }

    public class AiDayDto
    {
        [JsonPropertyName("date")]
        public string Date { get; set; } = string.Empty;

        [JsonPropertyName("location")]
        public string Location { get; set; } = string.Empty;

        [JsonPropertyName("imageLink")]
        public string ImageLink { get; set; } = string.Empty;

        [JsonPropertyName("activities")]
        public List<AiActivityDto> Activities { get; set; } = new();
    }

    public class AiActivityDto
    {
        [JsonPropertyName("startTime")]
        public string StartTime { get; set; } = string.Empty;

        [JsonPropertyName("endTime")]
        public string EndTime { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("visitLocation")]
        public string VisitLocation { get; set; } = string.Empty;

        [JsonPropertyName("coordinates")]
        public CoordinatesDto Coordinates { get; set; } = new();

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
    }

    public class CoordinatesDto
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
    }

    public class BudgetBreakdownDto
    {
        [JsonPropertyName("currency")]
        public string Currency { get; set; } = string.Empty;

        [JsonPropertyName("totalEstimated")]
        public decimal TotalEstimated { get; set; }

        [JsonPropertyName("categories")]
        public BudgetCategoriesDto Categories { get; set; } = new();
    }

    public class BudgetCategoriesDto
    {
        [JsonPropertyName("transport")]
        public decimal Transport { get; set; }

        [JsonPropertyName("accommodation")]
        public decimal Accommodation { get; set; }

        [JsonPropertyName("activities")]
        public decimal Activities { get; set; }

        [JsonPropertyName("foodAndDrinks")]
        public decimal FoodAndDrinks { get; set; }

        [JsonPropertyName("miscellaneous")]
        public decimal Miscellaneous { get; set; }
    }
}
