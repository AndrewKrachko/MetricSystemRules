using MetricSystemRules.Controllers;
using MetricSystemRules.Items;
using NUnit.Framework;

namespace MetricSystemRulesTests
{
    [TestFixture]
    public class ValidatorTests
    {
        [TestCase(double.MaxValue, true, "")]
        [TestCase(0.5 * (double.MaxValue - 273.16), true, "")]
        [TestCase(-273.16, true, "")]
        public void CheckMaxCelsiusTemperature(double cValue, bool expectedResult, string expectedMessage)
        {
            // Arrange
            var cT = new CelsiusTemperature(cValue);
            var sut = new ValidatorService();

            // Act 
            var result = sut.Validate(cT, out var message);

            // Assert
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(expectedMessage, message);
        }

        [TestCase(-273.16 - 1e-12, false, "Temperature must be not lower than")]
        [TestCase(double.NaN, false, "Temperature is not defined.")]
        public void CheckInvalidMinCelsiusTemperature(double cValue, bool expectedResult, string expectedMessage)
        {
            // Arrange
            var cT = new CelsiusTemperature(cValue);
            var sut = new ValidatorService();

            // Act 
            var result = sut.Validate(cT, out var message);

            // Assert
            Assert.AreEqual(expectedResult, result);
            Assert.IsTrue(message.Contains(expectedMessage));
        }
    }
}