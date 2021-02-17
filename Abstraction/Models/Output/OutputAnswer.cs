using System;
using System.Collections.Generic;
using System.Text;

namespace Abstraction.Models.Output
{
    public class OutputAnswer
    {
        public string Text { get; set; }
        public int AnswerStart { get; set; }
        public bool IsImpossible { get; set; }
    }
}
