using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireLibrary
{
	public class Question
	{
		private readonly List<Answer> possibleAnser = new();

		public string Text { get; set; }
		public string? ImageUrl { get; set; }

		public Question(string text)
		{
			Text = text;
		}

		public void Add(Answer answer)
		{
			possibleAnser.Add(answer);
		}

		public Answer GetAnswer(int index)
		{
			return possibleAnser[index];
		}

		public override string ToString()
		{
			return Text;
		}
	}
}
