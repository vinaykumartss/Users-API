using App.EnglishBuddy.Domain.Entities;
using AutoMapper;

namespace App.EnglishBuddy.Application.Features.UserFeatures.AllContactUs;

public sealed class AllContactUsMapper : Profile
{
    public AllContactUsMapper()
    {
        CreateMap<Domain.Entities.ContactUs, AllContactUsResponse>()
        .ForMember(dest =>
            dest.Name,
            opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
    }
}