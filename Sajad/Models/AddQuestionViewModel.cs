using Abstraction.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sajad.Models
{
    public class AddQuestionViewModel
    {
        [Required]
        public string ParagraphId { get; set; }

        [Required]
        public IEnumerable<QuestionStruct> Questions { get; set; }
    }
}
