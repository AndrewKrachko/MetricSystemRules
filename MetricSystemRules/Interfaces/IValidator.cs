namespace MetricSystemRules.Interfaces
{
    public interface IValidator
    {
        bool Validate(IUnit unit, out string message);

    }
}