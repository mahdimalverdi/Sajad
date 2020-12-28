using Abstraction.Models;
using Abstraction.Models.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Adapters
{
    public class OutputDocumentAdapter
    {
        private readonly Document document;

        public OutputDocumentAdapter(Document document)
        {
            this.document = document ?? throw new ArgumentNullException(nameof(document));
        }

        public OutputDocument Get()
        {
            return new OutputDocument()
            {
                Title = document.Title,
                Paragraphs = document.Paragraphs.Select(Get).Where(p => p.Qas.Any()).ToList()
            };
        }

        private OutputParagraph Get(Paragraph paragraph)
        {
            var adapter = new OutputParagraphAdapter(paragraph);

            return adapter.Get();
        }
    }
}
