using System.Data;

namespace App.EnglishBuddy.Application.Features.UserFeatures.AllContactUs;

public sealed record AllContactUsResponse
{
    public string? Name { get; set; }
    public string? Mobile { get; set; }
    public string? Email { get; set; }
    public DateTime? Created { get; set; }
    public string? Message { get; set; }
}