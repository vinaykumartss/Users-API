using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.SaveMeetings
{
    public class SaveMeetingsRequest : IRequest<SaveMeetingsResponse>
    {
        public Guid UserId { get; set; }
        
        public string? Subject { get; set; }
        
    }
}