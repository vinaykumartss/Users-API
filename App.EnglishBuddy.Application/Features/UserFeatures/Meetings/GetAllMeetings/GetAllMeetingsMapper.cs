using App.EnglishBuddy.Domain.Entities;
using AutoMapper;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetAllMeetings;

public sealed class GetAllMeetingsMapper : Profile
{
    public GetAllMeetingsMapper()
    {
        CreateMap<Meetings, GetAllMeetingsResponse>();
    }
}