using Abstraction.Models;
using Abstraction.Models.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Adapters
{
    public class OutputQuestionStructAdapter
    {
        private readonly QuestionStruct questionStruct;

        public OutputQuestionStructAdapter(QuestionStruct questionStruct)
        {
            this.questionStruct = questionStruct ?? throw new ArgumentNullException(nameof(questionStruct));
        }

        public OutputQuestionStruct Get()
        {
            return new OutputQuestionStruct()
            {
                Question = questionStruct.Question,
                Id = Guid.NewGuid().ToString(),
                Answers = questionStruct.Answers.Select(GetAnswer).ToList()
            };
        }

        private OutputAnswer GetAnswer(Answer answer)
        {
            var adapter = new OutputAnswerAdapter(answer);

            return adapter.Get();
        }
    }
}
