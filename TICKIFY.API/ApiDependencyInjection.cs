//using Microsoft.EntityFrameworkCore;
//using TICKIFY.API.Services.Abstracts;
//using TICKIFY.API.Services.Implementations;
//using FluentValidation;
//using Mapster;
//using MapsterMapper;
//using System.Reflection;
//using FluentValidation.AspNetCore;
//using TICKIFY.API.Persistence.Data;
//using Microsoft.Extensions.Configuration;

//namespace TICKIFY.API
//{
//    public static class ApiDependencyInjection
//    {
//        public static IServiceCollection AddApiDependencies(this IServiceCollection services, IConfiguration configuration)
//        {
//            services.AddControllers();




//            // Add Swagger, Mapster, FluentValidation configurations
//            services
//                .AddSwaggerServices()
//                .AddMapsterConfigurations()
//                .AddFluentValidationConfigurations();

//            // Add authentication configurations (if needed, pass relevant configuration)


//            // Add service dependencies (e.g. your service implementations)
//            services.AddServiceDependencies();

//            // Add DB context for SQL Server
//            services.AddTickifyDbContext(configuration.GetConnectionString("DefaultConnection"));

//            return services;
//        }

//        // Adds Swagger services
//        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
//        {
//            services.AddEndpointsApiExplorer();
//            services.AddSwaggerGen();
//            return services;
//        }

//        // Add Mapster configurations
//        public static IServiceCollection AddMapsterConfigurations(this IServiceCollection services)
//        {
//            var mappingConfig = TypeAdapterConfig.GlobalSettings;
//            mappingConfig.Scan(Assembly.GetExecutingAssembly());
//            services.AddSingleton<IMapper>(new Mapper(mappingConfig));
//            return services;
//        }

//        // Add FluentValidation configurations
//        public static IServiceCollection AddFluentValidationConfigurations(this IServiceCollection services)
//        {
//            services
//                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
//                .AddFluentValidationAutoValidation();
//            return services;
//        }

//        // Add service dependencies (your business logic services)
//        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
//        {
//            services.AddTransient<IDriverServices, DriverServices>();
//            services.AddTransient<IRoomServices, RoomServices>();
//            services.AddTransient<IHotelReservationServices, HotelReservationServices>();
//            services.AddTransient<IHotelServices, HotelServices>();

//            return services;
//        }

//        // Add SQL Server database context
//        public static void AddTickifyDbContext(this IServiceCollection services, string connectionString)
//        {
//            services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(connectionString));
//        }

//        // Add infrastructure dependencies like repositories (if needed)
 

//        // Add Authentication configurations (if needed)

//    }
//}
