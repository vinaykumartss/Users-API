using FluentValidation;

namespace App.EnglishBuddy.Application.Features.UserFeatures.Password;

public sealed class PasswordValidator : AbstractValidator<PasswordResponse>
{
    public PasswordValidator()
    {
        
    }
}