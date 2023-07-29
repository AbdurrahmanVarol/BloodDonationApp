﻿using BloodDonationApp.DataAccess.Entityframework.Contexts;
using BloodDonationApp.DataAccess.Interfaces.Repositories;
using BloodDonationApp.Entities.Entities;

namespace BloodDonationApp.DataAccess.Entityframework.Repositories;

public class EfGenderRepository : EfSelectableRepositoryBase<Gender, BloodDonationAppContext>, IGenderRepository
{
    public EfGenderRepository(BloodDonationAppContext context) : base(context)
    {
    }
}
