using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.EnglishBuddy.Application.Features.UserFeatures.FcmToken
{
    public class AccessTokenRequest : IRequest<AccessTokenResponse>
    {
        public string? FcmToken { get; set; }
        public Guid? UserId { get; set; }
    }
}