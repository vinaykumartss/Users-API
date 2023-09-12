using FluentValidation;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetMeetingsUsers;

public sealed class GetMeetingsUsersValidator : AbstractValidator<GetMeetingsUsersRequest>
{
    public GetMeetingsUsersValidator()
    {
    }
}