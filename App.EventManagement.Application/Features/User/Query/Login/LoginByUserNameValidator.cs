using FluentValidation;

namespace App.EventManagement.Application.Features.UserFeatures.LoginByUserName;

public sealed class LoginByUserNameValidator : AbstractValidator<LoginByUserNameResponse>
{
    public LoginByUserNameValidator()
    {
        
    }
}