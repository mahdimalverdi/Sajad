using Abstraction.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Abstraction.Managers
{
    public interface IContentManager
    {
        Task AddRangeAsync(IEnumerable<Document> documents);
        Task<Content> GetContent();
    }
}