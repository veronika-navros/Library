using Azure;
using Azure.Storage.Blobs;

namespace Library.BL.Services;

public class StorageService : IStorageService
{
    private readonly string _storageConnectionString;
    private const string ContainerName = "library-app-container";

    public StorageService(string storageConnectionString)
    {
        _storageConnectionString = storageConnectionString;
    }
    public async Task<string> Upload(string fileName, byte[] fileData)
    {
        var blobServiceClient = new BlobServiceClient(_storageConnectionString);
        var container = blobServiceClient.GetBlobContainerClient(ContainerName);
        var blob = container.GetBlobClient(fileName);
        await blob.UploadAsync(new BinaryData(fileData));
        return blob.Uri.ToString();
    }
}