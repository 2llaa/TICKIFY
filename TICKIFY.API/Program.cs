using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using TICKIFY.API.Persistence.Data;
using TICKIFY.API.Persistence.DataSeed;
using TICKIFY.API.Services.Abstracts;
using TICKIFY.API.Services.Implementations;
using TICKIFY.API.Services.Background;
using TICKIFY.Data.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using System.Reflection;
using Microsoft.Extensions.Options;
using TICKIFY.API.Helpers;

var builder = WebApplication.CreateBuilder(args);

// ----------------- Logging -----------------
builder.Host.UseSerilog((context, config) =>
{
    config.WriteTo.Console()
          .WriteTo.File("Logs/Tickify.log", rollingInterval: RollingInterval.Day);
});

// ----------------- Configuration -----------------
var connectionString = builder.Configuration.GetConnectionString("TickifyDbConnection");

// ----------------- DbContext -----------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// ----------------- Identity -----------------
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// ----------------- JWT Configuration -----------------
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
if (jwtSettings == null || string.IsNullOrEmpty(jwtSettings.SecretKey))
{
    throw new InvalidOperationException("JWT configuration is missing or invalid in appsettings.json");
}

// ----------------- JWT Authentication -----------------
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
    };
});

// ----------------- FluentValidation -----------------
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddFluentValidationAutoValidation();

// ----------------- Mapster -----------------
var mapsterConfig = TypeAdapterConfig.GlobalSettings;
mapsterConfig.Scan(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<IMapper>(new Mapper(mapsterConfig));

// ----------------- Application Services -----------------
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IDriverServices, DriverServices>();
builder.Services.AddTransient<IRoomServices, RoomServices>();
builder.Services.AddTransient<IHotelReservationServices, HotelReservationServices>();
builder.Services.AddTransient<IHotelServices, HotelServices>();
builder.Services.AddHostedService<ReservationExpiryService>();

// ----------------- CORS -----------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ----------------- Controllers & Swagger -----------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ----------------- Apply Migrations & Seed -----------------
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
        await SeedRoles.SeedRolesAsync(scope.ServiceProvider);
        logger.LogInformation("Data Seeding Completed Successfully.");
    }
    catch (Exception ex)
    {
        logger.LogError($"Error during migration/seeding: {ex.Message}");
    }
}

// ----------------- Middleware -----------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();