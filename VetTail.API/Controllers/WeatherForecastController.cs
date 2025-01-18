using Microsoft.AspNetCore.Mvc;
using VetTail.Application.Common.DTOs.Clients;
using VetTail.Application.Common.DTOs.Wrappers;
using VetTail.Application.Common.Interfaces;

namespace VetTail.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IClientService clientService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IClientService clientService)
        {
            _logger = logger;
            this.clientService = clientService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpGet("/clients")]
        public async Task<PaginatedList<ClientBriefDto>> GetClients()
        {
            return await this.clientService.GetAllPaginatedAsync(1, 10);
        }
    }
}
