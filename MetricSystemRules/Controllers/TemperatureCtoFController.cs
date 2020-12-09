using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MetricSystemRules.Enums;
using MetricSystemRules.Interfaces;
using MetricSystemRules.Items;
using MetricSystemRules.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MetricSystemRules.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureCtoFController : ControllerBase
    {
        private IConverterTemperature _converterTemperature;

        public TemperatureCtoFController()
        {
            _converterTemperature = new ConverterService(new ValidatorService());
        }

        private void ConvertCtoFImplementation(TemperatureCtoFViewModel value)
        {
            var resultT = new FahrenheitTemperature(0);
            _converterTemperature.Convert(new CelsiusTemperature(value.TemperatureMetric), resultT);
            value.TemperatureImperial = resultT.Value;
        }

        // POST: api/TemperatureCtoF
        [HttpPost]
        [Route("ConvertCtoF")]
        public IActionResult ConvertCtoF([FromBody] TemperatureCtoFViewModel value)
        {
            try
            {
                ConvertCtoFImplementation(value);
                return Accepted(value.TemperatureImperial);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("", e.Message);
                return BadRequest(this.ModelState);
            }
        }

        [HttpGet]
        [Route("RedirectToItAcademy")]
        public IActionResult RedirectToItAcademy()
        {
            return Redirect("https://it-academy.by");
        }

        [HttpGet]
        [Route("RedirectPermanentlyToItAcademy")]
        public IActionResult RedirectPermanentlyToItAcademy()
        {
            return RedirectPermanent("https://it-academy.by");
        }

        [HttpPost]
        [Route("ConvertCtoFToSpecificOutput")]
        public IActionResult ConvertCtoFToSpecificOutput([FromBody] TemperatureCtoFViewModel value, OutputType outputType)
        {

            try
            {
                ConvertCtoFImplementation(value);

                byte[] data = new UTF8Encoding(true).GetBytes($"Celsius temperature is {value.TemperatureMetric}/nThat corresponds to {value.TemperatureImperial} in Fahrenheit scale");

                switch (outputType)
                {
                    case OutputType.Int:
                        return Accepted((int)value.TemperatureImperial);
                    case OutputType.TxtFile:

                        var url = CreateTxtFile(data);
                        return File(data, System.Net.Mime.MediaTypeNames.Text.Plain);
                    default:
                        this.ModelState.AddModelError("", "Unknown OutputType");
                        return BadRequest(this.ModelState);
                }
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("", e.Message);
                return BadRequest(this.ModelState);
            }

        }

        private string CreateTxtFile(byte[] data)
        {
            var fileStream = System.IO.File.Create("result.txt");
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();

            return fileStream.Name;
        }
    }
}
