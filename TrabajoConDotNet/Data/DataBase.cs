using Microsoft.EntityFrameworkCore;
using TrabajoConDotNet.Models;

namespace TrabajoConDotNet.Data
{
	//Hereda de DbContext
	public class DataBase : DbContext
	{

		//Constructor
		public DataBase(DbContextOptions<DataBase> options): base(options)
		{

		}

		//Tabla de usuarios
		public DbSet<User> Users => Set<User>();

	}
}
