using Abstraction.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Repositories.ParaghraphCommands
{
    internal class GetAllIdsCommand
    {
        private const int From = 0;
        private const int Size = 10000;
        private const string Scroll = "100m";
        private readonly ElasticClient client;
        private ISearchResponse<Document> searchResponse;

        public GetAllIdsCommand(ElasticClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<string>> ExecuteAsync()
        {
            await SetSearchResponse().ConfigureAwait(false);

            var result = await GetDocumentsAsync().ToListAsync().ConfigureAwait(false);

            return result.SelectMany(e => e).SelectMany(d => d.Paragraphs).Select(p => p.Id);
        }

        private async Task SetSearchResponse()
        {
            searchResponse = await client.SearchAsync<Document>(s =>
                        s.From(From)
                        .Size(Size)
                        .MatchAll()
                        .Scroll(Scroll)).ConfigureAwait(false);
        }

        public async IAsyncEnumerable<IEnumerable<Document>> GetDocumentsAsync()
        {
            while (searchResponse.Documents.Any())
            {
                yield return searchResponse.Documents;
                searchResponse = await client.ScrollAsync<Document>(Scroll, searchResponse.ScrollId).ConfigureAwait(false);
            }
        }
    }
}
