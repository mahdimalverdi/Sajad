using System;
using System.Collections.Generic;
using System.Text;

namespace Abstraction.Models.Output
{
    public class OutputQuestionStruct
    {
        public string Id { get; set; }
        public string Question { get; set; }
        public bool IsImpossible { get; } = false;

        public List<OutputAnswer> Answers { get; set; }
    }
}
