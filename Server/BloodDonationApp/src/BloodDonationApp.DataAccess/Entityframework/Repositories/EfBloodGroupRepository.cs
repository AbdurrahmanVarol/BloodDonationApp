using BloodDonationApp.DataAccess.Entityframework.Contexts;
using BloodDonationApp.DataAccess.Interfaces.Repositories;
using BloodDonationApp.Entities.Entities;

namespace BloodDonationApp.DataAccess.Entityframework.Repositories;

public class EfBloodGroupRepository : EfSelectableRepositoryBase<BloodGroup, BloodDonationAppContext>, IBloodGroupRepository
{
    public EfBloodGroupRepository(BloodDonationAppContext context) : base(context)
    {
    }
}
