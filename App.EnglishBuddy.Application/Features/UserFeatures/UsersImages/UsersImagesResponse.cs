namespace App.EnglishBuddy.Application.Features.UserFeatures.UsersImages;

public sealed record UsersImagesResponse
{
    public Guid UserId { get; set; }
    public string? ImagePath { get; set; }
    public bool IsSuccess { get; set; }
}