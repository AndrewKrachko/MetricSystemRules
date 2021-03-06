﻿using System;
using System.IO;
using System.IO.Compression;
using System.IO.Pipes;
using System.Text;
using MetricSystemRules.Enums;
using MetricSystemRules.Interfaces;
using MetricSystemRules.Items;
using MetricSystemRules.Models;
using Microsoft.AspNetCore.Mvc;

namespace MetricSystemRules.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureCtoFController : Controller
    {
        private readonly IConverterTemperature _converterTemperature;

        public TemperatureCtoFController(IConverterTemperature converter)
        {
            _converterTemperature = converter;
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

        [HttpGet]
        [Route("ConvertCtoFToSpecificOutput")]
        public IActionResult ConvertCtoFToSpecificOutput(int value, OutputType outputType)
        {
            try
            {
                var cToFView = new TemperatureCtoFViewModel();
                ConvertCtoFImplementation(cToFView);

                byte[] data = new UTF8Encoding(true).GetBytes(
                    $"Celsius temperature: {cToFView.TemperatureMetric}\nFahrenheit temperature: {cToFView.TemperatureImperial}");

                switch (outputType)
                {
                    case OutputType.TxtFile:
                        return File(data, System.Net.Mime.MediaTypeNames.Text.Plain, "result.txt");
                    case OutputType.ZipFile:
                        return File(GenerateZipByteArray(data), System.Net.Mime.MediaTypeNames.Application.Zip,
                            "result.zip");
                    case OutputType.BinFile:
                        GenerateBinFile(cToFView);
                        return File(new FileStream("result.bin", FileMode.Open),
                            System.Net.Mime.MediaTypeNames.Application.Octet, "result.bin");
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

        private static void GenerateBinFile(TemperatureCtoFViewModel value)
        {
            if (System.IO.File.Exists("result.bin"))
            {
                System.IO.File.Delete("result.bin");
            }

            var fileStream = new FileStream("result.bin", FileMode.Create);
            using (var binaryStream = new BinaryWriter(fileStream, Encoding.ASCII))
            {
                binaryStream.Write("Celsius temperature: ");
                binaryStream.Write(value.TemperatureMetric);
                binaryStream.Write("\nFahrenheit temperature: ");
                binaryStream.Write(value.TemperatureImperial);
            }
        }

        private byte[] GenerateZipByteArray(byte[] data)
        {
            using (var archiveStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true))
                {
                    var zipArchiveEntry = archive.CreateEntry("result.txt", CompressionLevel.Optimal);
                    using (var zipStream = zipArchiveEntry.Open())
                        zipStream.Write(data, 0, data.Length);
                }

                return archiveStream.ToArray();
            }
        }

        [HttpGet]
        [Route("CtoFView")]
        public IActionResult CtoFView(double value = 0)
        {
            try
            {
                var viewModel = new TemperatureCtoFViewModel()
                {
                    TemperatureMetric = value
                };

                ConvertCtoFImplementation(viewModel);

                return View(viewModel);
            }
            catch (Exception e)
            {
                this.ModelState.AddModelError("", e.Message);
                return BadRequest(this.ModelState);
            }
        }
    }
}
