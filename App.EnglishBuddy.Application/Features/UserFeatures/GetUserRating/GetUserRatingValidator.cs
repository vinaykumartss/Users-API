using FluentValidation;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetUserRating;

public sealed class GetUserRatingValidator : AbstractValidator<GetUserRatingRequest>
{
    public GetUserRatingValidator()
    {
    }
}