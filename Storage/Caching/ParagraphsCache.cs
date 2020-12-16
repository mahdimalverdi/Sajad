using System;
using System.Collections.Generic;
using System.Text;

namespace Storage.Caching
{
    internal class ParagraphsCache
    {
        public static ParagraphsCache Instance { get; } = new ParagraphsCache();

        public List<string> Ids { get; } = new List<string>();


    }
}
