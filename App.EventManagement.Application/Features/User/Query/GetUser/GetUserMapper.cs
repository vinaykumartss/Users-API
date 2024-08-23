using App.EventManagement.Domain.Entities;
using AutoMapper;

namespace App.EventManagement.Application.Features.Query.GetUser;

public sealed class GetUserMapper : Profile
{
    public GetUserMapper()
    {
        CreateMap<GetUserResponse, Users>();
        CreateMap<Users, GetUserResponse>();
    }
}