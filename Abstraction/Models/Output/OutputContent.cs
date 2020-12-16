using System;
using System.Collections.Generic;
using System.Text;

namespace Abstraction.Models.Output
{
    public class OutputContent
    {
        public string Version { get; } = "v2.0";
        public List<OutputDocument> Data { get; set; }
    }
}
