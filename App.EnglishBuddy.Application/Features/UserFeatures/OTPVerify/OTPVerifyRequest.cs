using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CallUsers
{

    public class OTPVerifyRequest : IRequest<OTPVerifyResponse>
    {
        public string? Email { get; set; }

        public string? OTP { get; set; }
    }
}