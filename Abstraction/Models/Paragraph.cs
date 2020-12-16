using System;
using System.Collections.Generic;
using System.Text;

namespace Abstraction.Models
{
    public class Paragraph
    {
        public string Id { get; set; }
        public string Context { get; set; }
        public List<QuestionStruct> Questions { get; set; }
    }
}
