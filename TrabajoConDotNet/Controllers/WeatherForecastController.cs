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

			UserInDB user = await _dbContext.Users.Where(u => u.Username == Username).FirstOrDefaultAsync();

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

				Rootobject dailyTemp = JsonSerializer.Deserialize<Rootobject>(responseString);

				//Weather weather = WeatherParseoJson(dailyTemp);

				Weather weather = new Weather();

				weather.Longitude = dailyTemp.longitude;
				weather.Latitude = dailyTemp.latitude;
				weather.GenerationtimeMs = dailyTemp.generationtime_ms;
				weather.UtcOffsetSeconds = dailyTemp.utc_offset_seconds;
				weather.Timezone = dailyTemp.timezone;
				weather.TimezoneAbbreviation = dailyTemp.timezone_abbreviation;
				weather.Elevation = dailyTemp.elevation;
				var wdu = new DailyUnits();
				wdu.Time = dailyTemp.daily_units.time;
				wdu.ApparentTemperatureMax = dailyTemp.daily_units.apparent_temperature_max;
				weather.DailyUnits = wdu;
				var wd = new DailyA();
				wd.Time = dailyTemp.daily.time;
				wd.ApparentTemperatureMax = dailyTemp.daily.apparent_temperature_max;
				weather.Daily = wd;

				//JsonConvert.DeserializeObject<Weather>(responseString);

				/*
				float hottestDayTemperature = dailyTemp.daily.apparent_temperature_max.Max();
				int hottestDayID = Array.IndexOf(dailyTemp.daily.apparent_temperature_max, hottestDayTemperature);
				string hottestDay = dailyTemp.daily.time[hottestDayID];
				*/

				float hottestDayTemperature = weather.Daily.ApparentTemperatureMax.Max();
				int hottestDayID = Array.IndexOf(weather.Daily.ApparentTemperatureMax, hottestDayTemperature);
				string hottestDay = weather.Daily.Time[hottestDayID];
				

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

		/*
		public Weather WeatherParseoJson(Rootobject rootobject)
		{
			Weather weather = new Weather();

			weather.Longitude = rootobject.longitude;
			weather.Latitude = rootobject.latitude;
			weather.GenerationtimeMs = rootobject.generationtime_ms;
			weather.UtcOffsetSeconds = rootobject.utc_offset_seconds;
			weather.Timezone = rootobject.timezone;
			weather.TimezoneAbbreviation = rootobject.timezone_abbreviation;
			weather.Elevation = rootobject.elevation;
			weather.DailyUnits.ApparentTemperatureMax = rootobject.daily_units.apparent_temperature_max;
			weather.DailyUnits.Time = rootobject.daily_units.time;
			weather.Daily.Time = rootobject.daily.time;
			weather.Daily.ApparentTemperatureMax = rootobject.daily.apparent_temperature_max;

			return weather;
		}*/
	}
}
