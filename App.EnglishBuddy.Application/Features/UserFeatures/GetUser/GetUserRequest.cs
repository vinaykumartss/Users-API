using MediatR;
using System.Numerics;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetUser
{

    public class GetUserRequest : IRequest<GetUserResponse>
    {
        public Guid Id { get; set; }
    }
}