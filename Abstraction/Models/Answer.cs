using System;
using System.Collections.Generic;
using System.Text;

namespace Abstraction.Models
{
    public class Answer
    {
        public Answer()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public string Text { get; set; }
        public int AnswerStart { get; set; }
        public string QuestionStructId { get; set; }
    }
}
