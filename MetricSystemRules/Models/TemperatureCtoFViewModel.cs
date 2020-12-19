using System.ComponentModel.DataAnnotations;
using MetricSystemRules.Interfaces;

namespace MetricSystemRules.Models
{
    public class TemperatureCtoFViewModel
    {
        [Required(ErrorMessage = "Set temperature.")]
        public virtual double TemperatureMetric { get; set; }
        public virtual double TemperatureImperial { get; set; }
    }
}