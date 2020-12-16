using Abstraction.Models;
using Abstraction.Repositories;
using Nest;
using Storage.Repositories.QuestionCommands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storage.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ElasticClient client;

        public QuestionRepository(ElasticClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task AddAsync(string paraphId, IEnumerable<QuestionStruct> questions)
        {
            foreach (var question in questions)
            {
                var command = new AddCommand(paraphId, question, client);
                await command.ExecuteAsync().ConfigureAwait(false);
            }
        }
    }
}
