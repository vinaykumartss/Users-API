using App.EnglishBuddy.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.EnglishBuddy.Domain.Entities
{
    [Table("meetings")]
    public class Meetings : BaseEntity
    {
        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("subject")]
        public string? Subject { get; set; }

        [Column("start_time")]
        public string? StartTime { get; set; }

        [Column("start_date")]
        public DateTime? StartDate { get; set; }

        [Column("selected_hours")]
        public string? MeetingDurationHours { get; set; }

        [Column("selected_minutes")]
        public string? MeetingDurationMinutes { get; set; }

        [Column("start_ampm")]
        public string? StartAMPM { get; set; }

        [Column("meeting_id")]
        public Guid? MeetingId { get; set; }
        
    }
}
