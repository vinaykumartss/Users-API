using MediatR;
using System.Numerics;

namespace App.EventManagement.Application.Features.Query.GetUser
{
    public class GetUserRequest : IRequest<GetUserResponse>
    {
        public Guid Id { get; set; }
    }
}