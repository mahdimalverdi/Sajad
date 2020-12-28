using Abstraction.Models;
using Abstraction.Models.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Adapters
{
    public class QuestionStructAdapter
    {
        private readonly OutputQuestionStruct questionStruct;

        public QuestionStructAdapter(OutputQuestionStruct questionStruct)
        {
            this.questionStruct = questionStruct ?? throw new ArgumentNullException(nameof(questionStruct));
        }

        public QuestionStruct Get()
        {
            return new QuestionStruct()
            {
                Question = questionStruct.Question,
                Id = Guid.NewGuid().ToString(),
                Answers = questionStruct.Answers.Select(GetAnswer).ToList()
            };
        }

        private Answer GetAnswer(OutputAnswer answer)
        {
            var adapter = new AnswerAdapter(answer);

            return adapter.Get();
        }
    }
}
