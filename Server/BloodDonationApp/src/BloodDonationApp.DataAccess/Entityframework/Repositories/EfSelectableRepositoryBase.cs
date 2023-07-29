using BloodDonationApp.DataAccess.Interfaces.Repositories;
using BloodDonationApp.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BloodDonationApp.DataAccess.Entityframework.Repositories;

public class EfSelectableRepositoryBase<TEntity, TContext> : ISelectableRepository<TEntity>
    where TEntity : class, IEntity
    where TContext : DbContext
{
    private readonly TContext _context;

    public EfSelectableRepositoryBase(TContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null) => await (filter == null ? _context.Set<TEntity>().ToListAsync() : _context.Set<TEntity>().Where(filter).ToListAsync());

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter) => await _context.Set<TEntity>().FirstOrDefaultAsync(filter);

    public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filter) => await _context.Set<TEntity>().AnyAsync(filter);
}
