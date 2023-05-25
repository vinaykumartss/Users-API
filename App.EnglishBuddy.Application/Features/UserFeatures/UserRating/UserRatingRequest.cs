using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.UserRating
{
    public class UserRatingsRequest : IRequest<UserRatingsResponse>
    {
      
        public decimal Rating { get; set; }
        public Guid UserId { get; set; }
        public Guid ToUserId { get; set; }
    }
}