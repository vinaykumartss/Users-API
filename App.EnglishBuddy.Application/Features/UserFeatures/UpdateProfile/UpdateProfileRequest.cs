using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.EnglishBuddy.Application.Features.UserFeatures.UpdateProfile
{
    public class UpdateProfileRequest : IRequest<UpdateProfileResponse>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Guid? Id { get; set; }

        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }

        public string? StateId { get; set; }

        public string? CityId { get; set; }

        public int? Gender { get; set; }

        public string? Email { get; set; }

    }
}