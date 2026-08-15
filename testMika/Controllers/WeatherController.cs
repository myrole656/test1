using Microsoft.AspNetCore.Mvc;
using testMika.Models;

namespace testMika.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private static readonly string[] Summaries =
            ["Freezing", "Chilly", "Mild", "Warm", "Scorching"];

        [HttpGet]
        public IEnumerable<WeatherForecast> Get() =>
            Enumerable.Range(1, 5).Select(Forecast);

        [HttpGet("{id:int}")]
        public ActionResult<WeatherForecast> GetById(int id)
        {
            if (id < 1 || id > 5) return NotFound();
            return Forecast(id);
        }

        private static WeatherForecast Forecast(int dayOffset) => new()
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(dayOffset)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        };
    }
}
