namespace acciovac.Application.Abstractions
{
    public interface IUnsplashImageService
    {
        Task<string> GetImageUrlAsync(string location, CancellationToken cancellationToken = default);
    }
}
