using System;
using System.Collections.Generic;
using System.Text;

namespace Abstraction.Models
{
    public class AnswerStruct
    {
        public AnswerStruct(string answerId, string questionId, string title, string paragraph, string question, string answer)
        {
            AnswerId = answerId ?? throw new ArgumentNullException(nameof(answerId));
            QuestionId = questionId ?? throw new ArgumentNullException(nameof(questionId));
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Paragraph = paragraph ?? throw new ArgumentNullException(nameof(paragraph));
            Question = question ?? throw new ArgumentNullException(nameof(question));
            Answer = answer ?? throw new ArgumentNullException(nameof(answer));
        }

        public string AnswerId { get; }
        public string QuestionId { get; }
        public string Title { get; }
        public string Paragraph { get; }
        public string Question { get; }
        public string Answer { get; }
    }
}
