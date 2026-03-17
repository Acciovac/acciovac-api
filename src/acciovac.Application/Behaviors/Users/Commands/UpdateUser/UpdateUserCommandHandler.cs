using acciovac.Application.Abstractions;
using acciovac.Application.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace acciovac.Application.Behaviors.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<Guid>>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<Guid>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user is null)
                return Result<Guid>.Failure("User not found.");

            var modifiedBy = string.IsNullOrWhiteSpace(request.ModifiedBy) ? "system" : request.ModifiedBy;
            user.Update(request.Email, modifiedBy);

            await _userRepository.UpdateAsync(user);

            return Result<Guid>.Success(user.Id);
        }
    }
}
