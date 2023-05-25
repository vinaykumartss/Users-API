using App.EnglishBuddy.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.EnglishBuddy.Domain.Entities
{
    [Table("ratings")]
    public class Ratings : BaseEntity
    {
        [Column("rating")]
        public decimal Rating { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }


        [Column("touser_id")]
        public Guid ToUserId { get; set; }
        
    }
}
