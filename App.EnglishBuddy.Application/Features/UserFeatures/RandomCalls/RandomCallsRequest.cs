using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.RandomCalls
{

    public class RandomCallsRequest : IRequest<RandomCallsResponse>
    {
        public Guid UserId { get; set; }
        public string? Token { get; set; }
    }

    public class RandomCallsMatch
    {
     
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }

        public int Status { get; set; }
        public int Order  { get; set; }
        public Guid MeetingId { get; set; }
    }

}