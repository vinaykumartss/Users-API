using App.EnglishBuddy.Application.Common.AppMessage;
using FluentValidation;
using System.Text.RegularExpressions;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CallUsers;

public sealed class OTPValidator : AbstractValidator<OTPRequest>
{
    public OTPValidator()
    {
        //RuleFor(x => x.Mobile)
        //    .NotEmpty()
        //    .MinimumLength(10)
        //    .WithMessage(AppValidationMessage.Mobile)
        //    .MaximumLength(10)
        //    .WithMessage(AppValidationMessage.Mobile)
        //    .Matches(new Regex(@"^[0-9]{10}$")).WithMessage(AppValidationMessage.Mobile); 

    }
}