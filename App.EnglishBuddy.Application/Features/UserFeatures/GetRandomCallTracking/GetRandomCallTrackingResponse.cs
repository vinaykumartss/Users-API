using System.Data;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetRandomCallTracking;

public sealed record GetRandomCallTrackingResponse
{
    public  Guid UserId { get; set;}
    public int Minutes { get; set; }
    public bool IsSuccess { get; set; } 
}