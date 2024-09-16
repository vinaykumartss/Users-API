namespace App.EnglishBuddy.Application.Features.UserFeatures.GetAllMeetings;

public sealed record GetAllMeetingsResponse
{
   
    public string? Subject { get; set; }
   
    public Guid? UserId { get; set; }

    public Guid? MeetingId { get; set; }
    public bool UserIsActive { get; set; }
    
    public int UserCount { get; set; }

    public string? CreatedBy { get; set; }
    public string? ImagePath { get; set; }
}