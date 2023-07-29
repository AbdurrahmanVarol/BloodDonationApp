using BloodDonationApp.DataAccess.Entityframework.Contexts;
using BloodDonationApp.DataAccess.Interfaces.Transaction;
using Microsoft.EntityFrameworkCore.Storage;

namespace BloodDonationApp.DataAccess.Entityframework.Transaction;

public class EfDatabaseTransaction : IDatabaseTransaction
{
    private readonly BloodDonationAppContext _appContext;


    public EfDatabaseTransaction(BloodDonationAppContext context)
    {
        _appContext = context;
    }
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _appContext.Database.BeginTransactionAsync();
    }
}
