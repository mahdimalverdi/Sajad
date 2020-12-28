using Abstraction.Managers;
using Abstraction.Models;
using Abstraction.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class ParaghraphManager : IParaghraphManager
    {
        private readonly IParaghraphRepository repository;
        private static readonly Random random = new Random();

        public ParaghraphManager(IParaghraphRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Document> GetNextParagraphAsync()
        {
            var paragraphs = await repository.GetAllIdsAsync().ConfigureAwait(false);
            var paragraphIndex = random.Next(0, paragraphs.Count() - 1);
            var paragraphId = paragraphs.ElementAt(paragraphIndex);
            var paragraph = await repository.GetParaghtaphAsync(paragraphId).ConfigureAwait(false);
            return paragraph;
        }

        public async Task<int> GetCountAsync()
        {
            return await repository.GetCountAsync().ConfigureAwait(false);
        }
    }
}
