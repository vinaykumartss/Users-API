using App.EnglishBuddy.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.EnglishBuddy.Domain.Entities
{


    [Table("meetingsids")]
    public class MeetingIds : BaseEntity
    {
        [Column("from_user_id")]
        public Guid? FromUserId { get; set; }

        [Column("to_user_id")]
        public Guid? ToUserId { get; set; }


        [Column("jitsi_id")]
        public Guid JitsiId { get; set; }

        [Column("status")]
        public int Status { get; set; }
    }
}
