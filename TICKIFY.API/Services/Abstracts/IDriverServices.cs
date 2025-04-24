using TICKIFY.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using TICKIFY.API.Contracts.Drivers;
using TICKIFY.API.Contracts.Requests;
using TICKIFY.API.Abstracts;

namespace TICKIFY.API.Services.Abstracts
{
    public interface IDriverServices
    {
         Task<Result<DriverRes>> GetDriverByIdAsync(int id, CancellationToken cancellationToken);
         Task<Result<DriverRes>> CreateDriverAsync(DriverReq driverReq, CancellationToken cancellationToken);
          Task<Result<DriverRes>> UpdateDriverAsync(int id, DriverReq driverReq, CancellationToken cancellationToken);
        Task DeleteDriverAsync(int id, CancellationToken cancellationToken);
        Task<Result> SoftDeleteDriverAsync(int id, CancellationToken cancellationToken);
    }
}
