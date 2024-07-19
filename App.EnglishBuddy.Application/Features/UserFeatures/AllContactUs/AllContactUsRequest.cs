using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.AllContactUs
{

    public class AllContactUsRequest : IRequest<List<AllContactUsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public bool? IsActive { get; set; }


    }
}