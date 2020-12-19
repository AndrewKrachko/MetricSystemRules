using System;
using MetricSystemRules.Controllers;
using MetricSystemRules.Interfaces;
using MetricSystemRules.Items;
using Moq;
using NUnit.Framework;

namespace MetricSystemRulesTests
{
    public class ConverterTests
    {
        [TestCase(-20, -4)]
        [TestCase(0, 32)]
        [TestCase(40, 104)]
        public void ConvertValidCtoFTest(double cValue, double fValue)
        {
            // Arrange
            var cT = new CelsiusTemperature(cValue);
            var fT = new FahrenheitTemperature(0);
            var validatorMock = new Mock<IValidator>();
            validatorMock.Setup(s => s.Validate(It.IsAny<CelsiusTemperature>(), out It.Ref<string>.IsAny))
                .Returns(true);
            var converter = new ConverterService(validatorMock.Object);

            // Act
            converter.Convert(cT, fT);

            // Assert
            Assert.AreEqual(fValue, fT.Value);
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