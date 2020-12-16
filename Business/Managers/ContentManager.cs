using Abstraction.Managers;
using Abstraction.Models;
using Abstraction.Models.Output;
using Abstraction.Repositories;
using Business.Adapters;
using Business.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class ContentManager : IContentManager
    {
        private readonly IDocumentRepository documentRepository;

        public ContentManager(IDocumentRepository documentRepository)
        {
            this.documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
        }

        public async Task UploadFileAsync(Stream stream)
        {
            var serializer = new InputContentSerializer(stream);
            var content = await serializer.DeserializeAsync().ConfigureAwait(false);
            var documents = content.Data.Select(GetDocument);
            await documentRepository.AddRangeAsync(documents).ConfigureAwait(false);
        }

        private Document GetDocument(InputDocument document)
        {
            return new DocumentAdapter(document).GetDocument();
        }

        public async Task<Stream> DownloadFileAsync()
        {
            var content = await GetContent().ConfigureAwait(false);

            var adapter = new OutputContentAdapter(content);
            var outputContent = adapter.Get();

            return await Serialize(outputContent).ConfigureAwait(false);
        }

        private static async Task<Stream> Serialize(OutputContent content)
        {
            var serializer = new OutputContentSerializer(content);
            var stream = await serializer.SerializeAsync().ConfigureAwait(false);
            return stream;
        }

        private async Task<Content> GetContent()
        {
            var documents = await documentRepository.GetAllAsync().ConfigureAwait(false);
            var content = new Content() { Data = documents.ToList() };
            return content;
        }
    }
}
