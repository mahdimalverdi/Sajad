using Abstraction.Managers;
using Abstraction.Models;
using Abstraction.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class QuestionManager : IQuestionManager
    {
        private readonly IQuestionRepository questionRepository;

        public QuestionManager(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
        }

        public async Task AddRangeAsync(string paragraphId, IEnumerable<QuestionStruct> questions)
        {
            await questionRepository
                .AddAsync(paragraphId, questions)
                .ConfigureAwait(false);
        }

        public async Task<int> GetCountAsync()
        {
            return await questionRepository.GetCountAsync().ConfigureAwait(false);
        }
    }
}
