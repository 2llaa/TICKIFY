using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Tickfy.Entities;
using Tickfy.Persistence;

public static class StoreContextSeed
{
    public static async Task SeedAsync(TickfyDBContext _context)
    {
        await _context.AddFlights();
        await _context.AddClasses();
        await _context.AddHotels();
        await _context.AddRooms();
        await _context.AddBedInfos();
        await _context.AddUsers();


    }

    public static async Task AddFlights(this TickfyDBContext _context)
    {
        try
        {
            var FlightsData = File.ReadAllText("Persistence/DataSeed/Flights.json");
            var Flights = JsonSerializer.Deserialize<List<Flight>>(FlightsData);

            if (Flights?.Count > 0 && !await _context.Set<Flight>().AnyAsync())
            {
                await _context.Set<Flight>().AddRangeAsync(Flights);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding flights: {ex.Message}");
            throw;
        }
    }

    public static async Task AddClasses(this TickfyDBContext _context)
    {
        try
        {
            var ClassesData = File.ReadAllText("Persistence/DataSeed/Classes.json");
            var Classes = JsonSerializer.Deserialize<List<Class>>(ClassesData);

            if (Classes?.Count > 0 && !await _context.Set<Class>().AnyAsync())
            {
                await _context.Set<Class>().AddRangeAsync(Classes);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding classes: {ex.Message}");
            throw;
        }
    }
    public static async Task AddHotels(this TickfyDBContext _context)
    {
        try
        {
            var HotelsData = File.ReadAllText("Persistence/DataSeed/Hotels.json");
            var Hotels = JsonSerializer.Deserialize<List<Hotel>>(HotelsData);

            if (Hotels?.Count > 0 && !await _context.Set<Hotel>().AnyAsync())
            {
                await _context.Set<Hotel>().AddRangeAsync(Hotels);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding Hotels: {ex.Message}");
            throw;
        }
    }
    public static async Task AddRooms(this TickfyDBContext _context)
    {
        try
        {
            var RoomssData = File.ReadAllText("Persistence/DataSeed/Rooms.json");
            var Rooms = JsonSerializer.Deserialize<List<Room>>(RoomssData);

            if (Rooms?.Count > 0 && !await _context.Set<Room>().AnyAsync())
            {
                await _context.Set<Room>().AddRangeAsync(Rooms);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding Rooms: {ex.Message}");
            throw;
        }
    }
    public static async Task AddBedInfos(this TickfyDBContext _context)
    {
        try
        {
            var BedInfosData = File.ReadAllText("Persistence/DataSeed/BedInfos.json");
            var BedInfos = JsonSerializer.Deserialize<List<BedInfo>>(BedInfosData);

            if (BedInfos?.Count > 0 && !await _context.Set<BedInfo>().AnyAsync())
            {
                await _context.Set<BedInfo>().AddRangeAsync(BedInfos);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding BedInfos: {ex.Message}");
            throw;
        }
    }
    public static async Task AddUsers(this TickfyDBContext _context)
    {
        try
        {
            var ApplicationUsersData = File.ReadAllText("Persistence/DataSeed/Users.json");
            var ApplicationUsers = JsonSerializer.Deserialize<List<ApplicationUser>>(ApplicationUsersData);

            if (ApplicationUsers?.Count > 0 && !await _context.Set<ApplicationUser>().AnyAsync())
            {
                await _context.Set<ApplicationUser>().AddRangeAsync(ApplicationUsers);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding ApplicationUsers: {ex.Message}");
            throw;
        }
    }

}