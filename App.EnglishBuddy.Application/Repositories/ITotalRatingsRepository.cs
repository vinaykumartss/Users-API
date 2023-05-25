
using App.EnglishBuddy.Application.Features.UserFeatures.CallList;
using App.EnglishBuddy.Domain.Entities;

namespace App.EnglishBuddy.Application.Repositories;

public interface ITotalRatingsRepository : IBaseRepository<TotalRatings>
{
    Task<List<CallsDetailsResponse>> Save(Guid id, CancellationToken cancellationToken);
}