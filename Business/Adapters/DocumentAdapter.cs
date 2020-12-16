﻿using Abstraction.Adapters;
using Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Adapters
{
    public class DocumentAdapter : IDocumentAdapter
    {
        private readonly InputDocument document;

        public DocumentAdapter(InputDocument document)
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

        private Paragraph GetParagraph(InputParagraph paragraph)
        {
            return new ParagraphAdapter(paragraph).GetParagraph();
        }
    }
}
