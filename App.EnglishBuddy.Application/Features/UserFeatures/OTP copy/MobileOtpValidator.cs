using App.EnglishBuddy.Application.Common.AppMessage;
using System.Text.RegularExpressions;
using FluentValidation;

namespace App.EnglishBuddy.Application.Features.UserFeatures.MobileOtp;

public sealed class MobileOtpValidator : AbstractValidator<MobileOtpRequest>
{
    public MobileOtpValidator()
    {
        RuleFor(x => x.Mobile)
           .NotEmpty()
           .MinimumLength(10)
           .WithMessage(AppValidationMessage.Mobile)
           .MaximumLength(10)
           .WithMessage(AppValidationMessage.Mobile)
           .Matches(new Regex(@"^[0-9]{10}$")).WithMessage(AppValidationMessage.Mobile); 

    }
}