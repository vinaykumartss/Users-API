using System.Data;

namespace App.EnglishBuddy.Application.Features.UserFeatures.RandomCallTrackings;

public sealed record RandomCallTrackingResponse
{
    public  Guid UserId { get; set;}
    public int Minutes { get; set; }
    public bool IsSuccess { get; set; } 
}