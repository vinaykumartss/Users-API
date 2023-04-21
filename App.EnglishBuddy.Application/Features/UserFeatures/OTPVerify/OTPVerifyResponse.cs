namespace App.EnglishBuddy.Application.Features.UserFeatures.CallUsers;

public sealed record OTPVerifyResponse
{
    public string? Mobile { get; set; }
    public bool IsSuccess { get; set; }
}