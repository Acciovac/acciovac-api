using acciovac.Application.Abstractions;
using acciovac.Application.Common;
using MediatR;

namespace acciovac.Application.Behaviors.LocalExpirences.Commands.UpdateLocalExpirences
{
    public class UpdateLocalExpirencesCommandHandler : IRequestHandler<UpdateLocalExpirencesCommand, Result<Guid>>
    {
        private readonly ILocalExpirences _repository;

        public UpdateLocalExpirencesCommandHandler(ILocalExpirences repository)
        {
            _repository = repository;
        }

        public async Task<Result<Guid>> Handle(UpdateLocalExpirencesCommand request, CancellationToken cancellationToken)
        {
            var localExperience = await _repository.GetByIdAsync(request.Id);

            if (localExperience is null)
            {
                return Result<Guid>.Failure("Local experience not found");
            }

            localExperience.Update(
                request.LocationName,
                request.Description,
                "system"
            );

            // Update photos if provided
            if (request.PhotoUrls != null && request.PhotoUrls.Any())
            {
                localExperience.ClearPhotos();
                int displayOrder = 0;
                foreach (var photoUrl in request.PhotoUrls.Take(5)) // Limit to 5 photos
                {
                    localExperience.AddPhoto(photoUrl, displayOrder++);
                }
            }

            await _repository.UpdateAsync(localExperience);

            return Result<Guid>.Success(localExperience.Id);
        }
    }
}
