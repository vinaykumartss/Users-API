using FluentValidation;

namespace App.EnglishBuddy.Application.Features.UserFeatures.ForgotPassword;

public sealed class ForgotPasswordValidator : AbstractValidator<ForgotPasswordRequest>
{
    public ForgotPasswordValidator()
    {

        RuleFor(x => x.Email).NotEmpty().WithMessage("Email Id is required").EmailAddress().WithMessage("Please enetr valid email");
       
    }
}