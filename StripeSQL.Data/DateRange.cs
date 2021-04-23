using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeSQL.Data
{
    public class DateRange
    {
        [Key]
        public int DateRangeId { get; set; }

        [ForeignKey(nameof(Question))] //ForeignKey has a nameof and a public virtual.
        public int? QuestionId { get; set; }

        public virtual Question Question { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public DateRange Range { get; set; } //Needs a method.

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
