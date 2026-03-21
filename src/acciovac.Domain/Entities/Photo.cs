using System;
using acciovac.Domain.Common;

namespace acciovac.Domain.Entities;

public class Photo : Entity<Guid>
{
    public Guid LocalExpirencesId { get; private set; }
    public string PhotoUrl { get; private set; } = null!;
    public int DisplayOrder { get; private set; }

    private Photo() { }

    public Photo(Guid localExpirencesId, string photoUrl, int displayOrder)
    {
        Id = Guid.NewGuid();
        LocalExpirencesId = localExpirencesId;
        PhotoUrl = photoUrl;
        DisplayOrder = displayOrder;
    }

    public void UpdatePhotoUrl(string photoUrl)
    {
        PhotoUrl = photoUrl;
    }

    public void UpdateDisplayOrder(int displayOrder)
    {
        DisplayOrder = displayOrder;
    }
}
