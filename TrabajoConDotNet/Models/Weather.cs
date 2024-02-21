namespace TrabajoConDotNet.Models
{
	public class Weather
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
