using App.EnglishBuddy.Application.Features.UserFeatures.CallList;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using App.EnglishBuddy.Infrastructure.Context;

namespace App.EnglishBuddy.Infrastructure.Repositories;

public class TotalRatingsRepository : BaseRepository<TotalRatings>, ITotalRatingsRepository
{
    public EnglishBuddyDbContext _context;
    public TotalRatingsRepository(EnglishBuddyDbContext context) : base(context)
    {
        _context = context;
    }


    public Task<List<CallsDetailsResponse>> Save(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}