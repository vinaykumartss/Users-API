using App.EnglishBuddy.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.EnglishBuddy.Domain.Entities
{
    [Table("calls")]
    public class Calls : BaseEntity
    {
        [Column("user_id")]
        public string? UserId { get; set; }

        [Column("oponent_user_id")]
        public string? OpponentUserId { get; set; }

        [Column("call_initiator")]
        public bool CallInitiator { get; set; }


        [Column("is_session")]
        public bool? IsSession { get; set; }

        [Column("session_id")]
        public Guid? SessionId { get; set; }

        [Column("session_created_by")]
        public string? SessionCreatedBy { get; set; }
    }
}
