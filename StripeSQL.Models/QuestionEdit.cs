using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeSQL.Models
{
    public class QuestionEdit
    {
        [Key]
        public int QuestionId { get; set; }

        public string Content { get; set; }
    }
}
