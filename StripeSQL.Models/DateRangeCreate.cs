using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeSQL.Models
{
    public class DateRangeCreate
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public TimeSpan Range
        {
            get
            {
                var actualRange = EndDate - StartDate;
                return actualRange;
            }
        }

        public int QuestionId { get; set; }
    }
}
