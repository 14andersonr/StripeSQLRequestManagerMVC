using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeSQL.Models
{
    class SQLCodeCreate
    {
        [Key]
        public int SQLCodeId { get; set; }

        public int? QuestionId { get; set; }

        public int? DateRangeId { get; set; }

        [Required]
        public string SQL { get; set; }

        public DateTime? ResolvedDate { get; set; }

        [Required]
        public bool Resolved { get; set; }
    }
}
