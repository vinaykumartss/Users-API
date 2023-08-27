using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.ContactUs
{

    public class ContactUsRequest : IRequest<ContactUsResponse>
    {
        public string? Mobile { get; set; }
        public string? Comments { get; set; }
    }
}