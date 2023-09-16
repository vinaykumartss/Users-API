using App.EnglishBuddy.Application.Features.UserFeatures.GetAllMeetings;
using App.EnglishBuddy.Application.Features.UserFeatures.SaveMeetings;
using App.EnglishBuddy.Domain.Entities;

namespace App.EnglishBuddy.Application.Repositories;

public interface IMeetingsRepository : IBaseRepository<Meetings>
{
    Task<List<GetAllMeetingsResponse>> CallDetails(Guid id, CancellationToken cancellationToken);
}