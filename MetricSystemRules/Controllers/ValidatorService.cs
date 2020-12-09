using System;
using MetricSystemRules.Interfaces;
using MetricSystemRules.Items;

namespace MetricSystemRules.Controllers
{
    public class ValidatorService : IValidator
    {
        private const double _delta = 1e-13;
        private const double _celsiusLowLimit = -273.16;


        public bool Validate(IUnit unit, out string message)
        {
            switch (unit)
            {
                case CelsiusTemperature celsiusTemperature:
                    return Verify(celsiusTemperature, out message);
                    break;
                default:
                    message = $"Unsupported type of unit: {unit.GetType()}";
                    return false;
            }
        }

        private bool Verify(CelsiusTemperature item, out string message)
        {
            if (item == null ||
                Double.IsNaN(item.Value))
            {
                message = $"Temperature is not defined.";
                return false;
            }
            else if(item.Value > (_celsiusLowLimit - _delta))
            {
                message = "";
                return true;
            }

            message = $"Temperature must be not lower than {_celsiusLowLimit} °C.";
            return false;
        }
    }
}