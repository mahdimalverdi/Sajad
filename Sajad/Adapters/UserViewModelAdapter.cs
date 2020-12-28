using Microsoft.AspNetCore.Identity;
using Sajad.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sajad.Adapters
{
    public class UserViewModelAdapter
    {
        private readonly IEnumerable<IdentityUser> identityUsers;

        public UserViewModelAdapter(IEnumerable<IdentityUser> identityUsers)
        {
            this.identityUsers = identityUsers ?? throw new ArgumentNullException(nameof(identityUsers));
        }

        public IEnumerable<UserViewModel> GetUsers()
        {
            return identityUsers.Select(GetUser);
        }

        private UserViewModel GetUser(IdentityUser user)
        {
            return new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName
            };
        }
    }
}
