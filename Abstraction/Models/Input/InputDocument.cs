using System;
using System.Collections.Generic;
using System.Text;

namespace Abstraction.Models
{
    public class InputDocument
    {
        public string Title { get; set; }
        public List<InputParagraph> Paragraphs { get; set; }
    }
}
