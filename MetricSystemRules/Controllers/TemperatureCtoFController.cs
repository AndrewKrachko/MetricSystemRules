using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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

        // POST: api/TemperatureCtoF
        [HttpPost]
        [Route("ConvertCtoF")]
        public IActionResult ConvertCtoF([FromBody] TemperatureCtoFViewModel value)
        {
            try
            {
                var resultT = new FahrenheitTemperature(0);
                _converterTemperature.Convert(new CelsiusTemperature(value.TemperatureMetric), resultT);
                return Accepted(resultT.Value);
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


    }
}
