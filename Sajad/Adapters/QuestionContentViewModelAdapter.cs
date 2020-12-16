using Abstraction.Models;
using Sajad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sajad.Adapters
{
    public class QuestionContentViewModelAdapter
    {
        private readonly Document document;

        public QuestionContentViewModelAdapter(Document document)
        {
            this.document = document ?? throw new ArgumentNullException(nameof(document));
        }

        public QuestionContentViewModel GetQuestionContent()
        {
            var result = new QuestionContentViewModel()
            {
                Title = document.Title,
                Context = document.Paragraphs.Single().Context,
                ParagraphId = document.Paragraphs.Single().Id
            };

            return result;
        }
    }
}
