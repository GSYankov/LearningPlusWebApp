using Microsoft.AspNetCore.Http;

namespace LerningPlus.Web.Services.BlobService.Contract
{
    public interface IBlobService
    {
        string BlobUpload(IFormFile homework, string fileName);
    }
}
