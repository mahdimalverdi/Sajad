using Abstraction.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstraction.Repositories
{
    public interface IParaghraphRepository
    {
        Task<IEnumerable<string>> GetAllIdsAsync();
        Task<Document> GetParaghtaphAsync(string id);
    }
}