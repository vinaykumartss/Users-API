namespace App.EnglishBuddy.Application.Features.UserFeatures.GetUserRating;

public sealed record GetUserRatingResponse
{
   public decimal ? UserRating { get; set; }
   public Guid UserId { get; set; }
}