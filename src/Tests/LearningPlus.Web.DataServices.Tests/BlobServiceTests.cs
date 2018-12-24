using LerningPlus.Web.Services.BlobService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IO;
using Xunit;

namespace LearningPlus.Web.DataServices.Tests
{
    public class BlobServiceTests
    {

        [Fact]
        public void UplodedFileShouldUploadAFileAndReturnString()
        {
            //Arrange
            var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            IConfiguration configuration = TestsPrerequisites.GetConfiguration();

            var sut = new BlobService(configuration);
            var file = fileMock.Object;

            //Act
            var result = sut.BlobUpload(file, "test.file");

            //Assert
            Assert.IsType<string>(result);
            Assert.Equal("test.file", result);
        }
    }
}
