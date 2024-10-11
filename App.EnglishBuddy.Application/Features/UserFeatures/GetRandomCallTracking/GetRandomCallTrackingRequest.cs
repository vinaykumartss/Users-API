using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetRandomCallTracking
{

    public class GetRandomCallTrackingRequest : IRequest<GetRandomCallTrackingResponse>
    {
        public Guid UserId { get; set; }
    }
}