using BloodDonationApp.DataAccess.Entityframework.Contexts;
using BloodDonationApp.DataAccess.Interfaces.Repositories;
using BloodDonationApp.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BloodDonationApp.DataAccess.Entityframework.Repositories;

public class EfHospitalRepository : EfEntityRepositoryBase<Hospital, BloodDonationAppContext>, IHospitalRepository
{
    private BloodDonationAppContext _context;
    public EfHospitalRepository(BloodDonationAppContext context) : base(context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Hospital>> GetHospitalsWithIncludes(Expression<Func<Hospital, bool>>? filter = null)
    {
        return await (filter == null ? _context.Hospitals.Include(p => p.City).ToListAsync() :
                                      _context.Hospitals.Where(filter)
                                                        .Include(p => p.City).ToListAsync());
    }
}
