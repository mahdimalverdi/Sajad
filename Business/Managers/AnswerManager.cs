using Abstraction.Managers;
using Abstraction.Models;
using Abstraction.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class AnswerManager : IAnswerManager
    {
        private readonly IAnswerRepository answerRepository;

        public AnswerManager(IAnswerRepository answerRepository)
        {
            this.answerRepository = answerRepository ?? throw new ArgumentNullException(nameof(answerRepository));
        }

        public async Task<IEnumerable<AnswerStruct>> GetAnswersPerUserAsync(string userId)
        {
            return await answerRepository.GetAnswersPerUserAsync(userId).ConfigureAwait(false);
        }
    }
}
