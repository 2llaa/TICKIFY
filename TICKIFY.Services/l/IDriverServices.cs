using TICKIFY.Data.Entities;
using TICKIFY.Data.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TICKIFY.Services.Abstracts
{
    public interface IDriverServices
    {
        Task<IEnumerable<Drivers>> GetAllDriversAsync();
        Task<Drivers> GetDriverByIdAsync(int id);
        Task<Drivers> CreateDriverAsync(Drivers driver);
        Task<Drivers> UpdateDriverAsync(Drivers driver);
        Task DeleteDriverAsync(int id);
 
    }
}
