using Abstraction.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sajad.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerManager answerManager;

        public AnswerController(IAnswerManager answerManager)
        {
            this.answerManager = answerManager ?? throw new ArgumentNullException(nameof(answerManager));
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> GetAnswersPerUserAsync(string userId)
        {
            var answers = await answerManager.GetAnswersPerUserAsync(userId).ConfigureAwait(false);
            return Ok(answers);
        }
    }
}
