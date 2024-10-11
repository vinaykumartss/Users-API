using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.RandomCallTrackings
{

    public class RandomCallTrackingRequest : IRequest<RandomCallTrackingResponse>
    {
        public Guid UserId { get; set; }

        public Guid ToUserId { get; set; }
        public int Minutes { get; set; }
    }
}