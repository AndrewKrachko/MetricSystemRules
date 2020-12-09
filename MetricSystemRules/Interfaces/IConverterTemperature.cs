namespace MetricSystemRules.Interfaces
{
    public interface IConverterTemperature
    {
        bool Convert(ITemperature input, ITemperature output);
    }
}