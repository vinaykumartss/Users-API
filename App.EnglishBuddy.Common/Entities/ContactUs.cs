using App.EnglishBuddy.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.EnglishBuddy.Domain.Entities
{

    [Table("contactus")]
    public class ContactUs : BaseEntity
    {
     
        [Column("firstname")]
        public string? FirstName { get; set; }

        [Column("lastname")]
        public string? LastName { get; set; }

        [Column("mobile")]
        public string? Mobile { get; set; }


        [Column("emailadress")]
        public string? EmailAdress { get; set; }

        [Column("question")]
        public string? Question { get; set; }

    }
}
