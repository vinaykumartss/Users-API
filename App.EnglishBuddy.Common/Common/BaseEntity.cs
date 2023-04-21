using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.EnglishBuddy.Domain.Common
{
    public abstract class BaseEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("createdby")]
        public Guid? Createdby { get; set; }

        [Column("updateddate")]
        public DateTime? UpdateDate { get; set; }

        [Column("updatedby")]
        public Guid? Updatedby { get; set; }

        [Column("isactive")]
        public bool IsActive { get; set; }

    }
}
