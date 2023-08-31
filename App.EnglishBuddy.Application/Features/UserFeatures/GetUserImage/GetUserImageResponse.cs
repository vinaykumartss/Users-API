using System.ComponentModel.DataAnnotations.Schema;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetUserImage;

public sealed record GetUserImageResponse
{
    public Guid UserId { get; set; }

    public string? ImagePath { get; set; }

}