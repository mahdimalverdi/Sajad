using Abstraction.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class AuthenticationManager : IAuthenticationManager
    {
        protected readonly UserManager<IdentityUser> userManager;
        protected readonly SignInManager<IdentityUser> signInManager;
        protected readonly IConfiguration configuration;

        public AuthenticationManager(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public virtual async Task ChangePasswordAsync(IdentityUser user, string password)
        {
            await userManager.RemovePasswordAsync(user);
            await userManager.AddPasswordAsync(user, password);
        }

        public virtual async Task ChangePasswordAsync(string userId, string password)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            await ChangePasswordAsync(user, password);
        }

        public virtual async Task ChangePasswordAsync(IdentityUser user, string currentPassword, string newPassword)
        {
            var isValid = await userManager.CheckPasswordAsync(user, currentPassword);

            if (!isValid)
                throw new Exception();

            var result = await userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            if (!result.Succeeded)
                throw new ArgumentException();
        }

        public virtual async Task ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await this.userManager.FindByIdAsync(userId);
            await ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task<IdentityUser> GetByUserNameAsync(string userName)
        {
            return await userManager.Users.SingleOrDefaultAsync(u => u.UserName.ToLower().Equals(userName.ToLower())).ConfigureAwait(false);
        }

        public async Task<IEnumerable<IdentityUser>> GetUsersAsync()
        {
            return await userManager.Users.ToListAsync().ConfigureAwait(false);
        }

        protected virtual SecurityToken GetToken(IdentityUser user)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Role, user.UserName),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("Id", user.Id)
            };

            string tokenKey = configuration["Token:Key"];
            byte[] tokenKeyEncoded = Encoding.ASCII.GetBytes(tokenKey);
            var key = new SymmetricSecurityKey(tokenKeyEncoded);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return token;
        }

        public async Task RegisterAsync(string userName, string password)
        {
            await userManager.CreateAsync(new IdentityUser() { UserName = userName }, password).ConfigureAwait(false);
        }

        public virtual async Task<string> LoginAsync(string userName, string password)
        {
            var user = await this.GetByUserNameAsync(userName);

            if (user is null || !(await CheckPasswordAsync(password, user)).Succeeded)
                throw new Exception("نام کاربری یا رمز عبور اشتباه است.");

            var token = GetToken(user);

            string stringToken = new JwtSecurityTokenHandler().WriteToken(token);

            return stringToken;
        }

        private async Task<SignInResult> CheckPasswordAsync(string password, IdentityUser user)
        {
            return await signInManager.CheckPasswordSignInAsync(user, password, false);
        }

        public virtual async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
