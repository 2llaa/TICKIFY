using Microsoft.EntityFrameworkCore;
using TICKIFY.API.Persistence.Data;

namespace TICKIFY.API.Services.Implementations
{
    public class ReservationStatusUpdater : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ReservationStatusUpdater(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var roomsToUpdate = await context.Rooms
                        .Where(r => r.Status == "Booked" && r.DateOut <= DateTime.UtcNow)
                        .ToListAsync(stoppingToken);  

                    foreach (var room in roomsToUpdate)
                    {
                        room.Status = "Available"; 
                        room.DateOut = DateTime.MinValue; 
                        context.Rooms.Update(room);
                    }

                    await context.SaveChangesAsync(stoppingToken); 
                }

                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken); 
            }
        }
    }

}
