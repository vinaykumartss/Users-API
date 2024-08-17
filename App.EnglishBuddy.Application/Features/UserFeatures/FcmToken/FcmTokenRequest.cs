using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.EnglishBuddy.Application.Features.UserFeatures.FcmToken
{
    public class FcmTokenRequest : IRequest<FcmTokenResponse>
    {
        public string? FcmToken { get; set; }
        public Guid? UserId { get; set; }
    }
}