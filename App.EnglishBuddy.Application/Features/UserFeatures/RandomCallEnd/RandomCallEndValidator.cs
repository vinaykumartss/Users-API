using FluentValidation;

namespace App.EnglishBuddy.Application.Features.UserFeatures.RandomCallEnd;

public sealed class RandomCallsValidator : AbstractValidator<RandomCallEndRequest>
{
    public RandomCallsValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("User id can not be balnk");
        RuleFor(x => x.MeetingId).NotEmpty().WithMessage("Meeting id can not be balnk");
    }
}