using Abstraction.Adapters;
using Abstraction.Models;
using Abstraction.Models.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Adapters
{
    public class DocumentAdapter : IDocumentAdapter
    {
        private readonly OutputDocument document;

        public DocumentAdapter(OutputDocument document)
        {
            this.document = document;
        }

        public Document GetDocument()
        {
            return new Document()
            {
                Id = Guid.NewGuid().ToString(),
                Title = document.Title, 
                Paragraphs = document.Paragraphs.Select(GetParagraph).ToList()
            };
        }

        private Paragraph GetParagraph(OutputParagraph paragraph)
        {
            return new ParagraphAdapter(paragraph).GetParagraph();
        }
    }
}
