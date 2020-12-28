using Abstraction.Models;
using Abstraction.Repositories;
using Microsoft.EntityFrameworkCore;
using Storage.Caching;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storage.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly SajadDbContext dbContext;

        public DocumentRepository(SajadDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddRangeAsync(IEnumerable<Document> documents)
        {
            await dbContext.Documents.AddRangeAsync(documents).ConfigureAwait(false);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            ParagraphsCache.Instance.Ids.Clear();
        }

        public async Task<IEnumerable<Document>> GetAllAsync()
        {
            var result = await this.dbContext.Documents
                .Include(d => d.Paragraphs)
                .ThenInclude(p => p.Questions)
                .ThenInclude(q => q.Answers)
                .ToListAsync()
                .ConfigureAwait(false);

            return result;
        }

        public async Task<int> GetCountAsync()
        {
            var result = await dbContext.Documents.CountAsync().ConfigureAwait(false);

            return result;
        }
    }
}
