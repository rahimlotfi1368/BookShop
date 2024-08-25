using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Presentation.Shared.Application.Contracts;
using Presentation.Shared.Domain.Entities.Common;
using Presentation.Shared.Infrastracture.Persistence;

namespace Presentation.Shared.Infrastracture.Services;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext Context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        Context = context;
        _dbSet = Context.Set<T>();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        Context.Entry(entity).State = EntityState.Modified;
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }
}
