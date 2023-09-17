using App.EnglishBuddy.Domain.Entities;
using AutoMapper;


namespace App.EnglishBuddy.Application.Features.UserFeatures.SaveMeetings;

public sealed class SaveMeetingsMapper : Profile
{
    public SaveMeetingsMapper()
    {
        CreateMap<SaveMeetingsRequest, Meetings>();
        CreateMap<SaveMeetingsRequest, SaveMeetingsResponse>();
    }
       
}