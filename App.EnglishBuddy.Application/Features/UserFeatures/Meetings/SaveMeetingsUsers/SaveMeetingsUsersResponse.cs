namespace App.EnglishBuddy.Application.Features.UserFeatures.SaveMeetingsUsers;

public sealed record SaveMeetingsUsersResponse
{
    public bool IsSuccess { get; set; }
    public int TotalUsers { get; set; }
}