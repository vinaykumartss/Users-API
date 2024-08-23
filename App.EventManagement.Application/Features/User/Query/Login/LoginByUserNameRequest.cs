using MediatR;

namespace App.EventManagement.Application.Features.UserFeatures.LoginByUserName
{

    public class LoginByUserNameRequest : IRequest<LoginByUserNameResponse>
    {
       public string? Password { get; set; }
       public string? UserName { get; set; }

    }
}