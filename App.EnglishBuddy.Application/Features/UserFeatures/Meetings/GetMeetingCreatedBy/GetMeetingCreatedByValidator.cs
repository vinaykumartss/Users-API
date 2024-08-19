using FluentValidation;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetMeetingCreatedBy;

public sealed class GetMeetingCreatedByValidator : AbstractValidator<GetMeetingCreatedByRequest>
{
    public GetMeetingCreatedByValidator()
    {
    }
}