using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using App.EnglishBuddy.Application.Features.UserFeatures.CallUsers;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CallUsers;

public sealed class CallUsersHandlerMapper : Profile
{
    public CallUsersHandlerMapper()
    {
        CreateMap<CallUsersRequest, Calls>();
        CreateMap<Calls, CallUsersResponse>();
    }
}