using App.EnglishBuddy.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.EnglishBuddy.Domain.Entities
{
    [Table("users")]
    public class Users: BaseEntity
    {
        [Column("password")]
        public string? Password { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("phone")]
        public string? Mobile { get; set; }

        [Column("first_name")]
        public string? FirstName { get; set; }

        [Column("last_name")]
        public string? LastName { get; set; }

        [Column("countryid")]
        public string? CountryId { get; set; }

        [Column("stateid")]
        public string? StateId { get; set; }

        [Column("cityid")]
        public string? CityId { get; set; }


        [Column("state_name")]
        public string? StateName { get; set; }

        [Column("city_name")]
        public string? CityName { get; set; }


        [Column("mobile_prefix")]
        public string? MobilePrefix { get; set; }

        [Column("isOtpVerify")]
        public bool? IsOtpVerify { get; set; }

    }
}
