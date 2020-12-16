using Abstraction.Adapters;
using Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Adapters
{
    public class ParagraphAdapter : IParagraphAdapter
    {
        private readonly InputParagraph paragraph;

        public ParagraphAdapter(InputParagraph paragraph)
        {
            this.paragraph = paragraph ?? throw new ArgumentNullException(nameof(paragraph));
        }

        public Paragraph GetParagraph()
        {
            return new Paragraph()
            {
                Id = Guid.NewGuid().ToString(),
                Context = paragraph.Context.Trim()
            };
        }
    }
}
