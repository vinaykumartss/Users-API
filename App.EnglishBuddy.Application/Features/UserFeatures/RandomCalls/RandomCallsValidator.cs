using FluentValidation;

namespace App.EnglishBuddy.Application.Features.UserFeatures.RandomCalls;

public sealed class RandomCallsValidator : AbstractValidator<RandomCallsResponse>
{
    public RandomCallsValidator()
    {
        RuleFor(x => x.FromUserId).NotEmpty().WithMessage("User is not found");
    }
}