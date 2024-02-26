using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TrabajoConDotNet.Models
{
	public class User
	{
		[JsonIgnore]
		public int Id { get; set; }

		public string Username { get; set; }

		public float Latitude { get; set; }

		public float Longitude { get; set; }
	}
}
