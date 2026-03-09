using acciovac.Application.Common;
using MediatR;

namespace acciovac.Application.Behaviors.Tours.GenerateItinerary
{
    public sealed class GenerateItineraryCommandHandler : IRequestHandler<GenerateItineraryCommand, Result<Guid>>
    {
        public Task<Result<Guid>> Handle(GenerateItineraryCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
