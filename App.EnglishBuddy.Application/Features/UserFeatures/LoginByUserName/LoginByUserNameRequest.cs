using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.LoginByUserName
{

    public class LoginByUserNameRequest : IRequest<LoginByUserNameResponse>
    {
       public string? Password { get; set; }
       public string? Email { get; set; }

    }
}