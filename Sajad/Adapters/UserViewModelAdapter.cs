using Abstraction.Models;
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
        private readonly Dictionary<string, int> questionsCountPerUsers;

        public UserViewModelAdapter(IEnumerable<IdentityUser> identityUsers, IEnumerable<QuestionsCountPerUser> questionsCountPerUsers)
        {
            this.identityUsers = identityUsers ?? throw new ArgumentNullException(nameof(identityUsers));
            this.questionsCountPerUsers = questionsCountPerUsers?.ToDictionary(q => q.UserId, q => q.Count) ?? throw new ArgumentNullException(nameof(questionsCountPerUsers));
        }

        public IEnumerable<UserViewModel> GetUsers()
        {
            return identityUsers.Select(GetUser);
        }

        private UserViewModel GetUser(IdentityUser user)
        {
            if(!questionsCountPerUsers.TryGetValue(user.Id, out var questionCount))
            {
                questionCount = 0;
            }
            return new UserViewModel(user.UserName, user.Id, questionCount);
        }
    }
}
