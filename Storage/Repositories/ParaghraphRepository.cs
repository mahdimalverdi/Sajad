using Abstraction.Models;
using Abstraction.Repositories;
using Nest;
using Storage.Caching;
using Storage.Repositories.ParaghraphCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Storage.Repositories
{
    public class ParaghraphRepository : IParaghraphRepository
    {
        private readonly ElasticClient client;
        private static readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        public ParaghraphRepository(ElasticClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<Document> GetParaghtaphAsync(string id)
        {
            var command = new GetCommand(client, id);

            return await command.ExecuteAsync().ConfigureAwait(false);
        }


        public async Task<IEnumerable<string>> GetAllIdsAsync()
        {
            if(!ParagraphsCache.Instance.Ids.Any())
            {
                await semaphoreSlim.WaitAsync().ConfigureAwait(false);
                try
                {
                    await StoreIds().ConfigureAwait(false);
                }
                finally
                {
                    semaphoreSlim.Release();
                }
            }

            return ParagraphsCache.Instance.Ids;
        }

        private async Task StoreIds()
        {
            if (!ParagraphsCache.Instance.Ids.Any())
            {
                var command = new GetAllIdsCommand(client);
                var ids = await command.ExecuteAsync().ConfigureAwait(false);
                ParagraphsCache.Instance.Ids.AddRange(ids);
            }
        }
    }
}
