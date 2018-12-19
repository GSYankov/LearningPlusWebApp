using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace LerningPlus.Web.Services.BlobService.Contract
{
   public interface IBlobService
    {
        string BlobUpload(IFormFile homework);
    }
}
