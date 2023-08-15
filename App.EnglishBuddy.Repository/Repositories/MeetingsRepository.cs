using App.EnglishBuddy.Application.Features.UserFeatures.SaveMeetings;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using App.EnglishBuddy.Infrastructure.Context;

namespace App.EnglishBuddy.Infrastructure.Repositories;

public class MeetingsRepository : BaseRepository<Meetings>, IMeetingsRepository
{
    public EnglishBuddyDbContext _context;
    public MeetingsRepository(EnglishBuddyDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<List<SaveMeetingsResponse>> CallDetails(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}