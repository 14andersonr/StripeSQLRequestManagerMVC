using StripeSQL.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StripeSQL.Models
{
    public class QuestionListItem
    {
        [Key]
        public int QuestionId { get; set; }
        public IEnumerable<SQLCodeListItem> SQL { get; set; } = new List<SQLCodeListItem>();
        public IEnumerable<DateRangeListItem> DateRanges { get; set; } = new List<DateRangeListItem>();
        public string Content { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
