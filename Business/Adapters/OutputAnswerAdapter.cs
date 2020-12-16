using Abstraction.Models;
using Abstraction.Models.Output;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Adapters
{
    public class OutputAnswerAdapter
    {
        private readonly Answer answer;

        public OutputAnswerAdapter(Answer answer)
        {
            this.answer = answer ?? throw new ArgumentNullException(nameof(answer));
        }

        public OutputAnswer Get()
        {
            return new OutputAnswer()
            {
                AnswerStart = answer.AnswerStart,
                Text = answer.Text
            };
        }
    }
}
