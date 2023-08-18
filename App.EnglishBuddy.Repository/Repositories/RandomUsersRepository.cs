using App.EnglishBuddy.Application.Features.UserFeatures.CallList;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using App.EnglishBuddy.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace App.EnglishBuddy.Infrastructure.Repositories;

public class RandomUsersRepository : BaseRepository<RandomUsers>, IRandomUsersRepository
{
    public EnglishBuddyDbContext _context;
    public RandomUsersRepository(EnglishBuddyDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<CallsDetailsResponse>> CallDetails(Guid id, CancellationToken cancellationToken)
    {
        List<CallsDetailsResponse> query = await (from u in _context.Users
                                                  join p in _context.CallInfo on u.Id equals p.FromUserId into gj
                                                  from x in gj.DefaultIfEmpty()
                                                  where x.FromUserId == id
                                                  select new CallsDetailsResponse
                                                  {
                                                      Date = x.CreatedDate,
                                                      Name = u.FirstName + " " + u.LastName,
                                                      TotalMinutes = x.TotalTime,
                                                      Id = x.Id
                                                  }).ToListAsync(cancellationToken);
        return query;
    }
}