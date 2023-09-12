using App.EnglishBuddy.Domain.Entities;
using AutoMapper;


namespace App.EnglishBuddy.Application.Features.UserFeatures.SaveMeetingsUsers;

public sealed class SaveMeetingsUsersMapper : Profile
{
    public SaveMeetingsUsersMapper()
    {
        CreateMap<SaveMeetingsUsersRequest, MeetingUsers>();
    }
       
}