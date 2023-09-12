using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.SaveMeetingsUsers
{
    public class SaveMeetingsUsersRequest : IRequest<SaveMeetingsUsersResponse>
    {
        public Guid UserId { get; set; }
        public Guid MeetingId { get; set; }
        public bool IsmeetingAdmin { get; set; }
    }
}