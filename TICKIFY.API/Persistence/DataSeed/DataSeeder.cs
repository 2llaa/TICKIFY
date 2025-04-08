using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using TICKIFY.API.Persistence.Data;
using TICKIFY.Data.Entities;

namespace TICKIFY.API.Persistence.DataSeed
{
    public static class DataSeeder
    {
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public static async Task SeedAsync(ApplicationDbContext context)
        {
            await AddHotelsAsync(context);
            await AddRoomsAsync(context);
            await AddDriversAsync(context);
            await AddReservationsAsync(context);
        }

        public static async Task AddHotelsAsync(ApplicationDbContext context)
        {
            try
            {
                var hotelsPath = GetFilePath("Hotels.json");
                var hotelsData = File.ReadAllText(hotelsPath);
                var hotels = JsonSerializer.Deserialize<List<Hotels>>(hotelsData, _jsonOptions);

                if (hotels?.Any() == true && !await context.Set<Hotels>().AnyAsync())
                {
                    await context.Set<Hotels>().AddRangeAsync(hotels);
                    await context.SaveChangesAsync();
                    Console.WriteLine("Hotels have been added successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding hotels: {ex.Message}");
                throw;
            }
        }

        public static async Task AddRoomsAsync(ApplicationDbContext context)
        {
            try
            {
                var roomsPath = GetFilePath("Rooms.json");
                var roomsData = File.ReadAllText(roomsPath);
                var rooms = JsonSerializer.Deserialize<List<Rooms>>(roomsData, _jsonOptions);

                if (rooms?.Any() == true && !await context.Set<Rooms>().AnyAsync())
                {
                    await context.Set<Rooms>().AddRangeAsync(rooms);
                    await context.SaveChangesAsync();
                    Console.WriteLine("Rooms have been added successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding rooms: {ex.Message}");
                throw;
            }
        }

        public static async Task AddDriversAsync(ApplicationDbContext context)
        {
            try
            {
                var driversPath = GetFilePath("Drivers.json");
                var driversData = File.ReadAllText(driversPath);
                var drivers = JsonSerializer.Deserialize<List<Drivers>>(driversData, _jsonOptions);

                if (drivers?.Any() == true && !await context.Set<Drivers>().AnyAsync())
                {
                    await context.Set<Drivers>().AddRangeAsync(drivers);
                    await context.SaveChangesAsync();
                    Console.WriteLine("Drivers have been added successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding drivers: {ex.Message}");
                throw;
            }
        }

        public static async Task AddReservationsAsync(ApplicationDbContext context)
        {
            try
            {
                var reservationsPath = GetFilePath("HotelReservations.json");
                var reservationsData = File.ReadAllText(reservationsPath);
                var reservations = JsonSerializer.Deserialize<List<HotelReservations>>(reservationsData, _jsonOptions);

                if (reservations?.Any() == true && !await context.Set<HotelReservations>().AnyAsync())
                {
                    await context.Set<HotelReservations>().AddRangeAsync(reservations);
                    await context.SaveChangesAsync();
                    Console.WriteLine("Reservations have been added successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding reservations: {ex.Message}");
                throw;
            }
        }

        private static string GetFilePath(string fileName)
        {
            var basePath = Directory.GetCurrentDirectory();
            return Path.Combine(basePath, "..", "TICKIFY.API", "Persistence", "DataSeed", fileName);
        }
    }
}
