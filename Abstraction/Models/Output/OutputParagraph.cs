using System;
using System.Collections.Generic;
using System.Text;

namespace Abstraction.Models.Output
{
    public class OutputParagraph
    {
        public string Context { get; set; }
        public List<OutputQuestionStruct> Qas { get; set; }
    }
}
