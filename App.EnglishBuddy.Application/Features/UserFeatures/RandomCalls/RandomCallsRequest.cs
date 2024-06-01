using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.RandomCalls
{

    public class RandomCallsRequest : IRequest<RandomCallsResponse>
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
    }
}