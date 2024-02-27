using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

//using Newtonsoft.Json;
using System.Security.Claims;
using System.Text.Json;
using TrabajoConDotNet.Data;
using TrabajoConDotNet.Models;
using static System.Net.WebRequestMethods;

namespace TrabajoConDotNet.Controllers
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

				//Convierte el Json (string) en objeto
				Weather dailyTemp = JsonSerializer.Deserialize<Weather>(responseString);

				//Concatena y Obtiene el dia y la temperatura mas alta
				var hottestDayAndTemperature = dailyTemp.Daily.Time
					.Zip(dailyTemp.Daily.ApparentTemperatureMax)
					.OrderByDescending(dt => dt.Second)
					.First();

				//Respuesta
				var weatherResponse = new
				{
					Date = hottestDayAndTemperature.First,
					MaxTemperature = hottestDayAndTemperature.Second
				};

				_logger.LogInformation($"El dia con mayor temperatura será el {hottestDayAndTemperature.First} con una maxima de {hottestDayAndTemperature.Second}°C");

				return Ok(weatherResponse);
			}
		}
	}

	[ApiController]
	[Route("user")]
	public class UserController : Controller
	{
		private readonly DataBase _dbContext;

		private readonly ILogger<UserController> _logger;

		public UserController(ILogger<UserController> logger, DataBase dbContext)
		{
			_logger = logger;
			_dbContext = dbContext;
		}

		[HttpPost]
		public async Task<ActionResult> Post(User u)
		{
			_dbContext.Users.Add(u);
			await _dbContext.SaveChangesAsync();

			_logger.LogInformation($"Se ha creado usuario {u.Username}");
			return Ok();
		}
	}
}
