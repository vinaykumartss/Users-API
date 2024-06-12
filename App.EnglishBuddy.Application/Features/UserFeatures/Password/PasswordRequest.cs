using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.Password
{

    public class PasswordRequest : IRequest<PasswordResponse>
    {
        public Guid? Id { get; set; }
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }

        public bool IsForgotPassword { get; set; }
    }
}