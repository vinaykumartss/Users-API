using App.EventManagement.Domain.Entities;
using AutoMapper;

namespace App.EventManagement.Application.Features.Comand.CreateUser;

public sealed class CreateUserMapper : Profile
{
    public CreateUserMapper()
    {
        CreateMap<CreateUserRequest, Users>();
        CreateMap<Users, CreateUserResponse>();
    }
}