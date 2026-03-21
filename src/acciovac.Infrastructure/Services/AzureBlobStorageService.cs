using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using acciovac.Application.Abstractions;
using Microsoft.Extensions.Configuration;

namespace acciovac.Infrastructure.Services;

public class AzureBlobStorageService : IAzureBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly BlobContainerClient? _sasContainerClient;

    public AzureBlobStorageService(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("AzureBlob");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Azure Blob Storage connection string is not configured.");
        }

        if (Uri.TryCreate(connectionString, UriKind.Absolute, out var sasUri) &&
            (sasUri.Scheme == Uri.UriSchemeHttp || sasUri.Scheme == Uri.UriSchemeHttps))
        {
            _sasContainerClient = new BlobContainerClient(sasUri);
            _blobServiceClient = null!;
        }
        else
        {
            _blobServiceClient = new BlobServiceClient(connectionString);
        }
    }

    public async Task<string> UploadFileAsync(
        string containerName, 
        string fileName, 
        Stream fileStream, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var containerClient = _sasContainerClient ?? _blobServiceClient.GetBlobContainerClient(containerName);

            if (_sasContainerClient is null)
            {
                // Create container if it doesn't exist
                await containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);
            }

            var blobClient = containerClient.GetBlobClient(fileName);
            
            // Upload the file
            await blobClient.UploadAsync(fileStream, overwrite: true, cancellationToken: cancellationToken);

            return blobClient.Uri.ToString();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to upload file '{fileName}' to container '{containerName}'.", ex);
        }
    }

    public async Task DeleteFileAsync(
        string containerName, 
        string fileName, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var containerClient = _sasContainerClient ?? _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to delete file '{fileName}' from container '{containerName}'.", ex);
        }
    }

    public string GenerateSasUri(
        string containerName, 
        string fileName, 
        int expirationMinutes = 60)
    {
        try
        {
            var containerClient = _sasContainerClient ?? _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            if (_sasContainerClient is not null)
            {
                return blobClient.Uri.ToString();
            }

            // Create SAS builder
            var sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = containerName,
                BlobName = fileName,
                Resource = "b",
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(expirationMinutes)
            };

            // Set permissions (read-only)
            sasBuilder.SetPermissions(BlobSasPermissions.Read);

            // Generate SAS URI
            var sasUri = blobClient.GenerateSasUri(sasBuilder);

            return sasUri.ToString();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to generate SAS URI for '{fileName}'.", ex);
        }
    }
}
