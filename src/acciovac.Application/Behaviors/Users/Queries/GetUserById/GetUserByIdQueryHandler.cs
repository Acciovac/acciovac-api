using acciovac.Application.Abstractions;
using acciovac.Application.Common;
using acciovac.Domain.Entities;
using MediatR;

namespace acciovac.Application.Behaviors.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<User>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user is null)
            {
                return Result<User>.Failure("User not found");
            }

            return Result<User>.Success(user);
        }
    }
}
