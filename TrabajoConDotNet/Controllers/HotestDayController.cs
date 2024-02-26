using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

//using Newtonsoft.Json;
using System.Security.Claims;
using System.Text.Json;
using TrabajoConDotNet.Data;
using TrabajoConDotNet.Models;
using static System.Net.WebRequestMethods;

namespace WebApplicationPrueba1.Controllers
{
	[ApiController]
	[Route("hotest-day")]
	public class HotestDayController : ControllerBase
	{
		private readonly DataBase _dbContext;

		private readonly ILogger<HotestDayController> _logger;

		public HotestDayController(ILogger<HotestDayController> logger, DataBase dbContext)
		{
			_logger = logger;
			_dbContext = dbContext;
		}

		[HttpGet("{Username}")]
		public async Task<IActionResult> Get(string Username)
		{

			User user = await _dbContext.Users.Where(u => u.Username == Username).FirstOrDefaultAsync();

			if (user == null)
			{
				return NotFound();
			}
			else
			{
				var URL = $"https://api.open-meteo.com/v1/forecast?latitude={user.Latitude}&longitude={user.Longitude}&forecast_days=14&daily=apparent_temperature_max";
				var httpClient = new HttpClient();
				var response = await httpClient.GetAsync(URL);

				string responseString = await response.Content.ReadAsStringAsync();

				Weather dailyTemp = JsonSerializer.Deserialize<Weather>(responseString);

				var hottestDayAndTemperature = dailyTemp.Daily.Time
					.Zip(dailyTemp.Daily.ApparentTemperatureMax, (d, t) => $"{d} {t}")
					.OrderByDescending(dt => float.Parse(dt.Split(" ")[1]))
					.First()
					.Split(" ");

				var weatherResponse = new
				{
					Date = hottestDayAndTemperature[0],
					MaxTemperature = hottestDayAndTemperature[1]
				};

				_logger.LogInformation($"El dia con mayor temperatura será el {hottestDayAndTemperature[0]} con una maxima de {hottestDayAndTemperature[1]}°C");

				return Ok(weatherResponse);
			}
		}

	}
}
