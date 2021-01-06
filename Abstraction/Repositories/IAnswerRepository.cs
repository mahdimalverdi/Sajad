using Abstraction.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstraction.Repositories
{
    public interface IAnswerRepository
    {
        Task<IEnumerable<AnswerStruct>> GetAnswersPerUserAsync(string userId);
    }
}