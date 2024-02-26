using System.Text.Json.Serialization;

namespace TrabajoConDotNet.Models
{
	public class Weather
	{
		[JsonPropertyName("latitude")]
		public float Latitude { get; set; }

		[JsonPropertyName("longitude")]
		public float Longitude { get; set; }

		[JsonPropertyName("generationtime_ms")]
		public float GenerationtimeMs { get; set; }

		[JsonPropertyName("utc_offset_seconds")]
		public int UtcOffsetSeconds { get; set; }

		[JsonPropertyName("timezone")]
		public string Timezone { get; set; }

		[JsonPropertyName("timezone_abbreviation")]
		public string TimezoneAbbreviation { get; set; }

		[JsonPropertyName("elevation")]
		public float Elevation { get; set; }

		[JsonPropertyName("daily_units")]
		public DailyUnits DailyUnits { get; set; }

		[JsonPropertyName("daily")]
		public Daily Daily { get; set; }
	}

	public class DailyUnits
	{
		[JsonPropertyName("time")]
		public string Time { get; set; }

		[JsonPropertyName("apparent_temperature_max")]
		public string ApparentTemperatureMax { get; set; }
	}

	public class Daily
	{
		[JsonPropertyName("time")]
		public string[] Time { get; set; }

		[JsonPropertyName("apparent_temperature_max")]
		public float[] ApparentTemperatureMax { get; set; }
	}
}
