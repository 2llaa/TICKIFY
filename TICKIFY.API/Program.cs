using Microsoft.EntityFrameworkCore;
using Serilog;
using TICKIFY.API.Persistence.Data;
using TICKIFY.API.Persistence.DataSeed;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using System.Reflection;
using TICKIFY.API.Services.Abstracts;
using TICKIFY.API.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

//   Configure Serilog
builder.Host.UseSerilog((context, config) =>
{
    config.WriteTo.Console()
          .WriteTo.File("Logs/Tickify.log", rollingInterval: RollingInterval.Day);
});

//  Load connection string
var connectionString = builder.Configuration.GetConnectionString("TickifyDbConnection");

//  Register DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

//  Register Controllers
builder.Services.AddControllers();

//  Register Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//   Register FluentValidation
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddFluentValidationAutoValidation();

//  Register Mapster
var mapsterConfig = TypeAdapterConfig.GlobalSettings;
mapsterConfig.Scan(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<IMapper>(new Mapper(mapsterConfig));

//  Register Services
builder.Services.AddTransient<IDriverServices, DriverServices>();
builder.Services.AddTransient<IRoomServices, RoomServices>();
builder.Services.AddTransient<IHotelReservationServices, HotelReservationServices>();
builder.Services.AddTransient<IHotelServices, HotelServices>();

//   Register CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

//  Apply migrations and seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ApplicationDbContext>();
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        await dbContext.Database.MigrateAsync();
        logger.LogInformation("Migrations Applied Successfully.");

        await DataSeeder.SeedAsync(dbContext);
        logger.LogInformation("Data Seeding Completed Successfully.");
    }
    catch (Exception ex)
    {
        logger.LogError($"Error during migration/seeding: {ex.Message}");
    }
}

//   Enable Swagger in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//   Middleware
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

//  Run the app
await app.RunAsync();