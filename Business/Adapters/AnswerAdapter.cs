using Abstraction.Models;
using Abstraction.Models.Output;
using System;

namespace Business.Adapters
{
    internal class AnswerAdapter
    {
        private readonly OutputAnswer answer;

        public AnswerAdapter(OutputAnswer answer)
        {
            this.answer = answer;
        }

        internal Answer Get()
        {
            return new Answer()
            {
                AnswerStart = answer.AnswerStart,
                Text = answer.Text,
                Id = Guid.NewGuid().ToString()
            };
        }
    }
}