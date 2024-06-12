using App.EnglishBuddy.Application.Common.AppMessage;
using FluentValidation;

namespace App.EnglishBuddy.Application.Features.UserFeatures.UpdateProfile;

public sealed class UpdateProfileValidator : AbstractValidator<UpdateProfileRequest>
{
    public UpdateProfileValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required");
    }
}