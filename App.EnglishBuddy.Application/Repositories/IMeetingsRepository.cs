using App.EnglishBuddy.Application.Features.UserFeatures.SaveMeetings;
using App.EnglishBuddy.Domain.Entities;

namespace App.EnglishBuddy.Application.Repositories;

public interface IMeetingsUsersRepository : IBaseRepository<MeetingUsers>
{
    Task<List<SaveMeetingsResponse>> CallDetails(Guid id, CancellationToken cancellationToken);
}