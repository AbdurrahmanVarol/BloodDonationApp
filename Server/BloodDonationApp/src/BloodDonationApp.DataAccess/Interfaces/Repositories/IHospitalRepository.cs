using BloodDonationApp.Entities.Entities;
using System.Linq.Expressions;

namespace BloodDonationApp.DataAccess.Interfaces.Repositories;

public interface IHospitalRepository : IEntityRepository<Hospital>
{
    Task<IEnumerable<Hospital>> GetHospitalsWithIncludes(Expression<Func<Hospital, bool>>? filter = null);
}
