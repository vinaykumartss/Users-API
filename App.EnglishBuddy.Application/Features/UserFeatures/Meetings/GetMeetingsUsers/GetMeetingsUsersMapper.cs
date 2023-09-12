using App.EnglishBuddy.Domain.Entities;
using AutoMapper;


namespace App.EnglishBuddy.Application.Features.UserFeatures.GetMeetingsUsers;

public sealed class GetMeetingsUsersMapper : Profile
{
    public GetMeetingsUsersMapper()
    {
        CreateMap<GetMeetingsUsersRequest, MeetingUsers>();
    }
       
}