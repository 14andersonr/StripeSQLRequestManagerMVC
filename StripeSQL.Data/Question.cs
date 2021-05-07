using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeSQL.Data
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public string Content { get; set; }

        public virtual ICollection<SQLCode> SQLCollection { get; set; }

        public virtual ICollection<DateRange> DateRange { get; set; }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
