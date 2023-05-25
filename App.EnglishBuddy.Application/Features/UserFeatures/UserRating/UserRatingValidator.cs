using FluentValidation;

namespace App.EnglishBuddy.Application.Features.UserFeatures.UserRating;

public sealed class UserRatingValidator : AbstractValidator<UserRatingsRequest>
{
    public UserRatingValidator()
    {
    }
}