using App.EnglishBuddy.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.EnglishBuddy.Domain.Entities
{
    [Table("otp")]
    public class Otp : BaseEntity
    {
        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("otp")]
        public string? OTP { get; set; }

        [Column("type")]
        public string? Type { get; set; }

        //[ForeignKey(nameof(UserId))]
        //public Users? Users { get; set; }

    }
}
