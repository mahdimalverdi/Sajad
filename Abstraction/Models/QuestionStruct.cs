using System;
using System.Collections.Generic;
using System.Text;

namespace Abstraction.Models
{
    public class QuestionStruct
    {
        public string Question { get; set; }

        public List<Answer> Answers { get; set; }
    }
}
