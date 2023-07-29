using BloodDonationApp.Entities.Entities;
using System.Linq.Expressions;

namespace BloodDonationApp.DataAccess.Interfaces.Repositories;

public interface ISelectableRepository<TEntity> where TEntity : class, IEntity
{
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter);
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null);
    Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filter);
}