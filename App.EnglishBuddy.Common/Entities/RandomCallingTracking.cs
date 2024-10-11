using App.EnglishBuddy.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.EnglishBuddy.Domain.Entities
{
    [Table("random_call_tracking")]
    public class RandomCallingTracking : BaseEntity
    {

        [Column("userId")]
        public Guid UserId { get; set; }

        [Column("to_user_id")]
        public Guid ToUserId { get; set; }

        [Column("minutes")]
        public int Minutes { get; set; }
        
    }
}
