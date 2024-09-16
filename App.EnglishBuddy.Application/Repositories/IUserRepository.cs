
using App.EnglishBuddy.Application.Features.UserFeatures.GetAllUser;
using App.EnglishBuddy.Domain.Entities;

namespace App.EnglishBuddy.Application.Repositories;

public interface IUserRepository : IBaseRepository<Users>
{
    Task<Users> GetByEmail(string email, CancellationToken cancellationToken);
    Task<List<GetAllUserResponse>> GetAllUsers(GetAllUserRequest request, CancellationToken cancellationToken);
}
