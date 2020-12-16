using System.IO;
using System.Threading.Tasks;

namespace Abstraction.Managers
{
    public interface IContentManager
    {
        Task<Stream> DownloadFileAsync();
        Task UploadFileAsync(Stream stream);
    }
}