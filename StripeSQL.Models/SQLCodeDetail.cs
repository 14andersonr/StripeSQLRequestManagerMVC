using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeSQL.Models
{
    class SQLCodeDetail
    {
        [Key]
        public int SQLCodeId { get; set; }

        public string SQL { get; set; }

        public DateTime? ResolvedDate { get; set; }

        public bool Resolved { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
