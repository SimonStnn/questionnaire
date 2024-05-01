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
        public int CurrentQuestionIndex { get; set; }
        public List<Question> Questions { get; }

        public void QuestionAnswered(Answer answer);

        public void Done();
    }
}
