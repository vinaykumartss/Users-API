namespace App.EnglishBuddy.Application.Features.UserFeatures.NotificationToAllHandler;

public sealed record NotificationToAllResponse
{
    public string? AccessToken { get; set; }
    public bool IsSuccess { get; set; }
}



