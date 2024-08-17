namespace App.EnglishBuddy.Application.Features.UserFeatures.SaveMeetings;

public sealed record SaveMeetingsResponse
{
    public bool IsSuccess { get; set; }

    
    public string? Subject { get; set; }
   
    public Guid? UserId { get; set; }

    public Guid? MeetingId { get; set; }

    public int UserCount { get; set; }
}