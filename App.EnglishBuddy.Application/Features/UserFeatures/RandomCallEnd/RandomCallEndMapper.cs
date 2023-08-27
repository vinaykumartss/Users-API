using App.EnglishBuddy.Domain.Entities;
using AutoMapper;

namespace App.EnglishBuddy.Application.Features.UserFeatures.RandomCallEnd;

public sealed class RandomCallEndMapper : Profile
{
    public RandomCallEndMapper()
    {
        CreateMap<RandomCallEndRequest, App.EnglishBuddy.Domain.Entities.RandomUsers>();
        CreateMap<RandomCallEndRequest, RandomCallEndResponse>();
    }
}