using App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;
using MediatR;
using System.Numerics;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CreateUser
{

    public class CreateUserRequest : IRequest<CreateUserResponse>
    {
      
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? CountryCode { get; set; }

        public BigInteger? QuickBloxId { get; set; }

        public int? Otp { get; set; }

        public int? OtpGenerated { get; set; }
    }
}