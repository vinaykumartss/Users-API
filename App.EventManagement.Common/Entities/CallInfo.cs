
using App.EventManagement.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.EventManagement.Domain.Entities
{
    [Table("call_info")]
    public class CallInfo : BaseEntity
    {

        [Column("from_user_id")]
        public Guid FromUserId { get; set; }

        [Column("to_user_id")]
        public Guid ToUserId { get; set; }

        [Column("totaltime")]
        public string? TotalTime { get; set; }
        
    }
}
