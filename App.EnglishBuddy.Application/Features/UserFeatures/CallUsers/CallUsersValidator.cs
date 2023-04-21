using FluentValidation;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CallUsers;

public sealed class CallUsersValidator : AbstractValidator<CallUsersResponse>
{
    public CallUsersValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("Error Message");
        RuleFor(x => x.OpponentUserId).NotEmpty().WithMessage("Error Message");
    }
}