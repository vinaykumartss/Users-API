namespace App.EnglishBuddy.Application.Features.UserFeatures.UpdateUser;

public sealed record UpdateUserResponse
{
    public Guid Id { get; set; }
    public bool IsSuccess { get; set; }
}



