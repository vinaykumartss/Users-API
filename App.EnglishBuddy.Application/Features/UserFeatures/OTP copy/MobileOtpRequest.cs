using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.MobileOtp
{

    public class MobileOtpRequest : IRequest<MobileOtpResponse>
    {
        public string? Mobile { get; set; }
        public bool IsResend { get; set; }
        
    }
}