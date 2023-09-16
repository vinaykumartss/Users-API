using App.EnglishBuddy.Application.Features.UserFeatures.GetAllMeetings;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using App.EnglishBuddy.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace App.EnglishBuddy.Infrastructure.Repositories;

public class MeetingsRepository : BaseRepository<Meetings>, IMeetingsRepository
{
    public EnglishBuddyDbContext _context;
    public MeetingsRepository(EnglishBuddyDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<GetAllMeetingsResponse>> CallDetails(CancellationToken cancellationToken)
    {
        var result = await (from a in _context.Meetings
                            join b in _context.MeetingUsers on a.Id equals b.MeetingId
                            select new GetAllMeetingsResponse { MeetingId = a.MeetingId, Subject = a.Subject, Name = a.Name } into x
                            group x by new { x.MeetingId, x.Subject, x.Name } into g
                            select new GetAllMeetingsResponse
                            {
                                MeetingId = g.Key.MeetingId,
                                UserCount = g.Select(x => x.MeetingId).Count(),
                                Subject = g.Key.Subject,
                                Name = g.Key.Name

                            }).ToListAsync(cancellationToken);
        return result;

    }
}