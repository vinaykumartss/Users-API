using App.EnglishBuddy.Domain.Entities;
using AutoMapper;

namespace App.EnglishBuddy.Application.Features.UserFeatures.UsersImages;

public sealed class UsersImagesMapper : Profile
{
    public UsersImagesMapper()
    {
        CreateMap<UsersImagesRequest, App.EnglishBuddy.Domain.Entities.UsersImages>();
        CreateMap<UsersImagesRequest, UsersImagesResponse>();
    }
}