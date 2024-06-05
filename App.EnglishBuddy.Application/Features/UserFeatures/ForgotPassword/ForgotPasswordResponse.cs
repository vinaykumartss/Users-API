namespace App.EnglishBuddy.Application.Features.UserFeatures.ForgotPassword;

public sealed record ForgotPasswordResponse
{
    public Guid Id { get; set; }
    public bool IsSuccess { get; set; }
}



