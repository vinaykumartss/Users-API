using FluentValidation;

namespace App.EnglishBuddy.Application.Features.UserFeatures.Password;

public sealed class PasswordValidator : AbstractValidator<PasswordRequest>
{
    public PasswordValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("First Name is required");
       // RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage("Please enter current password").When(x=>x.IsForgotPassword==false);
        RuleFor(x => x.NewPassword).NotEmpty().WithMessage("Please enter New password");
    }
}