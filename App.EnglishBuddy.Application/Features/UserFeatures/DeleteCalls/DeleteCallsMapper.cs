using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;

public sealed class DeleteCallsMapper : Profile
{
    public DeleteCallsMapper()
    {
        CreateMap<CreateUserRequest, Users>();
        CreateMap<Users, CreateUserResponse>();
    }
}