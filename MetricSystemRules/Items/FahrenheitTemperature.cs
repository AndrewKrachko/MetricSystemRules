using MetricSystemRules.Enums;

namespace MetricSystemRules.Items
{
    public class FahrenheitTemperature : BaseTemperature
    {
        public FahrenheitTemperature(double value) : base(value, TemperatureUnitsEnum.F)
        {
        }
    }
}