using App.EnglishBuddy.Application.Features.UserFeatures.OtpTemplate;
using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.ContactUs
{

    public class ContactUsRequest : IRequest<ContactUsResponse>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Mobile { get; set; }
        public string? Question { get; set; }
        public string? EmailAddress { get; set; }



    }
}