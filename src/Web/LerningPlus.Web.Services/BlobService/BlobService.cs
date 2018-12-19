using LerningPlus.Web.Services.BlobService.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Threading.Tasks;

namespace LerningPlus.Web.Services.BlobService
{
    public class BlobService : IBlobService
    {
        private readonly IConfiguration configuration;

        public BlobService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string BlobUpload(IFormFile file)
        {
            string connectionString = this.configuration["ConnectionStrings:AzureBlobConnection"];
            CloudStorageAccount cloudStorage = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient cloudBlobClient = cloudStorage.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference("lpblobs");
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(file.FileName);
            blockBlob.Properties.ContentType = file.ContentType;
            using (var stream = file.OpenReadStream())
            {
                Task.Run(() => blockBlob.UploadFromStreamAsync(stream)).Wait();
            }

            return file.FileName;
        }
    }
}

