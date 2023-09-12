using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetMeetingsUsers
{
    public class GetMeetingsUsersRequest : IRequest<GetMeetingsUsersResponse>
    {
       
        public Guid MeetingId { get; set; }
        
    }
}