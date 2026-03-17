using acciovac.Application.Abstractions;
using acciovac.Application.Common;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace acciovac.Application.Behaviors.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<IReadOnlyList<UserDto>>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<IReadOnlyList<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();

            var dtos = users.Select(u => new UserDto(
                u.Id,
                u.FirebaseUid,
                u.Email,
                u.IsActive,
                u.CreatedAt,
                u.Roles.Select(r => r.Role.Name).ToList()
            )).ToList();

            return Result<IReadOnlyList<UserDto>>.Success(dtos);
        }
    }
}
