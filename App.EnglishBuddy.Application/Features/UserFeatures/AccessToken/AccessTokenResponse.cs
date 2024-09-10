namespace App.EnglishBuddy.Application.Features.UserFeatures.FcmToken;

public sealed record AccessTokenResponse
{
    public string? AccessToken { get; set; }
    public bool IsSuccess { get; set; }
}



