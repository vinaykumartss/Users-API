using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetMeetingJoined
{
    public class GetMeetingJoinedRequest : IRequest<GetMeetingJoinedResponse>
    {
        public Guid UserId { get; set; }

    }
}