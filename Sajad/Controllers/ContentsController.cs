using Abstraction.Managers;
using Abstraction.Models;
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
            await contentManager.UploadFileAsync(stream).ConfigureAwait(false);

            return Ok();
        }

        [HttpGet]
        public async Task<FileStreamResult> DownloadFileAsync()
        {
            var stream = await contentManager.DownloadFileAsync().ConfigureAwait(false);

            return File(stream, ContentType, FileDownloadName);
        }
    }
}
