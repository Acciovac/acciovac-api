namespace acciovac.Application.Abstractions;

public interface IAzureBlobStorageService
{
    /// <summary>
    /// Uploads a file to Azure Blob Storage.
    /// </summary>
    /// <param name="containerName">Name of the blob container</param>
    /// <param name="fileName">Name of the file to save as</param>
    /// <param name="fileStream">Stream of the file content</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The URI of the uploaded blob</returns>
    Task<string> UploadFileAsync(string containerName, string fileName, Stream fileStream, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a file from Azure Blob Storage.
    /// </summary>
    /// <param name="containerName">Name of the blob container</param>
    /// <param name="fileName">Name of the file to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task DeleteFileAsync(string containerName, string fileName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Generates a SAS (Shared Access Signature) URI for a blob.
    /// </summary>
    /// <param name="containerName">Name of the blob container</param>
    /// <param name="fileName">Name of the file</param>
    /// <param name="expirationMinutes">How many minutes the SAS URI should be valid</param>
    /// <returns>The SAS URI</returns>
    string GenerateSasUri(string containerName, string fileName, int expirationMinutes = 60);
}
