using BloodDonationApp.Entities.Entities;
using System.Linq.Expressions;

namespace BloodDonationApp.DataAccess.Interfaces.Repositories;

public interface IRequestRepository : IEntityRepository<Request>
{
    Task<IEnumerable<Request>> GetRequestsWithIncludes(Expression<Func<Request, bool>>? filter = null);
}
