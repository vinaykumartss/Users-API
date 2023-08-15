using MediatR;

namespace App.EnglishBuddy.Application.Features.UserFeatures.SaveMeetings
{
    public class SaveMeetingsRequest : IRequest<SaveMeetingsResponse>
    {
        public Guid UserId { get; set; }
        public string? Name { get; set; }
        public string? Subject { get; set; }
        public DateTime? StartDate { get; set; }
        public string? StartTime { get; set; }
        public string? MeetingDurationHours { get; set; }
        public string? MeetingDurationMinutes { get; set; }
        public string? StartAMPM { get; set; }
    }
}