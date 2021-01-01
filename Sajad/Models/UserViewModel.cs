using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sajad.Models
{
    public class UserViewModel
    {
        public UserViewModel(string userName, string id, int questionCount)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Id = id ?? throw new ArgumentNullException(nameof(id));
            QuestionCount = questionCount;
        }

        public string UserName { get;  }
        public string Id { get;  }
        public int QuestionCount { get; }
    }
}
