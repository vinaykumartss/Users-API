
using App.EnglishBuddy.Domain.Entities;

namespace App.EnglishBuddy.Application.Repositories;

public interface IOTPRepository : IBaseRepository<Otp>
{
    Task<Users> GetByEmail(string email, CancellationToken cancellationToken);
}