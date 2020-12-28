using Abstraction.Managers;
using Abstraction.Repositories;
using System;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class DocumentManager : IDocumentManager
    {
        private readonly IDocumentRepository documentRepository;

        public DocumentManager(IDocumentRepository documentRepository)
        {
            this.documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
        }

        public async Task<int> GetCountAsync()
        {
            return await documentRepository.GetCountAsync().ConfigureAwait(false);
        }
    }
}
