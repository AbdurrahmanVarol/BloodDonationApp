using BloodDonationApp.DataAccess.Interfaces.Repositories;
using BloodDonationApp.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationApp.DataAccess.Entityframework.Repositories;

public class EfEntityRepositoryBase<TEntity, TContext> : EfSelectableRepositoryBase<TEntity, TContext>, IEntityRepository<TEntity>
    where TEntity : class, IEntity
    where TContext : DbContext
{
    private readonly TContext _context;

    public EfEntityRepositoryBase(TContext context) : base(context)
    {
        _context = context;
    }

    public async Task AddAsync(TEntity entity)
    {
        var addedEntity = _context.Entry(entity);
        addedEntity.State = EntityState.Added;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        var deletedEntity = _context.Entry(entity);
        deletedEntity.State = EntityState.Deleted;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        var updatedEntity = _context.Entry(entity);
        updatedEntity.State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}
