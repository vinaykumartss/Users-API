using App.EnglishBuddy.Application.Features.UserFeatures.SaveMeetings;
using App.EnglishBuddy.Domain.Entities;

namespace App.EnglishBuddy.Application.Repositories;

public interface IMeetingsRepository : IBaseRepository<Meetings>
{
    Task<List<SaveMeetingsResponse>> CallDetails(Guid id, CancellationToken cancellationToken);
}