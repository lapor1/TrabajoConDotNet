using Microsoft.EntityFrameworkCore;
using TrabajoConDotNet.Data;
using TrabajoConDotNet.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Coneccion  a la base de datos
var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
builder.Services.AddDbContext<DataBase>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

/**************************************************************/

app.MapPost("/user/", async (User u, DataBase db) =>
{

	db.Users.Add(u);
	
	//Intento de escribir una sentencia se SQL
	//db.Users.FromSql($"INSERT INTO public.\"Users\"(\r\n\t\"Username\", \"Latitude\", \"Longitude\")\r\n\tVALUES ({u.Username},{u.Latitude},{u.Longitude});");
	
	await db.SaveChangesAsync();

	return Results.Created($"/User/{u.Username}", u);
});

/*
app.MapGet("/User/{id:int}", async (int id, DataBase db) =>
{
	return await db.Users.FindAsync(id)
		is User u ? Results.Ok(u) : Results.NotFound();
});

app.MapGet("/User/{username:string}", async (string username, DataBase db) =>
{
	return await db.Users.FindAsync(username)
		is User u ? Results.Ok(u) : Results.NotFound();
});

app.MapGet("/User", async (DataBase db) => await db.Users.ToListAsync());

app.MapPut("/User/{id:int}", async (int id, User u, DataBase db) =>
{
	if (u.Id != id)
		return Results.BadRequest();

	var User = await db.Users.FindAsync(id);

	if (User is null) return Results.NotFound();

	User.Username = u.Username;
	User.Latitude = u.Latitude;
	User.Longitude = u.Longitude;

	await db.SaveChangesAsync();

	return Results.Ok(User);
});

app.MapDelete("/User/{id:int}", async (int id, DataBase db) =>
{
	var User = await db.Users.FindAsync(id);

	if (User is null) return Results.NotFound();

	db.Users.Remove(User);
	await db.SaveChangesAsync();

	return Results.NoContent();
});*/

app.UseAuthorization();

app.MapControllers();

app.Run();
