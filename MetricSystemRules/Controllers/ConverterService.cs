using System;
using MetricSystemRules.Interfaces;
using MetricSystemRules.Items;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MetricSystemRules.Controllers
{
    public class ConverterService : IConverterTemperature
    {
        private IValidator _validator;

        public ConverterService(IValidator validator)
        {
            _validator = validator;
        }

        public bool Convert(ITemperature input, ITemperature output)
        {
            var isValid = _validator.Validate(input, out var message);

            if (!isValid)
            {
                throw new Exception(message);
            }

            switch (input)
            {
                case CelsiusTemperature celsiusTemperature:
                    ConvertFromCelsius(celsiusTemperature, ref output);
                    return true;
                default:
                    return false;
            }
        }

        private void ConvertFromCelsius(CelsiusTemperature celsiusTemperature, ref ITemperature output)
        {
            switch (output)
            {
                case FahrenheitTemperature fahrenheitTemperature:
                    fahrenheitTemperature.SetValue(celsiusTemperature.Value * 1.8 + 32);
                    break;
            }
        }
    }
}