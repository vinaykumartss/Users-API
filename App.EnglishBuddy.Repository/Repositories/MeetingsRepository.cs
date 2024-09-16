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
                            join b in _context.MeetingUsers on a.Id equals b.MeetingId into mm
                            from mmm in mm.DefaultIfEmpty()
                            join c in _context.Users on a.UserId equals c.Id into uu
                            from uuu in uu.DefaultIfEmpty()

                            join n in _context.UsersImages on a.UserId equals n.UserId into nn
                            from nnn in nn.DefaultIfEmpty()

                            where a.IsActive == true
                            select new GetAllMeetingsResponse()
                            {
                                    MeetingId = a.MeetingId,
                                    UserId = a.UserId,
                                    Subject = a.Subject,
                                    UserCount = _context.MeetingUsers.Where(x=>x.MeetingId ==a.MeetingId && x.IsActive ==true).Count(),

                            }
                            ).ToListAsync(cancellationToken); 

                            //select new GetAllMeetingsResponse { MeetingId = a.Id, Subject = a.Subject,UserId= a.UserId, UserIsActive= mmm.IsActive, CreatedBy= uuu.FirstName +" " +uuu.LastName, ImagePath = nnn.ImagePath } into x
                            //group x by new { x.MeetingId, x.Subject, x.UserIsActive, x.UserId, x.CreatedBy, x.ImagePath } into g
                          
                            //select new GetAllMeetingsResponse
                            //{
                            //    MeetingId = g.Key.MeetingId,
                            //    UserCount = g.Count(),
                            //    Subject = g.Key.Subject,
 
                            //    CreatedBy = g.Key.CreatedBy,
                            //    UserId = g.Key.UserId,
                            //    ImagePath = g.Key.ImagePath,
                                
                            //}).ToListAsync(cancellationToken);
        return result;

    }
}