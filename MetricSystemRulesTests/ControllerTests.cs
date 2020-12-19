using System;
using System.Net.Mime;
using MetricSystemRules.Controllers;
using MetricSystemRules.Enums;
using MetricSystemRules.Interfaces;
using MetricSystemRules.Items;
using MetricSystemRules.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace MetricSystemRulesTests
{
    [TestFixture]
    public class ControllerTests
    {
        Mock _tepmeratureCtoFViewModelMock = new Mock<TemperatureCtoFViewModel>(MockBehavior.Strict, new object[] { }).SetupAllProperties();

        [Test]
        public void ControllerConvertCtoFTest()
        {
            // Arrange
            var converterMock = new Mock<IConverterTemperature>();
            converterMock.Setup(s => s.Convert(It.IsAny<ITemperature>(), It.IsAny<ITemperature>()))
                .Returns(true);

            var controller = new TemperatureCtoFController(converterMock.Object);

            // Act
            var result = controller.ConvertCtoF((TemperatureCtoFViewModel)_tepmeratureCtoFViewModelMock.Object);

            // Assert
            Assert.AreEqual(202, ((AcceptedResult)result).StatusCode);
        }

        [Test]
        public void ControllerConvertCtoFNegativeTest()
        {
            // Arrange
            var converterMock = new Mock<IConverterTemperature>();
            converterMock.Setup(s => s.Convert(It.IsAny<ITemperature>(), It.IsAny<ITemperature>()))
                .Throws(new Exception(""));

            var controller = new TemperatureCtoFController(converterMock.Object);
            controller.ModelState.AddModelError("", "Unsupported type of unit:");

            // Act
            var result = controller.ConvertCtoF((TemperatureCtoFViewModel)_tepmeratureCtoFViewModelMock.Object);

            // Assert
            Assert.AreEqual(400, ((BadRequestObjectResult)result).StatusCode);
        }

        [Test]
        public void ControllerRedirectToItAcademyTest()
        {
            // Arrange
            var converterMock = new Mock<IConverterTemperature>();
            converterMock.Setup(s => s.Convert(It.IsAny<ITemperature>(), It.IsAny<ITemperature>()))
                .Returns(true);

            var controller = new TemperatureCtoFController(converterMock.Object);

            // Act
            var result = controller.RedirectToItAcademy();

            // Assert
            Assert.AreEqual("https://it-academy.by", ((RedirectResult)result).Url);
            Assert.AreEqual(false, ((RedirectResult)result).Permanent);
        }

        [Test]
        public void ControllerRedirectPermanentlyToItAcademyTest()
        {
            // Arrange
            var converterMock = new Mock<IConverterTemperature>();
            converterMock.Setup(s => s.Convert(It.IsAny<ITemperature>(), It.IsAny<ITemperature>()))
                .Returns(true);

            var controller = new TemperatureCtoFController(converterMock.Object);

            // Act
            var result = controller.RedirectPermanentlyToItAcademy();

            // Assert
            Assert.AreEqual("https://it-academy.by", ((RedirectResult)result).Url);
            Assert.AreEqual(true, ((RedirectResult)result).Permanent);
        }


        [TestCase(OutputType.TxtFile, MediaTypeNames.Text.Plain, "result.txt")]
        [TestCase(OutputType.ZipFile, MediaTypeNames.Application.Zip, "result.zip")]
        public void ControllerConvertCtoFToSpecificOutputTest(OutputType type, string resultMediaTypeNames, string fileName)
        {
            // Arrange
            var converterMock = new Mock<IConverterTemperature>();
            converterMock.Setup(s => s.Convert(It.IsAny<ITemperature>(), It.IsAny<ITemperature>()))
                .Returns(true);

            var controller = new TemperatureCtoFController(converterMock.Object);

            // Act
            var result = controller.ConvertCtoFToSpecificOutput(0, type);

            // Assert
            Assert.AreEqual(resultMediaTypeNames, ((FileContentResult)result).ContentType);
            Assert.AreEqual(fileName, ((FileContentResult)result).FileDownloadName);
        }

        [TestCase(OutputType.BinFile, MediaTypeNames.Application.Octet, "result.bin")]
        public void ControllerConvertCtoFToSpecificOutputTxtTest(OutputType type, string resultMediaTypeNames, string fileName)
        {
            // Arrange
            var converterMock = new Mock<IConverterTemperature>();
            converterMock.Setup(s => s.Convert(It.IsAny<ITemperature>(), It.IsAny<ITemperature>()))
                .Returns(true);

            var controller = new TemperatureCtoFController(converterMock.Object);

            // Act
            var result = controller.ConvertCtoFToSpecificOutput(0, type);

            // Assert
            Assert.AreEqual(resultMediaTypeNames, ((FileStreamResult)result).ContentType);
            Assert.AreEqual(fileName, ((FileStreamResult)result).FileDownloadName);
        }

        [Test]
        public void ControllerConvertCtoFToSpecificOutputNegativeTest()
        {
            // Arrange
            var converterMock = new Mock<IConverterTemperature>();
            converterMock.Setup(s => s.Convert(It.IsAny<ITemperature>(), It.IsAny<ITemperature>()))
                .Throws(new Exception(""));

            var controller = new TemperatureCtoFController(converterMock.Object);
            controller.ModelState.AddModelError("", "Unsupported type of unit:");

            // Act
            var result = controller.ConvertCtoFToSpecificOutput(-1000, OutputType.TxtFile);

            // Assert
            Assert.AreEqual(400, ((BadRequestObjectResult)result).StatusCode);
        }

        [Test]
        public void ControllerCtoFViewTest()
        {
            // Arrange
            var converterMock = new Mock<IConverterTemperature>();
            converterMock.Setup(s => s.Convert(It.IsAny<ITemperature>(), It.IsAny<ITemperature>()))
                .Returns(true);

            var controller = new TemperatureCtoFController(converterMock.Object);

            // Act
            var result = controller.CtoFView(0);

            // Assert
            Assert.AreEqual(0, ((TemperatureCtoFViewModel)((ViewResult)result).Model).TemperatureImperial);
        }
    }
}