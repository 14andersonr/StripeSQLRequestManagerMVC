using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeSQL.Models
{
    class QuestionCreate
    {
        [Key]
        public int QuestionId { get; set; }

        [Required]
        [MaxLength(8000)]
        public string Content { get; set; }
    }
}
