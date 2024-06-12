using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CallUsers
{

    public class OTPRequest : IRequest<OTPResponse>
    {
        public string? Email { get; set; }
        public int Code { get; set; }
    }
}