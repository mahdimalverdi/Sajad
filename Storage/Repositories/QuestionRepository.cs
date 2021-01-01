using Abstraction.Models;
using Abstraction.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly SajadDbContext dbContext;

        public QuestionRepository(SajadDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddAsync(string paraphId, IEnumerable<QuestionStruct> questions)
        {
            foreach(var question in questions)
            {
                question.ParagraphId = paraphId;
            }
            await dbContext.QuestionStructs.AddRangeAsync(questions).ConfigureAwait(false);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<int> GetCountAsync()
        {
            var result = await dbContext.QuestionStructs.CountAsync().ConfigureAwait(false);

            return result;
        }

        public async Task<IReadOnlyList<QuestionsCountPerUser>> GetQuestionsCountPerUsersAsync()
        {
            return await dbContext
                .QuestionStructs
                .GroupBy(q => q.UserId)
                .Where(q => q.Key != null)
                .Select(g => new QuestionsCountPerUser(g.Key, g.Count()))
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
