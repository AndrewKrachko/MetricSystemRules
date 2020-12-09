using System;
using MetricSystemRules.Controllers;
using MetricSystemRules.Interfaces;
using MetricSystemRules.Items;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class ConverterTests
    {
        [Test]
        public void ConvertValidCtoFTest()
        {
            // Arrange
            var cT = new CelsiusTemperature(0);
            var fT = new FahrenheitTemperature(0);
            var validatorMock = new Mock<IValidator>();
            validatorMock.Setup(s => s.Validate(It.IsAny<CelsiusTemperature>(), out It.Ref<string>.IsAny))
                .Returns(true);
            var converter = new ConverterService(validatorMock.Object);

            // Act
            converter.Convert(cT, fT);

            // Assert
            Assert.AreEqual(32, fT.Value);
        }

        [Test]
        public void ConvertInvalidCtoFTest()
        {
            // Arrange
            var cT = new CelsiusTemperature(0);
            var fT = new FahrenheitTemperature(0);
            var validatorMock = new Mock<IValidator>();
            validatorMock.Setup(s => s.Validate(It.IsAny<ITemperature>(), out It.Ref<string>.IsAny))
                .Returns(false);
            var converter = new ConverterService(validatorMock.Object);

            // Act

            // Assert
            Assert.Throws<Exception>(() => converter.Convert(cT, fT));
        }
    }
}