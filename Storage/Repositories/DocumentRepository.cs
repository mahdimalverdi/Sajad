using Abstraction.Models;
using Abstraction.Repositories;
using Nest;
using Storage.Caching;
using Storage.Repositories.DocumentCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly ElasticClient client;

        public DocumentRepository(ElasticClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task AddRangeAsync(IEnumerable<Document> documents)
        {
            var command = new AddRangeCommand(client, documents);
            await command.ExecuteAsync().ConfigureAwait(false);

            ParagraphsCache.Instance.Ids.Clear();
        }

        public async Task<IEnumerable<Document>> GetAllAsync()
        {
            var command = new GetAllCommands(client);

            return await command.ExecuteAsync().ConfigureAwait(false);
        }
    }
}
