using App.EnglishBuddy.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.EnglishBuddy.Domain.Entities
{
    [Table("Friend")]
    public class Friend : BaseEntity
    {

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("to_user_id")]
        public Guid ToUserId { get; set; }
    }
}
