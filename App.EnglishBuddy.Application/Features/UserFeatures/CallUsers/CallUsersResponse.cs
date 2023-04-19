namespace App.EnglishBuddy.Application.Features.UserFeatures.CallUsers;

public sealed record CallUsersResponse
{
    public string? UserId { get; set; }
    public string? OpponentUserId { get; set; }
    public Guid? SessionId { get; set; }
    public bool CallInitiator { get; set; }

    public bool IsUserFound { get; set; }

   
}