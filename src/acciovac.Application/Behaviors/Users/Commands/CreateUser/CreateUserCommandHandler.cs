using acciovac.Application.Abstractions;
using acciovac.Application.Common;
using acciovac.Domain.Entities;
using MediatR;

namespace acciovac.Application.Behaviors.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByFirebaseUidAsync(request.FirebaseUid);
            
            if (existingUser is not null)
            {
                return Result<Guid>.Failure("User with this Firebase UID already exists");
            }

            var user = new User(request.FirebaseUid, request.Email, request.CreatedBy);
            var role = string.IsNullOrWhiteSpace(request.Role) ? "User" : request.Role.Trim();
            var createdBy = string.IsNullOrWhiteSpace(request.CreatedBy) ? "system" : request.CreatedBy;

            await _userRepository.AddAsync(user, role, createdBy);

            return Result<Guid>.Success(user.Id);
        }
    }
}
