using Tickfy.Contracts.Classes;
using Tickfy.Entities;

namespace Tickfy.Services.Classes;


public interface IClassService
{
    Task<IEnumerable<Class>> GetAsync(int flightID,CancellationToken cancellationToken);
    Task<Result<ClassResponse>> GetAsync(int flightID, int classID, CancellationToken cancellationToken);
    Task<Result<CreateClassResponse>> AddAsync(int flightId,CreateClassRequest classRequest, CancellationToken cancellationToken = default!);

}
