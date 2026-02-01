namespace AccioVac.Domain.Entities;

public class Trip
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; } // Foreign Key
    
    public required string Destination { get; set; }
    public required string UserInterests { get; set; } // "Hiking, Food, History"
    public int DurationDays { get; set; }
    
    // The "Magic" JSON Column for Postgres
    public string? AiItineraryJson { get; set; } 
    public bool IsGenerated { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User? User { get; set; }
}