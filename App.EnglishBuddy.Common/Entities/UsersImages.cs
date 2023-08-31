using App.EnglishBuddy.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.EnglishBuddy.Domain.Entities
{

    [Table("users_images")]
    public class UsersImages : BaseEntity
    {
        [Column("user_id")]
        public Guid UserId { get; set; }


        [Column("image_path")]
        public string? ImagePath { get; set; }
    }
}
