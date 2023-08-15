using FluentValidation;

namespace App.EnglishBuddy.Application.Features.UserFeatures.SaveMeetings;

public sealed class SaveMeetingsValidator : AbstractValidator<SaveMeetingsRequest>
{
    public SaveMeetingsValidator()
    {
    }
}