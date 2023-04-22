using AutoMapper;
using ChatServer.Api.ViewModels.User;
using ChatServer.Api.WebConfig;
using ChatServer.Data.Entities;
using ChatServer.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatServer.Api.Controllers
{
	[Route("[controller]")]
	public class WeatherForecastController : AppControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

		private readonly ILogger<WeatherForecastController> _logger;
		public WeatherForecastController(
			ILogger<WeatherForecastController> logger,
			IMapper mapper) : base(mapper)
		{
			_logger = logger;
		}

		[HttpGet(Name = "GetWeatherForecast")]
		public IEnumerable<WeatherForecast> Get()
		{
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = Summaries[Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}
	}
}