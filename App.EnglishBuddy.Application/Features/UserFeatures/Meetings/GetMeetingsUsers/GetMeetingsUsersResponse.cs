namespace App.EnglishBuddy.Application.Features.UserFeatures.GetMeetingsUsers;

public sealed record GetMeetingsUsersResponse
{
    public bool IsSuccess { get; set; }
    public int TotalUsers { get; set; }
}