namespace App.EnglishBuddy.Application.Features.UserFeatures.RandomCalls;

public sealed record RandomCallsResponse
{
    public Guid? FromUserId { get; set; }

    public Guid? ToUserId { get; set; }
    public Guid? JistiId { get; set; }

    public Guid? CallId { get; set; }

    public int Status { get; set; }
}