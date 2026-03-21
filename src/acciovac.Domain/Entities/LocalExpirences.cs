using System;
using System.Collections.Generic;
using acciovac.Domain.Common;

namespace acciovac.Domain.Entities;

public class LocalExpirences : AuditableEntity<Guid>
{
    public string expireancename { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public ICollection<Photo> Photos { get; private set; } = new List<Photo>();

    private LocalExpirences() { }

    public LocalExpirences(
        string locationName,
        string description,
        string createdBy = "system")
    {
        Id = Guid.NewGuid();
        expireancename = locationName;
        Description = description;
        SetCreated(createdBy);
    }

    public void Update(
        string locationName,
        string description,
        string modifiedBy = "system")
    {
        expireancename = locationName;
        Description = description;
        SetModified(modifiedBy);
    }

    public void AddPhoto(string photoUrl, int displayOrder = 0)
    {
        if (Photos.Count >= 5)
        {
            throw new InvalidOperationException("Cannot add more than 5 photos to a local experience.");
        }

        var photo = new Photo(Id, photoUrl, displayOrder);
        Photos.Add(photo);
    }

    public void RemovePhoto(Guid photoId)
    {
        var photo = Photos.FirstOrDefault(p => p.Id == photoId);
        if (photo != null)
        {
            Photos.Remove(photo);
        }
    }

    public void ClearPhotos()
    {
        Photos.Clear();
    }
}
