using Presentation.Shared.Domain.Entities.Common;

namespace Presentation.Shared.Application.Contracts;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    Task<int> CompleteAsync();
}