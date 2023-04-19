using App.EnglishBuddy.Application.Common.AppMessage;
using FluentValidation;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;

public sealed class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Mobile).NotEmpty().MaximumLength(10).MaximumLength(10).WithMessage(AppValidationMessage.Mobile);
       
    }
}