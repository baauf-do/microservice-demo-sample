using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Product.API.Models;

namespace Product.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //private static int count = 0;

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> Get()
        {
            // count++;
            // Console.WriteLine($"get... {count}");
            // //if (count % 2 == 1)
            // if (count < 3)
            // {
            //     Thread.Sleep(5000);
            // }

            var rng = new Random();
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            await Task.CompletedTask;
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post()
        {
            var rng = new Random();
            var result = "Datetime: " + DateTime.UtcNow.ToString() + "; Lucky number: " + rng.Next(0, 99);

            await Task.CompletedTask;
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> Put(string id)
        {
            await Task.CompletedTask;
            return Ok(id);
        }
    }
}
