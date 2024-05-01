using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestionnaireLibrary;

namespace QuestionnaireTheGame
{
    public interface IQuestionPageHandler
    {
        public Question CurrentQuestion { get; }
        public List<Question> Questions { get; }
        public List<Answer> Guesses { get; }

        public void ProcessQuestion(Question question);

        public void QuestionAnswered(Answer answer);
    }
}
