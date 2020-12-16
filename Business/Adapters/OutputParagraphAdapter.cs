using Abstraction.Models;
using Abstraction.Models.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Adapters
{
    public class OutputParagraphAdapter
    {
        private readonly Paragraph paragraph;

        public OutputParagraphAdapter(Paragraph paragraph)
        {
            this.paragraph = paragraph ?? throw new ArgumentNullException(nameof(paragraph));
        }

        public OutputParagraph Get()
        {
            return new OutputParagraph()
            {
                Context = paragraph.Context,
                Qas = paragraph.Questions?.Select(Get)?.ToList() ?? new List<OutputQuestionStruct>()
            };
        }

        private OutputQuestionStruct Get(QuestionStruct questionStruct)
        {
            var adapter = new OutputQuestionStructAdapter(questionStruct);
            return adapter.Get();
        }
    }
}
