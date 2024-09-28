using App.EnglishBuddy.Application.Common.AppMessage;
using FluentValidation;

namespace App.EnglishBuddy.Application.Features.UserFeatures.NotificationToAllHandler;

public sealed class NotificationToAllValidator : AbstractValidator<NotificationToAllRequest>
{
    public NotificationToAllValidator()
    {
        
    }
}