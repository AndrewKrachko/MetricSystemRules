namespace MetricSystemRules.Interfaces
{
    public interface IValidationDictionary
    {
        bool IsValid { get; }
        void AddError(string error);
    }
}