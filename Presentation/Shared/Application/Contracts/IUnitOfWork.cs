using Presentation.Features.Security.Query;
using Presentation.Shared.Domain.Entities.Common;

namespace Presentation.Shared.Application.Contracts;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    IUserRepository UserRepository();
    Task<int> CompleteAsync();
}