using App.EnglishBuddy.Domain.Entities;
using AutoMapper;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetUserImage;

public sealed class GetUserImageMapper : Profile
{
    public GetUserImageMapper()
    {
        CreateMap<GetUserImageRequest, App.EnglishBuddy.Domain.Entities.UsersImages>();
        CreateMap<GetUserImageRequest, GetUserImageResponse>();
    }
}