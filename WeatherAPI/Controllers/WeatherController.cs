using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeatherAPI.Services;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("{location}")]
        public async Task<IActionResult> GetWeather(string location)
        {
            try
            {
                var weatherData = await _weatherService.GetWeatherAsync(location);
                return Ok(weatherData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
