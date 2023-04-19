using App.EnglishBuddy.Domain.Entities;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;

public sealed record DeleteCallsResponse
{
     public bool Success { get; set; }

     public List<Calls> Result { get; set; }

}



