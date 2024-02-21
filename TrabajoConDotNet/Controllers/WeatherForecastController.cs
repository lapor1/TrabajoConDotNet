using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using TrabajoConDotNet.Data;
using TrabajoConDotNet.Models;
using static System.Net.WebRequestMethods;

namespace WebApplicationPrueba1.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private readonly DataBase _dbContext;

		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger, DataBase dbContext)
		{
			_logger = logger;
			_dbContext = dbContext;
		}

		[HttpGet(Name = "GetWeatherFromUser")]
		public async Task<string> Get(string username)
		{

			User user = await _dbContext.Users.Where(u => u.Username == username).FirstOrDefaultAsync();

			if (user == null)
			{
				return Results.NotFound().ToString();
			}
			else
			{
				var URL = $"https://api.open-meteo.com/v1/forecast?latitude={user.Latitude}&longitude={user.Longitude}&forecast_days=14&daily=apparent_temperature_max";
				var httpClient = new HttpClient();
				var response = await httpClient.GetAsync(URL);

				string responseString = await response.Content.ReadAsStringAsync();

				Weather dailyTemp = JsonConvert.DeserializeObject<Weather>(responseString);

				float hottestDayTemperature = dailyTemp.daily.apparent_temperature_max.Max();
				int hottestDayID = Array.IndexOf(dailyTemp.daily.apparent_temperature_max, hottestDayTemperature);
				string hottestDay = dailyTemp.daily.time[hottestDayID];

				Console.WriteLine($"El dia con mayor temperatura fue el {hottestDay} con una maxima de {hottestDayTemperature}°C");

				return responseString;
			}
		}
	}
}
