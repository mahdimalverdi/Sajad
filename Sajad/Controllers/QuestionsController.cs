using Abstraction.Managers;
using Abstraction.Models;
using Abstraction.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sajad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sajad.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionManager questionManager;

        public QuestionsController(IQuestionManager questionManager)
        {
            this.questionManager = questionManager ?? throw new ArgumentNullException(nameof(questionManager));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody]AddQuestionViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await questionManager.AddRangeAsync(viewModel.ParagraphId, viewModel.Questions).ConfigureAwait(false);

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCountAsync()
        {
            var count = await questionManager.GetCountAsync().ConfigureAwait(false);

            return Ok(count);
        }
    }
}
