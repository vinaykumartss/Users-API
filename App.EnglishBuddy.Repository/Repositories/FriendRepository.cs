using App.EnglishBuddy.Application.Features.UserFeatures.CallList;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using App.EnglishBuddy.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;

namespace App.EnglishBuddy.Infrastructure.Repositories;

public class FriendRepository : BaseRepository<Friend>, IFriendRepository
{
    public EnglishBuddyDbContext _context;
    public FriendRepository(EnglishBuddyDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<CallsDetailsResponse>> CallDetails(Guid userId, CancellationToken cancellationToken)
    {
        List<CallsDetailsResponse> query = await (from u in _context.Users
                                                  join p in _context.Friend on u.Id equals p.ToUserId into gj
                                                  from x in gj.DefaultIfEmpty()
                                                  where x.ToUserId == userId
                                                  select new CallsDetailsResponse
                                                  {
                                                      Date = x.CreatedDate,
                                                      Name = u.FirstName + " " + u.LastName,
                                                      Id = x.Id,
                                                      FromUserId =x.UserId,
                                                      FromName = _context.Users.Where(m => m.Id == x.UserId).FirstOrDefault().FirstName,

                                                  }).ToListAsync(cancellationToken);
        return query;
    }
}