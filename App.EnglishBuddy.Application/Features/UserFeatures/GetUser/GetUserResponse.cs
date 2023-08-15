namespace App.EnglishBuddy.Application.Features.UserFeatures.GetUser;

public sealed record GetUserResponse
{
    public string? Email { get; set; }
    public string? Mobile { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CountryCode { get; set; }
    public string? Country { get; set; }
    public string? CallingCode { get; set; }
    public long? QuickBloxId { get; set; }
}



