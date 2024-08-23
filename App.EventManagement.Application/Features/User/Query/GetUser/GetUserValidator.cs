using App.EventManagement.Application.Common.AppMessage;
using FluentValidation;

namespace App.EventManagement.Application.Features.Query.GetUser;

public sealed class GetUserValidator : AbstractValidator<GetUserRequest>
{
    public GetUserValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage(AppValidationMessage.UserIdEmptyMessage);
    }
}