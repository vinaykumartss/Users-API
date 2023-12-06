namespace App.EnglishBuddy.Application.Features.UserFeatures.GetAllMeetings;

public sealed record GetAllMeetingsResponse
{
    public string? Name { get; set; }
    public string? Subject { get; set; }
    public string? StartTime { get; set; }
    public string? SelectedHours { get; set; }
    public string? SelectedMinutes { get; set; }
    public string? StartAmPm { get; set; }
    public Guid? UserId { get; set; }

    public Guid? MeetingId { get; set; }
    public bool UserIsActive { get; set; }
    
    public int UserCount { get; set; }

    public string? CreatedBy { get; set; }
}