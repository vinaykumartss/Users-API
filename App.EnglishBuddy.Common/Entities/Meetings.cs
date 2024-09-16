using App.EnglishBuddy.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.EnglishBuddy.Domain.Entities
{
    [Table("meetings")]
    public class Meetings : BaseEntity
    {
        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("subject")]
        public string? Subject { get; set; }

        [Column("meeting_id")]
        public Guid MeetingId { get; set; }
    }
}
