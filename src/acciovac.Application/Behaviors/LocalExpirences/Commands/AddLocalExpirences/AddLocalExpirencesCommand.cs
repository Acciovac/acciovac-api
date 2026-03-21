using acciovac.Application.Common;
using MediatR;

namespace acciovac.Application.Behaviors.LocalExpirences.Commands.AddLocalExpirences
{
    public sealed record AddLocalExpirencesCommand(
        string LocationName,
        string Description,
        List<string> PhotoUrls = null
    ) : IRequest<Result<Guid>>
    {
        public AddLocalExpirencesCommand(string locationName, string description)
            : this(locationName, description, new List<string>())
        {
        }
    }
}
