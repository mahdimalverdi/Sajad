using Abstraction.Models;
using System.Threading.Tasks;

namespace Abstraction.Managers
{
    public interface IParaghraphManager
    {
        Task<int> GetCountAsync();
        Task<Document> GetNextParagraphAsync();
    }
}