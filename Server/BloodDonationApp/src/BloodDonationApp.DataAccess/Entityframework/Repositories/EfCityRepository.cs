using BloodDonationApp.DataAccess.Entityframework.Contexts;
using BloodDonationApp.DataAccess.Interfaces.Repositories;
using BloodDonationApp.Entities.Entities;

namespace BloodDonationApp.DataAccess.Entityframework.Repositories;

public class EfCityRepository : EfSelectableRepositoryBase<City, BloodDonationAppContext>, ICityRepository
{
    public EfCityRepository(BloodDonationAppContext context) : base(context)
    {
    }
}
