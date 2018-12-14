using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace LerningPlus.Web.Services.BlobService
{
    public class BlobService
    {
        private readonly IConfiguration configuration;

        public BlobService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void BlobUpload()
        {
            string connectionString = this.configuration["ConnectionStrings:AzureBlobConnection"];
            CloudStorageAccount cloudStorage = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient cloudBlobClient = cloudStorage.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference("webappblobs");
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference("csBlobTest.jpg");
            blockBlob.Properties.ContentType = "image/png";
            using (var fileStream = System.IO.File.OpenRead(@"C:\Users\gyankov2\Desktop\AzureTest.jpg"))
            {
                Task.Run(() => blockBlob.UploadFromStreamAsync(fileStream)).Wait();
            }
        }
    }
}

