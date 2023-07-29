using Microsoft.EntityFrameworkCore.Storage;

namespace BloodDonationApp.DataAccess.Interfaces.Transaction;

public interface IDatabaseTransaction
{
    Task<IDbContextTransaction> BeginTransactionAsync();
}
