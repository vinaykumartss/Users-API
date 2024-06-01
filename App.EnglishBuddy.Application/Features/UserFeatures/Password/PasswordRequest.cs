using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.Password
{

    public class PasswordRequest : IRequest<PasswordResponse>
    {
       public string? Password { get; set; }
       public string? Login { get; set; }
    }
}