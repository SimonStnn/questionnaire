using QuestionnaireLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuestionnaireTheGame
{
	/// <summary>
	/// Interaction logic for QuestionsPage.xaml
	/// </summary>
	public partial class QuestionsPage : Page
	{
		public readonly IQuestionPageHandler handler;

		public QuestionsPage(IQuestionPageHandler handler)
		{
			this.handler = handler;

			InitializeComponent();
		}
		public void RenderQuestion()
		{
			RenderQuestion(handler.CurrentQuestionIndex);
		}
		public void RenderQuestion(int index)
		{
			RenderQuestion(handler.Questions[index]);
		}
		public void RenderQuestion(Question question)
		{
			tbQuestion.Text = question.Text;
			spAnswers.Children.Clear();
			foreach(Answer answer in question.Answers)
			{
				Button btn = RenderAnswer(answer);
				spAnswers.Children.Add(btn);
			}
		}

		public Button RenderAnswer(Answer answer)
		{
			Button btn = new()
			{
				Content = answer.Text,
			};
			btn.Click += (sender, e) =>
			{
				if(++handler.CurrentQuestionIndex < handler.Questions.Count)
				{
					handler.QuestionAnswered(answer);
					RenderQuestion();
				}
				else
				{
					handler.QuestionAnswered(answer);
					handler.Done();
				}
			};
			return btn;
		}
	}
}
