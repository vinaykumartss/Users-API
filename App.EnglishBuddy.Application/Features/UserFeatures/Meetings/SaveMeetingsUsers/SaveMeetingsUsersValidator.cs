using FluentValidation;

namespace App.EnglishBuddy.Application.Features.UserFeatures.SaveMeetingsUsers;

public sealed class SaveMeetingsUsersValidator : AbstractValidator<SaveMeetingsUsersRequest>
{
    public SaveMeetingsUsersValidator()
    {
    }
}