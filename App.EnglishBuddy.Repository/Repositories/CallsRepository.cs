using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using App.EnglishBuddy.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace App.EnglishBuddy.Infrastructure.Repositories;

public class CallsRepository : BaseRepository<Calls>, ICallsRepository
{
    public CallsRepository(EnglishBuddyDbContext context) : base(context)
    {
    }
    public Task<Calls> Connected(string email, CancellationToken cancellationToken)
    {
        return Context.Calls.FirstOrDefaultAsync(x => x.UserId == email, cancellationToken);
    }

}