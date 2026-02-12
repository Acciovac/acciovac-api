using acciovac.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace acciovac.Domain.Entities
{
    public class Rate : AuditableEntity<Guid>
    {
        public decimal BaseFare { get; private set; }
        public decimal PerKm { get; private set; }
        public decimal? NightMultiplier { get; private set; }
        public decimal? PeakMultiplier { get; private set; }

        private Rate() { }

        public Rate(
            decimal baseFare,
            decimal perKm,
            decimal nightMultiplier,
            decimal peakMultiplier)
        {
            Id = Guid.NewGuid();
            BaseFare = baseFare;
            PerKm = perKm;
            NightMultiplier = nightMultiplier;
            PeakMultiplier = peakMultiplier;
        }

        public void UpdateRates(
            decimal baseFare,
            decimal perKm,
            decimal nightMultiplier,
            decimal peakMultiplier)
        {
            BaseFare = baseFare;
            PerKm = perKm;
            NightMultiplier = nightMultiplier;
            PeakMultiplier = peakMultiplier;
        }
    }
}
