using AccioVac.Application.Common.Interfaces;

namespace AccioVac.Infrastructure.Services;

// This service pretends to be the user we seeded above
public class DevCurrentUserService : ICurrentUserService
{
    public Guid UserId => Guid.Parse("11111111-1111-1111-1111-111111111111");
}