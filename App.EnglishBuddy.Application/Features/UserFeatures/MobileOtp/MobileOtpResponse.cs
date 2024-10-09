namespace App.EnglishBuddy.Application.Features.UserFeatures.MobileOtp;

public sealed record MobileOtpResponse
{
    public bool IsSuccess { get; set; }
    public string Otp { get; set; }
}