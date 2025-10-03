

using System.Text.Json.Serialization;

using Airline.Database;
using Airline.Repositories.Implementations;
using Airline.Repositories.Interfaces;
using Airline.Services.Implementations;
using Airline.Services.Interfaces;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AirlineContext>(options =>
    options.UseNpgsql(builder.Configuration["ConnectionStrings:AirlineDB"]));


builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddScoped<IAircraftRepository, AircraftRepository>();
builder.Services.AddScoped<IRouteRepository, RouteRepository>();
builder.Services.AddScoped<ICalculateRoutePriceService, CalculateRoutePriceService>();

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
