using App.EnglishBuddy.Application.Features.UserFeatures.GetAllMeetings;
using App.EnglishBuddy.Application.Features.UserFeatures.GetAllUser;
using App.EnglishBuddy.Application.Features.UserFeatures.SaveMeetings;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using App.EnglishBuddy.Infrastructure.Context;
using System.Linq;

namespace App.EnglishBuddy.Infrastructure.Repositories;

public class MeetingsRepository : BaseRepository<Meetings>, IMeetingsRepository
{
    public EnglishBuddyDbContext _context;
    public MeetingsRepository(EnglishBuddyDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<List<GetAllMeetingsResponse>> CallDetails(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        //List<GetAllMeetingsResponse> obj = new List<GetAllMeetingsResponse>();
        //return obj;
        //var result = (from a in _context.Meetings
        //              join b in _context.MeetingUsers on a.MeetingId equals b.MeetingId
        //              group b by a.MeetingId into g
        //              orderby g.Count() descending
        //              select new GetAllMeetingsResponse
        //              {
        //                 MeetingId = g.Key.HasValue.


        //              }).ToList();
        //return result;
    }
}