using App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;

namespace App.EnglishBuddy.Application.Features.UserFeatures.UpdateProfile;

public sealed class UpdateProfileMapper : Profile
{
    public UpdateProfileMapper()
    {
        CreateMap<UpdateProfileRequest, Users>();
        CreateMap<Users, UpdateProfileResponse>();
    }
}