using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeSQL.Data
{
    public class SQLCode
    {
        [Key]
        public int SQLCodeId { get; set; }

        [ForeignKey(nameof(Question))] //ForeignKey has a nameof and a public virtual.
        public int? QuestionId { get; set; }
        public virtual Question Question { get; set; }

        [ForeignKey(nameof(DateRange))] //ForeignKey has a nameof and a public virtual.
        public int? DateRangeId { get; set; }
        public virtual DateRange DateRange { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public string SQL { get; set; }

        public DateTime? ResolvedDate { get; set; }

        [Required]
        public bool Resolved { get; set; }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
