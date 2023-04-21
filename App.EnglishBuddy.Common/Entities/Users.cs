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
        //public Users() {
        //    Otps = new HashSet<Otp>();
        //}

        [Column("email")]
        public string? Email { get; set; }

        [Column("phone")]
        public string? Mobile { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("quick_blox_id")]
        public long? QuickBloxId { get; set; }

        //public ICollection<Otp>? Otps { get; set; }

    }
}
