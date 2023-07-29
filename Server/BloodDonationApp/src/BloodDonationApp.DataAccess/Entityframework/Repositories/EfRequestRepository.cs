using BloodDonationApp.DataAccess.Entityframework.Contexts;
using BloodDonationApp.DataAccess.Interfaces.Repositories;
using BloodDonationApp.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BloodDonationApp.DataAccess.Entityframework.Repositories;

public class EfRequestRepository : EfEntityRepositoryBase<Request, BloodDonationAppContext>, IRequestRepository
{
    private readonly BloodDonationAppContext _context;
    public EfRequestRepository(BloodDonationAppContext context) : base(context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Request>> GetRequestsWithIncludes(Expression<Func<Request, bool>>? filter = null)
    {
        return await (filter == null ? _context.Requests.Include(p => p.BloodGroup)
                                                        .Include(p => p.Hospital)
                                                        .ThenInclude(p => p.City).ToListAsync() :
                                      _context.Requests.Where(filter)
                                                        .Include(p => p.BloodGroup)
                                                        .Include(p => p.Hospital)
                                                        .ThenInclude(p => p.City).ToListAsync());
    }
}
