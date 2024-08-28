using Microsoft.EntityFrameworkCore;
using Presentation.Shared.Application.Contracts;
using Presentation.Shared.Domain.Entities;
using Presentation.Shared.Extensions;
using Presentation.Shared.Infrastracture.Persistence;
using Presentation.Shared.Infrastracture.Services;

namespace Presentation.Features.Security.Query;

public class UserRepository:GenericRepository<User>,IUserRepository
{
    private readonly AppDbContext _context;

    /// <inheritdoc />
    public async Task<User> GetByUserNameAndPasswordAsync(string userName, string password)
    {
        var user=await _context.Users
            .FirstOrDefaultAsync(u => u.Username.ToLower() == userName.ToLower()
                                      &&
                                      u.PasswordHash.ToLower() == password.ComputeSha256Hash());
        return user;
    }

    /// <inheritdoc />
    public UserRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}