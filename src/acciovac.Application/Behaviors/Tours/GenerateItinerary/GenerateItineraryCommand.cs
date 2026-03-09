using acciovac.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace acciovac.Application.Behaviors.Tours.GenerateItinerary
{
    public sealed record GenerateItineraryCommand(
        string FirebaseUid,
        DateTime StartDate,
        DateTime EndDate,
        decimal DailyBudget,
        List<string> Interests,
        TimeSpan AvailableFrom,
        TimeSpan AvailableTo
    ) : IRequest<Result<Guid>>;
}
