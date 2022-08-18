using Microsoft.EntityFrameworkCore;
using ReferenceMaterials_API.Data;
using static Raven.Client.Constants;
using Microsoft.Extensions.Configuration;
using ReferenceMaterials_API.Persistence;

var builder = WebApplication.CreateBuilder(args);

//starting our ravendb server:

// Add services to the container.

//creating a in memory database: (just for testing the api with in memory database either use this or ravendb)
//builder.Services.AddDbContext<APIContext> (opt => opt.UseInMemoryDatabase("MaterialDb"));


//creating service for ravendb as:
//creating a singleton for ravendb as: (so that for every access to the database it will use a 1 time created instance of this class )
builder.Services.AddSingleton(typeof(IRepository<>), typeof(RavenDbRepository<>));
builder.Services.AddSingleton<IRavenDbContext, RavenDbContext>();//interface and it's implementation
//it connects the RavenDb database with our model class
builder.Services.Configure<PersistenceSettings>(builder.Configuration.GetSection("Database"));//takes section from our appsettings.json file
//we created the persistenceSettings class down in this file


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


//Implementing the persistence class:
public class PersistenceSettings
{
    //Database Name:
    public String DatabaseName { get; set; }
    //Array of strings called url:
    public String[] Urls { get; set; }
}
