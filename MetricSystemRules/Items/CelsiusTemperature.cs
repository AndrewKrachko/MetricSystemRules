using MetricSystemRules.Enums;

namespace MetricSystemRules.Items
{
    public class CelsiusTemperature : BaseTemperature
    {
        public CelsiusTemperature(double value) : base(value, TemperatureUnitsEnum.C)
        {
        }
    }
}