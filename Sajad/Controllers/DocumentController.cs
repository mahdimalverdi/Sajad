using Abstraction.Managers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sajad.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentManager documentManager;

        public DocumentController(IDocumentManager documentManager)
        {
            this.documentManager = documentManager;
        }

        [HttpPost]
        public async Task<IActionResult> GetCountAsync()
        {
            var count = await documentManager.GetCountAsync().ConfigureAwait(false);

            return Ok(count);
        }
    }
}
