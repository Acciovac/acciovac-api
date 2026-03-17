using acciovac.Application.Abstractions;
using acciovac.Application.Common;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace acciovac.Application.Behaviors.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserByIdDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserByIdDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user is null)
                return Result<UserByIdDto>.Failure("User not found.");

            var dto = new UserByIdDto(
                user.Id,
                user.FirebaseUid,
                user.Email,
                user.IsActive,
                user.CreatedAt,
                user.Roles.Select(r => r.Role.Name).ToList());

            return Result<UserByIdDto>.Success(dto);
        }
    }
}
