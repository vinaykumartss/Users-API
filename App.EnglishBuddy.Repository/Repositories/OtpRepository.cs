using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using App.EnglishBuddy.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace App.EnglishBuddy.Infrastructure.Repositories;

public class OTPRepository : BaseRepository<Otp>, IOTPRepository
{
    public OTPRepository(EnglishBuddyDbContext context) : base(context)
    {
    }
    
    public Task<Users> GetByEmail(string email, CancellationToken cancellationToken)
    {
        return Context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }
}