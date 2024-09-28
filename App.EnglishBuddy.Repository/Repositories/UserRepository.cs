using App.EnglishBuddy.Application.Features.UserFeatures.GetAllUser;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using App.EnglishBuddy.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace App.EnglishBuddy.Infrastructure.Repositories;

public class UserRepository : BaseRepository<Users>, IUserRepository
{
    public EnglishBuddyDbContext _context;
    public UserRepository(EnglishBuddyDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<GetAllUserResponse>> GetAllUsers(GetAllUserRequest request, CancellationToken cancellationToken)
    {
        List<GetAllUserResponse> query = await (from u in _context.Users
                                                join p in _context.UsersImages on u.Id equals p.UserId into gj
                                                from x in gj.DefaultIfEmpty()
                                                select new GetAllUserResponse
                                                {
                                                    Name = u.FirstName + " " +u.LastName,
                                                    Email = u.Email,
                                                    Address= u.CityName,
                                                    FcmToken= u.FcmToken,
                                                    Mobile = u.Mobile,
                                                  //  Image = x.ImagePath
                                                })
                                               .Skip((request.PageNumber - 1) * request.PageSize)
                                               .Take(request.PageSize).ToListAsync(cancellationToken);

                                                   
        return query;
    }

    public Task<Users> GetByEmail(string email, CancellationToken cancellationToken)
    {
        return Context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }
}