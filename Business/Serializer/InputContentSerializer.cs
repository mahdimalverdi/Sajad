using Abstraction.Models;
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

        public async Task<InputContent> DeserializeAsync()
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var result = await JsonSerializer.DeserializeAsync<InputContent>(stream, options).ConfigureAwait(false);

            return result;
        }
    }
}
