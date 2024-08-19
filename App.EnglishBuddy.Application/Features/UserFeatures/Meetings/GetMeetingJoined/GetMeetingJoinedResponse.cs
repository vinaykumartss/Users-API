namespace App.EnglishBuddy.Application.Features.UserFeatures.GetMeetingJoined;

public sealed record GetMeetingJoinedResponse
{
    public bool IsSuccess { get; set; }

    public int Total { get; set; }


}