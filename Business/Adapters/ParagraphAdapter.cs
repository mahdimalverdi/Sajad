﻿using Abstraction.Adapters;
using Abstraction.Models;
using Abstraction.Models.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Adapters
{
    public class ParagraphAdapter : IParagraphAdapter
    {
        private readonly OutputParagraph paragraph;

        public ParagraphAdapter(OutputParagraph paragraph)
        {
            this.paragraph = paragraph ?? throw new ArgumentNullException(nameof(paragraph));
        }

        public Paragraph GetParagraph()
        {
            return new Paragraph()
            {
                Id = Guid.NewGuid().ToString(),
                Context = paragraph.Context.Trim(),
                Questions = paragraph.Qas.Select(Get).ToList()
            };
        }

        private QuestionStruct Get(OutputQuestionStruct questionStruct)
        {
            var adapter = new QuestionStructAdapter(questionStruct);
            return adapter.Get();
        }
    }
}
