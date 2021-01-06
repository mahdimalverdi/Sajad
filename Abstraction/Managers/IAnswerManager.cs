using Abstraction.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstraction.Managers
{
    public interface IAnswerManager
    {
        Task<IEnumerable<AnswerStruct>> GetAnswersPerUserAsync(string userId);
    }
}