using System.Threading.Tasks;

namespace Abstraction.Managers
{
    public interface IDocumentManager
    {
        Task<int> GetCountAsync();
    }
}