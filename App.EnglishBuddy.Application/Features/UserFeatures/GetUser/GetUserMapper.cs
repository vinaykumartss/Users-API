using App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetUser;

public sealed class GetUserMapper : Profile
{
    public GetUserMapper()
    {
        CreateMap<GetUserResponse, Users>();
        CreateMap<Users, GetUserResponse>();
    }
}