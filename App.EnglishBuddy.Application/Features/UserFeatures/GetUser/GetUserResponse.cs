namespace App.EnglishBuddy.Application.Features.UserFeatures.GetUser;

public sealed record GetUserResponse
{
    public string? Email { get; set; }
    public string? Mobile { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CountryCode { get; set; }
    public string? CountryId { get; set; }
    public string? StateId { get; set; }

    public string? CityId { get; set; }
    public string? MobilePrefix { get; set; }
    public Guid? Id { get; set; }
    public string? StateName { get; set; }

    public string? CountryName { get; set; }

    public string? CityName { get; set; }
    public string? ImagePath { get; set; }
}



