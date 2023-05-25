using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetUserRating
{
    public class GetUserRatingRequest : IRequest<GetUserRatingResponse>
    {
        public Guid UserId { get; set; }
       
    }
}