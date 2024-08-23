
using App.EventManagement.Application.Repositories;
using App.EventManagement.Domain.Entities;
using App.EventManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace App.EventManagement.Infrastructure.Repositories;

public class UserRepository : BaseRepository<Users>, IUserRepository
{
    public UserRepository(EventManagementDbContext context) : base(context)
    {
    }
    
    public Task<Users> GetByEmail(string email, CancellationToken cancellationToken)
    {
        return Context.Users.FirstOrDefaultAsync(x => x.UserName == email, cancellationToken);
    }
}