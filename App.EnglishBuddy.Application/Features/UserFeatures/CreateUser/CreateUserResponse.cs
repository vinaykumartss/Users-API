namespace App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;

public sealed record CreateUserResponse
{
    public Guid Id { get; set; }
    public bool IsSuccess { get; set; }
}



