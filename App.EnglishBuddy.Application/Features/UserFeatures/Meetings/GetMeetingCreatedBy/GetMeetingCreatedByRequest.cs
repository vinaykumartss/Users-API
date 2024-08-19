using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetMeetingCreatedBy
{
    public class GetMeetingCreatedByRequest : IRequest<GetMeetingCreatedByResponse>
    {
        public Guid UserId { get; set; }

    }
}