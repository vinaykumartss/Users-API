using App.EnglishBuddy.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.EnglishBuddy.Domain.Entities
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
