using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.RandomCallEnd
{

    public class RandomCallEndRequest : IRequest<RandomCallEndResponse>
    {
        public Guid UserId { get; set; }
        public Guid MeetingId { get; set; }
        public int Status { get; set; }
    }
}