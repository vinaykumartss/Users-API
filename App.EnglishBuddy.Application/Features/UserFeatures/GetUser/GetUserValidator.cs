using App.EnglishBuddy.Application.Common.AppMessage;
using FluentValidation;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetUser;

public sealed class GetUserValidator : AbstractValidator<GetUserRequest>
{
    public GetUserValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage(AppValidationMessage.UserIdEmptyMessage);
    }
}