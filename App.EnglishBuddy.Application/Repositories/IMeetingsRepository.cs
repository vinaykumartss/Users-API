using App.EnglishBuddy.Application.Features.UserFeatures.GetAllMeetings;
using App.EnglishBuddy.Application.Features.UserFeatures.SaveMeetings;
using App.EnglishBuddy.Domain.Entities;

namespace App.EnglishBuddy.Application.Repositories;

public interface IMeetingsUsersRepository : IBaseRepository<MeetingUsers>
{
    //Task<List<GetAllMeetingsResponse>> CallDetails(CancellationToken cancellationToken);
}