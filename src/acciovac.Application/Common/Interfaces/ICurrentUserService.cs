namespace AccioVac.Application.Common.Interfaces;

public interface ICurrentUserService
{
    Guid UserId { get; } // Will return hardcoded ID in Dev, Real ID in Prod
}