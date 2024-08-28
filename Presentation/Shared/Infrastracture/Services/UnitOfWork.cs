using Microsoft.EntityFrameworkCore;
using Presentation.Features.Security.Query;
using Presentation.Shared.Application.Contracts;
using Presentation.Shared.Domain.Entities.Common;
using Presentation.Shared.Infrastracture.Persistence;

namespace Presentation.Shared.Infrastracture.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly Dictionary<string, object> _repositories = new();

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<TEntity>)_repositories[type];
    }

    /// <inheritdoc />
    public IUserRepository UserRepository() => new UserRepository(_context);
    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}