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
    public class InputContentSerializer 
    {
        private readonly Stream stream;

        public InputContentSerializer(Stream stream)
        {
            this.stream = stream;
        }

        public async Task<OutputContent> DeserializeAsync()
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = new SnakeCaseNamingPolicy()
            };

            var result = await JsonSerializer.DeserializeAsync<OutputContent>(stream, options).ConfigureAwait(false);

            return result;
        }
    }
}
