using Abstraction.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstraction.Repositories
{
    public interface IDocumentRepository
    {
        Task AddRangeAsync(IEnumerable<Document> documents);
        Task<IEnumerable<Document>> GetAllAsync();
    }
}