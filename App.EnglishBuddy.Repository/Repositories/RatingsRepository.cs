using App.EnglishBuddy.Application.Features.UserFeatures.CallList;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using App.EnglishBuddy.Infrastructure.Context;

namespace App.EnglishBuddy.Infrastructure.Repositories;

public class RatingsRepository : BaseRepository<Ratings>, IRatingsRepository
{
    public EnglishBuddyDbContext _context;
    public RatingsRepository(EnglishBuddyDbContext context) : base(context)
    {
        _context = context;
    }


    public Task<List<CallsDetailsResponse>> Save(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}