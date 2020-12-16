using Abstraction.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Repositories.ParaghraphCommands
{
    internal class GetCommand
    {
        private const string Field = "paragraphs.id";
        private const string ParaghraphFields = "paragraphs";
        private readonly ElasticClient client;
        private readonly string id;

        public GetCommand(ElasticClient client, string id)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.id = id ?? throw new ArgumentNullException(nameof(id));
        }

        public async Task<Document> ExecuteAsync()
        {
            var response = await client.SearchAsync<Document>(r =>
            r.From(0)
            .Size(1)
            .Source(GetSourceFilter)
            .Query(GetQuery)).ConfigureAwait(false);
            Document document = GetDocument(response);

            return document;
        }

        private static Document GetDocument(ISearchResponse<Document> response)
        {
            var document = response.Documents.First();
            var paragraph = response.Hits.First()
                .InnerHits[ParaghraphFields].Hits.Hits.First().Source.As<Paragraph>();
            document.Paragraphs = new List<Paragraph>() { paragraph };
            return document;
        }

        private ISourceFilter GetSourceFilter(SourceFilterDescriptor<Document> descriptor)
        {
            return descriptor.Includes(i => i.Fields(d => d.Title));
        }

        private QueryContainer GetQuery(QueryContainerDescriptor<Document> descriptor)
        {
            return descriptor.Nested(GetNestedQuery);
        }

        private INestedQuery GetNestedQuery(NestedQueryDescriptor<Document> descriptor)
        {
            return descriptor
                .Path(d => d.Paragraphs)
                .Query(q => q.Match(m => m.Field(Field).Query(id)))
                .InnerHits();
        }
    }
}
