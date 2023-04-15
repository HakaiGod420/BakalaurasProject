using BoardTableInformationBackEnd.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using Moq;
using ServiceLayer.Interfaces;
using System.IO;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using static System.Net.Mime.MediaTypeNames;

namespace UnitTestGameBoardWeb.ControllersTests.FilesControllerTests
{
    public class UploadImageTests
    {
        private readonly Mock<IWebHostEnvironment> _webHostEnvironmentMock;
        private readonly FilesController _filesController;

        public UploadImageTests()
        {
            _webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            _filesController = new FilesController();
        }

        [Fact]
        public async Task UploadImage_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var tabletopTitle = "test-tabletop";
            var fileNames = new List<string> { "test1.jpg", "test2.jpg" };
            var images = new List<IFormFile> {
            new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("test")), 0, 0, fileNames[0], "test1.jpg"),
            new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("test")), 0, 0, fileNames[1], "test2.jpg"),
        };
            var createImageDto = new CreateImageDto { TabletopTitle = tabletopTitle, FileNames = fileNames, Images = images };

            _webHostEnvironmentMock.Setup(x => x.WebRootPath).Returns(Directory.GetCurrentDirectory());

            // Act
            var result = await _filesController.UploadImage(createImageDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<bool>(okResult.Value);
            Assert.True(returnValue);
        }

        [Fact]
        public async Task UploadImage_EmptyImages_ReturnsBadRequestResult()
        {
            // Arrange
            var createImageDto = new CreateImageDto { TabletopTitle = "test-tabletop", FileNames = new List<string>(), Images = new List<IFormFile>() };

            _webHostEnvironmentMock.Setup(x => x.WebRootPath).Returns(Directory.GetCurrentDirectory());

            // Act
            var result = await _filesController.UploadImage(createImageDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UploadImage_NullTabletopTitle_ReturnsBadRequestResult()
        {
            // Arrange
            var fileNames = new List<string> { "test1.jpg", "test2.jpg" };
            var images = new List<IFormFile> {
            new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("test")), 0, 0, fileNames[0], "test1.jpg"),
            new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("test")), 0, 0, fileNames[1], "test2.jpg"),
        };
            var createImageDto = new CreateImageDto { TabletopTitle = null, FileNames = fileNames, Images = images };

            _webHostEnvironmentMock.Setup(x => x.WebRootPath).Returns(Directory.GetCurrentDirectory());

            // Act
            var result = await _filesController.UploadImage(createImageDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UploadImage_EmptyTabletopTitle_ReturnsBadRequestResult()
        {
            // Arrange
            var fileNames = new List<string> { "test1.jpg", "test2.jpg" };
            var images = new List<IFormFile> {
            new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("test")), 0, 0, fileNames[0], "test1.jpg"),
            new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("test")), 0, 0, fileNames[1], "test2.jpg"),
        };
            var createImageDto = new CreateImageDto { TabletopTitle = "", FileNames = fileNames, Images = images };

            _webHostEnvironmentMock.Setup(x => x.WebRootPath).Returns(Directory.GetCurrentDirectory());

            // Act
            var result = await _filesController.UploadImage(createImageDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UploadImage_InvalidModelState_ReturnsBadRequestResult()
        {
            // Arrange
            _filesController.ModelState.AddModelError("key", "error message");

            // Act
            var result = await _filesController.UploadImage(new CreateImageDto());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UploadImage_SuccessfulUpload_ReturnsOkResult()
        {
            // Arrange
            var createImageDto = new CreateImageDto()
            {
                TabletopTitle = "testTitle",
                FileNames = new List<string>() { "test1.jpg", "test2.jpg" },
                Images = new List<IFormFile>()
                {
                    new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "test1", "test1.jpg"),
                    new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "test2", "test2.jpg")
                }
            };

            // Act
            var result = await _filesController.UploadImage(createImageDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
