namespace TrabajoConDotNet.Models
{
	public class Weather
	{
		public float Latitude { get; set; }
		public float Longitude { get; set; }
		public float GenerationtimeMs { get; set; }
		public int UtcOffsetSeconds { get; set; }
		public string Timezone { get; set; }
		public string TimezoneAbbreviation { get; set; }
		public float Elevation { get; set; }
		public DailyUnits DailyUnits { get; set; }
		public DailyA Daily { get; set; }
	}

	public class DailyUnits
	{
		public string Time { get; set; }
		public string ApparentTemperatureMax { get; set; }
	}

	public class DailyA
	{
		public string[] Time { get; set; }
		public float[] ApparentTemperatureMax { get; set; }
	}


	public class Rootobject
	{
		public float latitude { get; set; }
		public float longitude { get; set; }
		public float generationtime_ms { get; set; }
		public int utc_offset_seconds { get; set; }
		public string timezone { get; set; }
		public string timezone_abbreviation { get; set; }
		public float elevation { get; set; }
		public Daily_Units daily_units { get; set; }
		public Daily daily { get; set; }
	}

	public class Daily_Units
	{
		public string time { get; set; }
		public string apparent_temperature_max { get; set; }
	}

	public class Daily
	{
		public string[] time { get; set; }
		public float[] apparent_temperature_max { get; set; }
	}

	

}
