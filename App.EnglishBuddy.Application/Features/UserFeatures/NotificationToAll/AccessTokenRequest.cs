using App.EnglishBuddy.Application.Features.UserFeatures.FcmToken;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.EnglishBuddy.Application.Features.UserFeatures.NotificationToAllHandler
{
    public class NotificationToAllRequest : IRequest<NotificationToAllResponse>
    {
        public string? FcmToken { get; set; }
        public Guid? UserId { get; set; }
    }
}