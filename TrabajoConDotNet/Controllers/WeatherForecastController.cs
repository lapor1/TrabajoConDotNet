using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json;
using System.Security.Claims;
using System.Text.Json;
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

		[HttpGet(Name = "Get Weather From User")]
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
				
				float hottestDayTemperature = dailyTemp.Daily.ApparentTemperatureMax.Max();
				int hottestDayID = Array.IndexOf(dailyTemp.Daily.ApparentTemperatureMax, hottestDayTemperature);
				string hottestDay = dailyTemp.Daily.Time[hottestDayID];
				

				//var q = dailyTemp.Daily.Time.Zip(dailyTemp.Daily.ApparentTemperatureMax, (l,n) => l + n.ToString());
				//var hottestDay = q[hottestDayTemperature];

				var wR = new WeatherResponse();

				wR.Date = hottestDay;
				wR.MaxTemperature = hottestDayTemperature;
				
				/*
				foreach(var s in q)
					_logger.LogInformation($"among: {s}");*/

				_logger.LogInformation($"El dia con mayor temperatura será el {hottestDay} con una maxima de {hottestDayTemperature}°C");

				return Ok(wR);
			}
		}

		private class WeatherResponse {
			#pragma warning disable CS8618 
			public WeatherResponse() {}
			#pragma warning restore
			public string Date { get; set; }
			public float MaxTemperature { get; set; }
		}

	}
}
