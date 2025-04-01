using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using VirginMediaTechExercise.Controllers;
using VirginMediaTechExercise.Models;
using VirginMediaTechExercise.Services;

namespace VirginMediaTechExerciseTests
{
    public class HomeControllerTests
    {
        private readonly HomeController _controller;

        private readonly Mock<ILogger<HomeController>> _mockLogger;
        private Mock<IFormFile> _mockFile;  
        private Mock<ISalesDataFileService> _mockSalesDataFileService;

        public HomeControllerTests()
        {
            _mockLogger = new Mock<ILogger<HomeController>>();

            _mockSalesDataFileService = new Mock<ISalesDataFileService>();
            _mockSalesDataFileService.Setup(x => x.ParseSalesData(It.IsAny<string>())).Returns([]);

            _mockFile = new Mock<IFormFile>();
            _mockFile.Setup(f => f.Length).Returns(1);
            _mockFile.Setup(f => f.OpenReadStream()).Returns(new MemoryStream());

            _controller = new HomeController(_mockLogger.Object, _mockSalesDataFileService.Object);            
        }

        [Fact]
        public void Index_ReturnsWithViewModel()
        {
            var result = _controller.Index();

            result.ShouldBeOfType<ViewResult>();
            (result as ViewResult).Model.ShouldBeOfType<SalesDataViewModel>();
        }

        [Fact]
        public void Upload_ReturnsIndexOnEmptyFile()
        {
            IFormFile file = null;
            var result = _controller.Upload(file);

            result.ShouldBeOfType<RedirectToActionResult>();
        }

        [Fact]
        public void Upload_ReturnsIndexWithData()
        {
            var result = _controller.Upload(_mockFile.Object);

            result.ShouldBeOfType<ViewResult>();
            (result as ViewResult).Model.ShouldBeOfType<SalesDataViewModel>();
        }

        [Fact]
        public void Upload_ReturnsIndexWithGarbageData()
        {
            _mockSalesDataFileService.Setup(x => x.ParseSalesData(It.IsAny<string>()))
                .Callback(() => throw new Exception("Some message"))
                .Returns([]);

            var result = _controller.Upload(_mockFile.Object);

            result.ShouldBeOfType<ViewResult>();
            (result as ViewResult).Model.ShouldBeOfType<ErrorViewModel>();
        }

        [Fact]
        public void Error_ReturnsErrorView()
        {
            // Act
            IActionResult result = _controller.Error();

            // Assert
            result.ShouldBeOfType<ViewResult>();
            (result as ViewResult).Model.ShouldBeOfType<ErrorViewModel>();
        }
    }
}