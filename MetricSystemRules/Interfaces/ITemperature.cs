using MetricSystemRules.Enums;

namespace MetricSystemRules.Interfaces
{
    public interface ITemperature : IUnit
    {
        double Value { get; }
        TemperatureUnitsEnum Units { get; }

        void SetValue(double value);
    }
}