using System;
using System.Collections.Generic;
using System.Text;

namespace Abstraction.Models.Output
{
    public class OutputDocument
    {
        public string Title { get; set; }
        public List<OutputParagraph> Paragraphs { get; set; }
    }
}
