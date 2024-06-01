using FluentValidation;

namespace App.EnglishBuddy.Application.Features.UserFeatures.LoginByUserName;

public sealed class LoginByUserNameValidator : AbstractValidator<LoginByUserNameResponse>
{
    public LoginByUserNameValidator()
    {
        
    }
}