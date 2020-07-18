using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MicroTester.Example.Controllers
{
    [ApiController]
    [Route("test")]
    public class TestController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("weather")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        [Route("step1")]
        public StepResponse Step1([Required, FromBody]StepRequest request)
        {
            return new StepResponse()
            {
                Date = DateTime.Now,
                Name = "Step1",
                ClientId = request.ClientId
            };
        }

        [HttpPost]
        [Route("step2")]
        public StepResponse Step2([Required, FromBody]StepRequest request)
        {
            return new StepResponse()
            {
                Date = DateTime.Now,
                Name = "Step2",
                ClientId = request.ClientId
            };
        }
    }
}
