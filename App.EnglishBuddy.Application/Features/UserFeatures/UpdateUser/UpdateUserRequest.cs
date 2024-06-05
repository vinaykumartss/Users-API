using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.EnglishBuddy.Application.Features.UserFeatures.UpdateUser
{
    public class UpdateUserRequest : IRequest<UpdateUserResponse>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Guid? Id { get; set; }

        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }

    }
}