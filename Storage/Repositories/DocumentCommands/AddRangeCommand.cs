using Abstraction.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Repositories.DocumentCommands
{
    internal class AddRangeCommand
    {
        private readonly ElasticClient client;
        private readonly IEnumerable<Document> documents;

        public AddRangeCommand(ElasticClient client, IEnumerable<Document> documents)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.documents = documents ?? throw new ArgumentNullException(nameof(documents));
        }

        public async Task ExecuteAsync()
        {
            var response = await client
                .BulkAsync(s => s.Index<Document>().IndexMany(documents))
                .ConfigureAwait(false);
        }
    }
}
