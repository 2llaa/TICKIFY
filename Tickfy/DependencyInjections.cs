using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Reflection;
using System.Text;
using Tickfy.Authentication;
using Tickfy.Entities;
using Tickfy.Persistence;
using Tickfy.Services.Authorization;
using Tickfy.Services.Classes;
using Tickfy.Services.Driver;
using Tickfy.Services.Email;
using Tickfy.Services.Flights;
using Tickfy.Services.HotelReservations;
using Tickfy.Services.Hotels;
using Tickfy.Services.Rooms;
using Tickfy.Settings;

namespace Tickfy;

public static class DependencyInjections
{
    public static IServiceCollection AddDependencies(this IServiceCollection services,
       IConfiguration configurations)
    {

        services.AddControllers();

        var connectionString = configurations.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("No connection string named 'DefaultConnection' is found.");

        services.AddDbContext<TickfyDBContext>(options =>
            options.UseSqlServer(connectionString));



        services
            .AddSwaggerServices()
            .AddMapsterConfigurations()
            .AddFluentValidationCONFIGURATIONS()
            .AddAuthConfigurations(configurations);


        services.AddScoped<IFlightService, FlightService>();
        services.AddScoped<IClassService, ClassService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IFlightReservationService, FlightReservationService>();
        services.AddScoped<IHotelService, HotelService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IDriverService, DriverService>();
        services.AddScoped<IHotelReservationService, HotelReservationService>();
        services.AddScoped<IEmailSender, EmailService>();


        services.Configure<MailSettings>(configurations.GetSection(nameof(MailSettings)));

        services.AddHttpContextAccessor();


        return services;

    }

    public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;

    }
    public static IServiceCollection AddMapsterConfigurations(this IServiceCollection services)
    {


        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMapper>(new Mapper(mappingConfig));


        return services;

    }
    public static IServiceCollection AddFluentValidationCONFIGURATIONS(this IServiceCollection services)
    {

        services
           .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
           .AddFluentValidationAutoValidation();

        return services;

    }

    private static IServiceCollection AddAuthConfigurations(this IServiceCollection services
        , IConfiguration configuration)
    {
        services.AddSingleton<IJwtProvider, JwtProvider>();

        //services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        services.AddOptions<JwtOptions>()
            .BindConfiguration(JwtOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var jwtSettings = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<TickfyDBContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {

            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }
        )
            .AddJwtBearer(o =>
            {
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key!)),
                    ValidIssuer = jwtSettings?.Issuer,
                    ValidAudience = jwtSettings?.Audience
                };
            });

        services.Configure<IdentityOptions>(options =>
        {
            options.SignIn.RequireConfirmedEmail = true;
            options.User.RequireUniqueEmail = true;
            options.Password.RequiredLength = 8;
        });

        return services;


    }

}