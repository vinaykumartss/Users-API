using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.MeetingInActive
{
    public class MeetingInActiveRequest : IRequest<MeetingInActiveResponse>
    {
        public Guid Meeting { get; set; }

        public Guid UserId { get; set; }

    }
}