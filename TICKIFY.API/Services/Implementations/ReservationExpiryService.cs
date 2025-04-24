using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TICKIFY.API.Persistence.Data;

namespace TICKIFY.API.Services.Background
{
    public class ReservationExpiryService : BackgroundService
    {
        private readonly ILogger<ReservationExpiryService> _logger;
        private readonly IServiceProvider _services;

        public ReservationExpiryService(ILogger<ReservationExpiryService> logger, IServiceProvider services)
        {
            _logger = logger;
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Reservation Expiry Service running.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _services.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                        var expiredReservations = await dbContext.HotelReservations
                            .Include(r => r.Room)
                            .Where(r => !r.IsDeleted &&
                                   r.Status == "Confirmed" &&
                                   r.CheckOutDate < DateTime.UtcNow)
                            .ToListAsync(stoppingToken);

                        foreach (var reservation in expiredReservations)
                        {
                            reservation.Status = "Completed";
                            if (reservation.Room != null)
                            {
                                reservation.Room.Status = "Available";
                                reservation.Room.DateOut = DateTime.UtcNow;
                            }
                            _logger.LogInformation($"Marked reservation {reservation.HotelReservationId} as completed and room as available.");
                        }

                        if (expiredReservations.Any())
                        {
                            await dbContext.SaveChangesAsync(stoppingToken);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing expired reservations");
                }

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}