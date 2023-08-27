
using App.EnglishBuddy.Application.Features.UserFeatures.CallList;
using App.EnglishBuddy.Domain.Entities;

namespace App.EnglishBuddy.Application.Repositories;

public interface IUsersImagesRepository : IBaseRepository<UsersImages>
{
    Task<List<CallsDetailsResponse>> CallDetails(Guid id, CancellationToken cancellationToken);
}