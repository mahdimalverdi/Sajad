using Abstraction.Models;
using Abstraction.Repositories;
using Storage.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Storage.Repositories
{
    public class ParaghraphRepository : IParaghraphRepository
    {
        private readonly SajadDbContext dbContext;
        private static readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        public ParaghraphRepository(SajadDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Document> GetParaghtaphAsync(string id)
        {
            var document = await dbContext.Documents
                .Include(d => d.Paragraphs)
                .SingleAsync(d => d.Paragraphs.Any(p => p.Id.Equals(id)))
                .ConfigureAwait(false);

            document.Paragraphs = document.Paragraphs.Where(p => p.Id.Equals(id)).ToList();

            return document;
        }

        public async Task<IEnumerable<string>> GetAllIdsAsync()
        {
            if(!ParagraphsCache.Instance.Ids.Any())
            {
                await semaphoreSlim.WaitAsync().ConfigureAwait(false);
                try
                {
                    await StoreIdsAsync().ConfigureAwait(false);
                }
                finally
                {
                    semaphoreSlim.Release();
                }
            }

            return ParagraphsCache.Instance.Ids;
        }

        public async Task<int> GetCountAsync()
        {
            var result = await dbContext.Paragraphs.CountAsync().ConfigureAwait(false);

            return result;
        }

        private async Task StoreIdsAsync()
        {
            if (!ParagraphsCache.Instance.Ids.Any())
            {
                var ids = await dbContext.Paragraphs.Select(p => p.Id).ToListAsync().ConfigureAwait(false);
                ParagraphsCache.Instance.Ids.AddRange(ids);
            }
        }
    }
}
