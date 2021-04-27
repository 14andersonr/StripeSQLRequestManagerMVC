using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeSQL.Models
{
    public class SQLCodeListItem
    {
        [Key]
        public int SQLCodeId { get; set; }

        public string SQL { get; set; }

        public DateTime? ResolvedDate { get; set; }

        public bool Resolved { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
