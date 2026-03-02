using System;
using System.Collections.Generic;
using System.Text;

namespace acciovac.Domain.DTOs
{
    public record AiDayDto(
        int DayNumber,
        string Theme, // e.g., "Arrival in Colombo"
        List<AiActivityDto> Activities
    );

    public record AiActivityDto(
        string Time,         // e.g., "09:00 AM"
        string Category,     // e.g., "FOOD", "CULTURE", "STAY RECOMMENDATION", "TRANSIT"
        string Title,        // e.g., "Breakfast at Gallery Café"
        string Description,  // e.g., "Famous for its architecture..."
        string CostDetails,  // e.g., "Ticket: 1,500 LKR" or "$60 per night"
        double Lat,
        double Lon
    );
}
