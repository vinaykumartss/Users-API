
using App.EnglishBuddy.Application.Features.UserFeatures.CallList;
using App.EnglishBuddy.Domain.Entities;

namespace App.EnglishBuddy.Application.Repositories;

public interface IRatingsRepository : IBaseRepository<Ratings>
{
    Task<List<CallsDetailsResponse>> Save(Guid id, CancellationToken cancellationToken);
}