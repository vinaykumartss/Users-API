using FluentValidation;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CallUsers;

public sealed class OTPVerifyValidator : AbstractValidator<OTPVerifyRequest>
{
    public OTPVerifyValidator()
    {
        RuleFor(x => x.Mobile)
          .NotEmpty();
            

    }
}