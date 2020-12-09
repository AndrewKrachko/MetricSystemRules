using MetricSystemRules.Controllers;
using MetricSystemRules.Items;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ValidatorTests
    {
        [Test]
        public void CheckMaxCelsiusTemperature()
        {
            // Arrange
            var cT = new CelsiusTemperature(double.MaxValue);
            var sut = new ValidatorService();

            // Act 
            var result = sut.Validate(cT, out string message);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual("", message);
        }

        [Test]
        public void CheckMidCelsiusTemperature()
        {
            // Arrange
            var cT = new CelsiusTemperature(0.5 * (double.MaxValue - 273.16));
            var sut = new ValidatorService();

            // Act 
            var result = sut.Validate(cT, out string message);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual("", message);
        }

        [Test]
        public void CheckMinCelsiusTemperature()
        {
            // Arrange
            var cT = new CelsiusTemperature(-273.16);
            var sut = new ValidatorService();

            // Act 
            var result = sut.Validate(cT, out string message);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual("", message);
        }

        [Test]
        public void CheckInvalidMinCelsiusTemperature()
        {
            // Arrange
            var cT = new CelsiusTemperature(-273.16 - 1e-12);
            var sut = new ValidatorService();

            // Act 
            var result = sut.Validate(cT, out string message);

            // Assert
            Assert.AreEqual(false, result);
            Assert.IsTrue(message.Contains("Temperature must be not lower than"));
        }

        [Test]
        public void CheckNullCelsiusTemperature()
        {
            // Arrange
            var cT = new CelsiusTemperature(double.NaN);
            var sut = new ValidatorService();

            // Act 
            var result = sut.Validate(cT, out string message);

            // Assert
            Assert.AreEqual(false, result);
            Assert.IsTrue(message.Contains("Temperature is not defined."));
        }

    }
}