using Abstraction.Models;
using Abstraction.Models.Output;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Business.Serializer
{
    public class OutputContentSerializer
    {
        private readonly OutputContent content;

        public OutputContentSerializer(OutputContent content)
        {
            this.content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public async Task<Stream> SerializeAsync()
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = new SnakeCaseNamingPolicy()
            };
            var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, content, options).ConfigureAwait(false);
            stream.Position = 0;
            return stream;
        }
    }
}
