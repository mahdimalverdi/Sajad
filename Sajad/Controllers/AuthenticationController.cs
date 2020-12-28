using Abstraction.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sajad.Adapters;
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
    public class AuthenticationController : ControllerBase
    {
        protected readonly IAuthenticationManager manager;

        public AuthenticationController(IAuthenticationManager manager)
        {
            this.manager = manager ?? throw new ArgumentNullException(nameof(manager));
        }

        [AllowAnonymous]
        [HttpPost]
        public virtual async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var token = await this.manager.LoginAsync(model.UserName, model.Password);
                var result = new { token };

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> LogoutAsync()
        {
            await this.manager.LogoutAsync();
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> GetUsers()
        {
            var users = await manager.GetUsersAsync().ConfigureAwait(false);
            var adapter = new UserViewModelAdapter(users);
            return Ok(adapter.GetUsers());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await manager.RegisterAsync(model.UserName, model.Password).ConfigureAwait(false);
            return Ok();
        }
    }
}
