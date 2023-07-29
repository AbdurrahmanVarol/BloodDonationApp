using BloodDonationApp.Entities.Entities;

namespace BloodDonationApp.DataAccess.Interfaces.Repositories;

public interface IEntityRepository<TEntity> : ISelectableRepository<TEntity>
    where TEntity : class, IEntity
{
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}
