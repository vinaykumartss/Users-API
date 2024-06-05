using App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;

namespace App.EnglishBuddy.Application.Features.UserFeatures.UpdateUser;

public sealed class UpdateUserMapper : Profile
{
    public UpdateUserMapper()
    {
        CreateMap<UpdateUserRequest, Users>();
        CreateMap<Users, UpdateUserResponse>();
    }
}