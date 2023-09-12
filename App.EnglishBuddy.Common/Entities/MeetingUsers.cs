using App.EnglishBuddy.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.EnglishBuddy.Domain.Entities
{
    [Table("meetingsusers")]
    public class MeetingUsers : BaseEntity
    {
        [Column("userid")]
        public Guid UserId { get; set; }

        [Column("meeting_id")]
        public Guid MeetingId { get; set; }

        [Column("ismeetingAdmin")]
        public bool IsmeetingAdmin { get; set; }
    }
}
