using App.EnglishBuddy.Application.Features.UserFeatures.OtpTemplate;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;

namespace App.EnglishBuddy.Application.Features.UserFeatures.ContactUs;

public sealed class ContactUsMapper : Profile
{
    public ContactUsMapper()
    {
        CreateMap<ContactUsRequest, Domain.Entities.ContactUs>();
        CreateMap<ContactUsRequest, ContactUsResponse>();
    }
}