
using App.EnglishBuddy.Application.Features.UserFeatures.CallList;
using App.EnglishBuddy.Domain.Entities;

namespace App.EnglishBuddy.Application.Repositories;

public interface IFriendRepository : IBaseRepository<Friend>
{
    Task<List<CallsDetailsResponse>> CallDetails(Guid id, CancellationToken cancellationToken);
}