using FluentValidation;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CallList;

public sealed class CallListValidator : AbstractValidator<CallListRequest>
{
    public CallListValidator()
    {
          //RuleFor(x => x.Mobile)
          //  .NotEmpty()
          //  .MinimumLength(10)
          //  .WithMessage(AppValidationMessage.Mobile)
          //  .MaximumLength(10)
          //  .WithMessage(AppValidationMessage.Mobile)
          //  .Matches(new Regex(@"^[0-9]{10}$")).WithMessage(AppValidationMessage.Mobile); ;

    }
}