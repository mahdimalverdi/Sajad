using Abstraction.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Repositories.QuestionCommands
{
    internal class AddCommand
    {
        private readonly string paraphId;
        private readonly QuestionStruct question;
        private const string Field = "paragraphs.id";
        private const string IdKey = "id";
        private const string QuestionKey = "question";
        private readonly ElasticClient client;

        private static readonly string script = @"
                for (int i = 0; i<ctx._source.paragraphs.size(); i++)
                {
                    if(ctx._source.paragraphs[i].id == params.id)
                    {
                        if(ctx._source.paragraphs[i].questions == null)
                        {
                            ctx._source.paragraphs[i].questions = [];
                        }
                        ctx._source.paragraphs[i].questions.add(params.question);
                    }
                }";

        public AddCommand(string paraphId, QuestionStruct question, ElasticClient client)
        {
            this.paraphId = paraphId ?? throw new ArgumentNullException(nameof(paraphId));
            this.question = question ?? throw new ArgumentNullException(nameof(question));
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task ExecuteAsync()
        {
            var response = await client.UpdateByQueryAsync<Document>(GetUpdateByQuery).ConfigureAwait(false);
        }

        private IScript GetScript(ScriptDescriptor descriptor)
        {
            var scriptParams = new Dictionary<string, object>
            {
                { IdKey, paraphId },
                { QuestionKey, question }
            };
            return descriptor.Source(script).Params(scriptParams);
        }

        private IUpdateByQueryRequest GetUpdateByQuery(UpdateByQueryDescriptor<Document> descriptor)
        {
            return descriptor.Query(GetQuery).Script(GetScript);
        }
        private QueryContainer GetQuery(QueryContainerDescriptor<Document> descriptor)
        {
            return descriptor.Nested(GetNestedQuery);
        }

        private INestedQuery GetNestedQuery(NestedQueryDescriptor<Document> descriptor)
        {
            return descriptor
                .Path(d => d.Paragraphs)
                .Query(q => q.Match(m => m.Field(Field).Query(paraphId)))
                .InnerHits();
        }
    }

}
