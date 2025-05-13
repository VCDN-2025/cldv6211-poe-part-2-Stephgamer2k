using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Blobs;

namespace EventEaseDB.Controllers
{
    public class BlobStorage : Controller
    {
        private const string ContainerName = "myfiles";
        public const string SuccessMessageKey = "Successmessage";
        public const string ErrorMessageKey = "Errormessage";

        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _containerClient;

        public BlobStorage(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
            _containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            _containerClient.CreateIfNotExists();
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                var blobClient = _containerClient.GetBlobClient(file.FileName);
                await blobClient.UploadAsync(file.OpenReadStream(), true);
                TempData[SuccessMessageKey] = "File uploaded successfully.";
            }
            catch (Exception ex)
            {
                TempData[ErrorMessageKey] = "An error occurred while uploading the file: " + ex.Message;
                return View();
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Index()
        {
            var blobs = new List<string>();
            await foreach (var blobItem in _containerClient.GetBlobsAsync())
            {
                blobs.Add(blobItem.Name);
            }
            return View(blobs);
        }

        public async Task<IActionResult> Download(string fileName)
        {
            try
            {
                var blobClient = _containerClient.GetBlobClient(fileName);
                var memoryStream = new MemoryStream();
                await blobClient.DownloadToAsync(memoryStream);
                memoryStream.Position = 0;
                var contentType = blobClient.GetProperties().Value.ContentType;
                return File(memoryStream, contentType, fileName);
            }
            catch (Exception ex)
            {
                TempData[ErrorMessageKey] = "An error occurred while downloading the file: " + ex.Message;
                return RedirectToAction("Index");
            }
        }


        public async Task<IActionResult> Delete(string fileName)
        {
            try
            {
                var blobClient = _containerClient.GetBlobClient(fileName);
                await blobClient.DeleteIfExistsAsync();
                TempData[SuccessMessageKey] = "File deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData[ErrorMessageKey] = "An error occurred while deleting the file: " + ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
