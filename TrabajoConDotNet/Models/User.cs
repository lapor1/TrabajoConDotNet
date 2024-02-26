using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TrabajoConDotNet.Models
{
	public class UserInJson
	{
		//public int Id { get; set; }

		public required string Username { get; set; }

		public float Latitude { get; set; }

		public float Longitude { get; set; }
	}
	
	public class UserInDB
	{
		public UserInDB() { }

		public int Id { get; set; }

		public string Username { get; set; }

		public float Latitude { get; set; }

		public float Longitude { get; set; }
	}
}
