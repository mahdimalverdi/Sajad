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
    [Authorize]
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

            string userId = GetUserId();

            foreach (var question in viewModel.Questions)
            {
                question.UserId = userId;
            }

            foreach (var answer in viewModel.Questions.SelectMany(q => q.Answers))
            {
                answer.UserId = userId;
            }

            await questionManager.AddRangeAsync(viewModel.ParagraphId, viewModel.Questions).ConfigureAwait(false);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> GetCountAsync()
        {
            var count = await questionManager.GetCountAsync().ConfigureAwait(false);

            return Ok(count);
        }
        private string GetUserId()
        {
            return HttpContext.User.Claims.Single(c => c.Type.Equals("Id")).Value;
        }
    }
}
