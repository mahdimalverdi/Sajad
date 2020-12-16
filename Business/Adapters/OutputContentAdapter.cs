using Abstraction.Models;
using Abstraction.Models.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Adapters
{
    public class OutputContentAdapter
    {
        private readonly Content content;

        public OutputContentAdapter(Content content)
        {
            this.content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public OutputContent Get()
        {
            return new OutputContent()
            {
                Data = content.Data.Select(Get).Where(d => d.Paragraphs.Any()).ToList()
            };
        }

        private  OutputDocument Get(Document document)
        {
            var adapter = new OutputDocumentAdapter(document);

            return adapter.Get();
        }
    }
}
