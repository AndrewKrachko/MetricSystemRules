using System.ComponentModel.DataAnnotations;
using MetricSystemRules.Interfaces;

namespace MetricSystemRules.Models
{
    public class TemperatureCtoFViewModel
    {
        private IValidationDictionary _validationDictionary;

        [Required(ErrorMessage = "Set temperature.")]
        public double TemperatureMetric { get; set; }
        public double TemperatureImperial { get; set; }
    }
}