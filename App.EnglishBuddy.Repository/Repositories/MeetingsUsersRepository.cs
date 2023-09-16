using App.EnglishBuddy.Application.Features.UserFeatures.GetAllMeetings;
using App.EnglishBuddy.Application.Features.UserFeatures.SaveMeetings;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using App.EnglishBuddy.Infrastructure.Context;

namespace App.EnglishBuddy.Infrastructure.Repositories;

public class MeetingsUsersRepository : BaseRepository<MeetingUsers>, IMeetingsUsersRepository
{
    public EnglishBuddyDbContext _context;
    public MeetingsUsersRepository(EnglishBuddyDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<List<GetAllMeetingsResponse>> CallDetails(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}