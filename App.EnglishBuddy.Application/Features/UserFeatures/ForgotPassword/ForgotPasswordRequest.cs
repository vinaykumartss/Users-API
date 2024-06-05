using App.EnglishBuddy.Application.Features.UserFeatures.ForgotPassword;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.EnglishBuddy.Application.Features.UserFeatures.ForgotPassword
{
    public class ForgotPasswordRequest : IRequest<ForgotPasswordResponse>
    {
        public string? Email { get; set; }
    }
}