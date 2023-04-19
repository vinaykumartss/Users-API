
using App.EnglishBuddy.Domain.Entities;

namespace App.EnglishBuddy.Application.Repositories;

public interface ICallsRepository : IBaseRepository<Calls>
{
    Task<Calls> Connected(string email, CancellationToken cancellationToken);
}