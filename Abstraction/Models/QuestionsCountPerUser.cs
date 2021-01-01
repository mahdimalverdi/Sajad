using System;
using System.Collections.Generic;
using System.Text;

namespace Abstraction.Models
{
    public class QuestionsCountPerUser
    {
        public QuestionsCountPerUser(string userId, int count)
        {
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
            Count = count;
        }

        public string UserId { get; }
        public int Count { get; }
    }
}
