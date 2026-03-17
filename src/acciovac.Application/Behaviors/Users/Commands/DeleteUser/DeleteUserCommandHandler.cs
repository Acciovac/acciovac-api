using acciovac.Application.Abstractions;
using acciovac.Application.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace acciovac.Application.Behaviors.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<Guid>>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<Guid>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user is null)
                return Result<Guid>.Failure("User not found.");

            var modifiedBy = string.IsNullOrWhiteSpace(request.ModifiedBy) ? "system" : request.ModifiedBy;
            user.Deactivate(modifiedBy);

            await _userRepository.DeleteAsync(user);

            return Result<Guid>.Success(user.Id);
        }
    }
}
