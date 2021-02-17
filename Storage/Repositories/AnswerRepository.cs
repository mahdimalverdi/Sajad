using Abstraction.Models;
using Abstraction.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly SajadDbContext dbContext;

        public AnswerRepository(SajadDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AnswerStruct>> GetAnswersPerUserAsync(string userId)
        {
            var answers = await GetAnswers(userId).ConfigureAwait(false);
            var questionStructIds = answers.Select(q => q.QuestionStructId).ToList();
            var questions = await GetQuestions(questionStructIds).ConfigureAwait(false);
            var paragraphIds = questions.Select(q => q.Value.ParagraphId).ToList();
            var paragraphs = await GetParagraphs(paragraphIds).ConfigureAwait(false);
            var documentIds = paragraphs.Select(p => p.Value.DocumentId).ToList();
            var documents = await GetDocuments(documentIds).ConfigureAwait(false);
            var result = GetResult(answers, questions, paragraphs, documents);
            return result;
        }

        private static IEnumerable<AnswerStruct> GetResult(IEnumerable<Answer> answers, Dictionary<string, QuestionStruct> questions, Dictionary<string, Paragraph> paragraphs, Dictionary<string, Document> documents)
        {
            foreach (var answer in answers)
            {
                var answerStruct = GetAnswerStruct(questions, paragraphs, documents, answer);
                yield return answerStruct;
            }
        }

        private static AnswerStruct GetAnswerStruct(Dictionary<string, QuestionStruct> questions, Dictionary<string, Paragraph> paragraphs, Dictionary<string, Document> documents, Answer answer)
        {
            var question = questions[answer.QuestionStructId];
            var paragraph = paragraphs[question.ParagraphId];
            var document = documents[paragraph.DocumentId];

            var answerStruct = new AnswerStruct(
                answer.Id,
                question.Id,
                document.Title,
                paragraph.Context,
                question.Question,
                answer.Text,
                answer.IsImpossible);
            return answerStruct;
        }

        private async Task<Dictionary<string, Document>> GetDocuments(List<string> documentIds)
        {
            return await dbContext
                .Documents
                .Where(p => documentIds.Contains(p.Id))
                .ToDictionaryAsync(d => d.Id).ConfigureAwait(false);
        }

        private async Task<Dictionary<string, Paragraph>> GetParagraphs(List<string> paragraphIds)
        {
            return await dbContext
                .Paragraphs
                .Where(p => paragraphIds.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id).ConfigureAwait(false);
        }

        private async Task<Dictionary<string, QuestionStruct>> GetQuestions(List<string> answerIds)
        {
            return await dbContext
                   .QuestionStructs
                   .Include(q => q.Answers)
                    .Where(q => answerIds.Contains(q.Id))
                   .ToDictionaryAsync(q => q.Id)
                   .ConfigureAwait(false);
        }

        private async Task<IEnumerable<Answer>> GetAnswers(string userId)
        {
            return await dbContext
                .Answers
                .Where(q => q.UserId.Equals(userId))
                .ToListAsync().ConfigureAwait(false);
        }
    }
}
