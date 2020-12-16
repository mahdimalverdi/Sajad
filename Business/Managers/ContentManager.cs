using Abstraction.Managers;
using Abstraction.Models;
using Abstraction.Models.Output;
using Abstraction.Repositories;
using Business.Adapters;
using Business.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class ContentManager : IContentManager
    {
        private readonly IDocumentRepository documentRepository;

        public ContentManager(IDocumentRepository documentRepository)
        {
            this.documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
        }

        public async Task AddRangeAsync(IEnumerable<Document> documents)
        {
            await documentRepository.AddRangeAsync(documents).ConfigureAwait(false);
        }

        public async Task<Content> GetContent()
        {
            var documents = await documentRepository.GetAllAsync().ConfigureAwait(false);
            var content = new Content() { Data = documents.ToList() };
            return content;
        }
    }
}
