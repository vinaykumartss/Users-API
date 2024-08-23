
using App.EventManagement.Domain.Entities;

namespace App.EventManagement.Application.Repositories;

public interface IUserRepository : IBaseRepository<Users>
{
    Task<Users> GetByEmail(string email, CancellationToken cancellationToken);
}