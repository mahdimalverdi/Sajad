using System;
using System.Collections.Generic;
using System.Text;

namespace Abstraction.Models
{
    public class Document
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public List<Paragraph> Paragraphs { get; set; }
    }
}
