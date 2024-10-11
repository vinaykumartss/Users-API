namespace App.EnglishBuddy.Application.Features.UserFeatures.UpdateProfile;

public sealed record UpdateProfileResponse
{
    public Guid Id { get; set; }
    public bool IsSuccess { get; set; }
    public string Name { get; set; }
}



