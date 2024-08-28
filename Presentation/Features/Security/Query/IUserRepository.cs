using Presentation.Shared.Application.Contracts;
using Presentation.Shared.Domain.Entities;

namespace Presentation.Features.Security.Query;

public interface IUserRepository:IGenericRepository<User>
{
    Task<User> GetByUserNameAndPasswordAsync(string userName,string password);
}