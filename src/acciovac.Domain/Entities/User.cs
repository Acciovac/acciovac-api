namespace AccioVac.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string ExternalIdentityId { get; set; } // Firebase/Auth0 ID
    public required string Email { get; set; }
    public required string FullName { get; set; }
    
    // Navigation Property
    public ICollection<Trip> Trips { get; set; } = new List<Trip>();
}