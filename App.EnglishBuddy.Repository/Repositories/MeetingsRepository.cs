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
                            join c in _context.Users on a.UserId equals c.Id
                            where a.IsActive == true && b.IsActive == true

                            select new GetAllMeetingsResponse { MeetingId = a.Id, Subject = a.Subject, Name = a.Name , StartTime = a.StartTime , StartAmPm =a.StartAMPM, UserId= a.UserId, UserIsActive= b.IsActive, CreatedBy= c.FirstName +" " +c.LastName} into x
                            group x by new { x.MeetingId, x.Subject, x.Name, x.StartTime , x.StartAmPm, x.UserIsActive, x.UserId, x.CreatedBy } into g
                          
                            select new GetAllMeetingsResponse
                            {
                                MeetingId = g.Key.MeetingId,
                                UserCount = g.Count(),
                                Subject = g.Key.Subject,
                                Name = g.Key.Name,
                                StartTime = g.Key.StartTime,
                                StartAmPm = g.Key.StartAmPm,
                                CreatedBy = g.Key.CreatedBy,
                                UserId = g.Key.UserId
                                
                            }).ToListAsync(cancellationToken);
        return result;

    }
}