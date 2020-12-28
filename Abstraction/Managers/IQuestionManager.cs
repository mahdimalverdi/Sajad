using Abstraction.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstraction.Managers
{
    public interface IQuestionManager
    {
        Task AddRangeAsync(string paragraphId, IEnumerable<QuestionStruct> questions);
        Task<int> GetCountAsync();
    }
}