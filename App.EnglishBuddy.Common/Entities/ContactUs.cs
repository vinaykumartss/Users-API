using App.EnglishBuddy.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.EnglishBuddy.Domain.Entities
{

    [Table("contact_us")]
    public class ContactUs : BaseEntity
    {
        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("mobile_no")]
        public string? MobileNo { get; set; }

        [Column("comments")]
        public string? Comments { get; set; }
    }
}
