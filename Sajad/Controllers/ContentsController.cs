using Abstraction.Managers;
using Abstraction.Models;
using Abstraction.Models.Output;
using Abstraction.Repositories;
using Business.Adapters;
using Business.Serializer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Storage.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sajad.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContentsController : ControllerBase
    {
        private const string ContentType = "application/json";
        private const string FileDownloadName = "result.json";
        private readonly IContentManager contentManager;

        public ContentsController(IContentManager contentManager)
        {
            this.contentManager = contentManager ?? throw new ArgumentNullException(nameof(contentManager));
        }

        [HttpPost]
        public async Task<IActionResult> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var stream = file.OpenReadStream();
            var serializer = new InputContentSerializer(stream);
            var content = await serializer.DeserializeAsync().ConfigureAwait(false);
            var documents = content.Data.Select(GetDocument);

            await contentManager.AddRangeAsync(documents).ConfigureAwait(false);

            return Ok();
        }

        private Document GetDocument(InputDocument document)
        {
            return new DocumentAdapter(document).GetDocument();
        }

        [HttpGet]
        public async Task<FileStreamResult> DownloadFileAsync()
        {
            var content = await contentManager.GetContent().ConfigureAwait(false);

            var adapter = new OutputContentAdapter(content);
            var outputContent = adapter.Get();

            var stream = await Serialize(outputContent).ConfigureAwait(false);

            return File(stream, ContentType, FileDownloadName);
        }

        private static async Task<Stream> Serialize(OutputContent content)
        {
            var serializer = new OutputContentSerializer(content);
            var stream = await serializer.SerializeAsync().ConfigureAwait(false);
            return stream;
        }
    }
}
