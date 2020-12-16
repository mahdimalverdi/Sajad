using Abstraction.Managers;
using Microsoft.AspNetCore.Mvc;
using Sajad.Adapters;
using System;
using System.Threading.Tasks;

namespace Sajad.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ParaghraphController : ControllerBase
    {
        private readonly IParaghraphManager paraghraphManager;

        public ParaghraphController(IParaghraphManager paraghraphManager)
        {
            this.paraghraphManager = paraghraphManager ?? throw new ArgumentNullException(nameof(paraghraphManager));
        }

        [HttpPost]
        public async Task<IActionResult> GetNextParaghraphAsync()
        {
            var document = await paraghraphManager.GetNextParagraphAsync().ConfigureAwait(false);

            var adapter = new QuestionContentViewModelAdapter(document);
            var result = adapter.GetQuestionContent();

            return Ok(result);
        }
    }
}
