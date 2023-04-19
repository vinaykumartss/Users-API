using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using App.EnglishBuddy.Domain.Common;
using System.Numerics;

namespace App.EnglishBuddy.Domain.Entities
{
    [Table("users")]
    public class Users: BaseEntity
    {
       
        [Column("email")]
        public string? Email { get; set; }

        [Column("phone")]
        public string? Mobile { get; set; }

        [Column("name")]
        public string? Name { get; set; }


        [Column("quick_blox_id")]
        public BigInteger? QuickBloxId { get; set; }


        [Column("otp")]
        public int? Otp { get; set; }


        [Column("otp_generated")]
        public int? OtpGenerated { get; set; }



    }
}
