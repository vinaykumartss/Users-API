using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CreateUser
{
    public class CreateUserRequest : IRequest<CreateUserResponse>
    {
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CountryId { get; set; }
        public string? StateId { get; set; }
        public string? CityId { get; set; }
        public string? MobilePrefix { get; set; }
        public Guid? Id { get; set; }
    }
}