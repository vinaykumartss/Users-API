using FluentValidation;

namespace App.EnglishBuddy.Application.Features.UserFeatures.SaveMeetings;

public sealed class GetAllMeetingsValidator : AbstractValidator<SaveMeetingsRequest>
{
    public GetAllMeetingsValidator()
    {
    }
}