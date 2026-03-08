using acciovac.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace acciovac.Domain.Entities
{
    public class Review : AuditableEntity<Guid>
    {
        public Guid UserId { get; private set; }
        public int Rating { get; private set; }
        public string Comment { get; private set; }
        public bool IsVerified {get; private set;}

        private Review() { }

        public Review(
            Guid userId,
            int rating,
            string comment,
            string createdBy = "system")
        {
            Id = Guid.NewGuid();
            Rating = rating;
            Comment = comment;
            IsVerified = false;
            SetCreated(createdBy);
        }

        // public void UpdateRates(
        //     decimal baseFare,
        //     decimal perKm,
        //     decimal nightMultiplier,
        //     decimal peakMultiplier,
        //     string modifiedBy = "system")
        // {
        //     BaseFare = baseFare;
        //     PerKm = perKm;
        //     NightMultiplier = nightMultiplier;
        //     PeakMultiplier = peakMultiplier;
        //     SetModified(modifiedBy);
        // }

        public void UpdateReview (int rating, string comment, string modifiedBy)
        {
            Rating = rating;
            Comment = comment;
            SetModified(modifiedBy);
        }

        public void Verify(string modifiedBy)
        {
            IsVerified = true;
            SetModified(modifiedBy);
        }

    }
}
