using acciovac.Application.Common;
using MediatR;

namespace acciovac.Application.Behaviors.LocalExpirences.Commands.UpdateLocalExpirences
{
    public sealed record UpdateLocalExpirencesCommand(
        Guid Id,
        string LocationName,
        string Description,
        List<string> PhotoUrls = null
    ) : IRequest<Result<Guid>>
    {
        public UpdateLocalExpirencesCommand(Guid id, string locationName, string description)
            : this(id, locationName, description, new List<string>())
        {
        }
    }
}
