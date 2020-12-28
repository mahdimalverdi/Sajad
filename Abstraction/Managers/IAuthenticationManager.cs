using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Abstraction.Managers
{
    public interface IAuthenticationManager
    {
        Task ChangePasswordAsync(IdentityUser user, string password);
        Task ChangePasswordAsync(IdentityUser user, string currentPassword, string newPassword);
        Task ChangePasswordAsync(string userId, string password);
        Task ChangePasswordAsync(string userId, string currentPassword, string newPassword);
        Task<IdentityUser> GetByUserNameAsync(string userName);
        Task<string> LoginAsync(string userName, string password);
        Task LogoutAsync();
        Task RegisterAsync(string userName, string password);
    }
}