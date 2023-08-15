using App.EnglishBuddy.Domain.Entities;
using AutoMapper;

namespace App.EnglishBuddy.Application.Features.UserFeatures.RandomCalls;

public sealed class RandomCallsMapper : Profile
{
    public RandomCallsMapper()
    {
        CreateMap<RandomCallsRequest, App.EnglishBuddy.Domain.Entities.RandomCalls>();
        CreateMap<RandomCallsRequest, RandomCallsResponse>();
    }
}