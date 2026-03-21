namespace acciovac.Domain.DTOs;

public class PhotoDto
{
    public Guid Id { get; set; }
    public string PhotoUrl { get; set; } = null!;
    public int DisplayOrder { get; set; }
}

public class CreateLocalExpirenceDto
{
    public string LocationName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<string> PhotoUrls { get; set; } = new();
}

public class UpdateLocalExpirenceDto
{
    public string LocationName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<string> PhotoUrls { get; set; } = new();
}

public class LocalExpirenceDto
{
    public Guid Id { get; set; }
    public string LocationName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<PhotoDto> Photos { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}
