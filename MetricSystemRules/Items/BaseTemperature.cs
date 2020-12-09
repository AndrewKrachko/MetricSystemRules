using MetricSystemRules.Enums;
using MetricSystemRules.Interfaces;

namespace MetricSystemRules.Items
{
    public abstract class BaseTemperature : ITemperature
    {
        protected double _value;

        public double Value => _value;
        public TemperatureUnitsEnum Units { get; }

        protected BaseTemperature(double value, TemperatureUnitsEnum temperatureUnits)
        {
            _value = value;
            Units = temperatureUnits;
        }

        public void SetValue(double value)
        {
            _value = value;
        }
    }
}