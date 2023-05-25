using App.EnglishBuddy.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.EnglishBuddy.Domain.Entities
{
    [Table("total_ratings")]
    public class TotalRatings : BaseEntity
    {

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("rating")]
        public decimal TotalRating { get; set; }
    }
}
