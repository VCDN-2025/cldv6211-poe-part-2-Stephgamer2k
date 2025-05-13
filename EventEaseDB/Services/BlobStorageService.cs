using Azure.Storage.Blobs;
using Azure.Storage.Sas;

namespace EventEaseDB.Services
{
    public class BlobStorageService
    {

        private readonly BlobServiceClient _blobserviceClient;
        private readonly string _containerName;

        public BlobStorageService(IConfiguration configuration)
        {
            _blobserviceClient = new BlobServiceClient(configuration["ConnectionStrings:StorageAccount"]);
            _containerName = configuration["ConnectionStrings:ContainerName"];
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
        {
            //Get the container client
            var containerClient = _blobserviceClient.GetBlobContainerClient(_containerName);

            //Get the blob client
            var blobClient = containerClient.GetBlobClient(fileName);

            //Upload the file
            await blobClient.UploadAsync(fileStream, overwrite: true);


            /*
            A shared access signature (SAS) token
            is a security token that grants limited access to specific resources or operations
            without requiring the sharing of the storage account key
             */

            //Generate the SAS token specific to our blob storage
            var sasToken = GenerateSasToken(blobClient);

            //Construct the image url with SAS token
            var imageUrlWithSas = $"{blobClient.Uri}?{sasToken}";//append the url with the SAS token to retrieve from azure storage

            //Return the image URL with SAS token
            return imageUrlWithSas;

        }

        private string GenerateSasToken(BlobClient blobClient)
        {
            //Create a SAS builder to specify permissions and expiry
            //this creates the token
            var sasBuilder = new BlobSasBuilder
            {
                BlobContainerName = _containerName,
                BlobName = blobClient.Name,
                ExpiresOn = DateTimeOffset.UtcNow.AddHours(1)//Token expiration time (adjust as needed)
            };
            //Set the permissions for the SAS token (read permissions)
            sasBuilder.SetPermissions(BlobSasPermissions.Read);

            //Generate the SAS token using the blob's URI
            var sasToken = blobClient.GenerateSasUri(sasBuilder).Query;

            return sasToken;
        }
    }
}
