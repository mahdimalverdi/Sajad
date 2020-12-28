using Abstraction.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstraction.Repositories
{
    public interface IQuestionRepository
    {
        Task AddAsync(string paraphId, IEnumerable<QuestionStruct> questions);
        Task<int> GetCountAsync();
    }
}