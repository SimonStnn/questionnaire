using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireLibrary
{
	public class Question
	{
		private List<Answer> possibleAnser = new();

		public string Text { get; set; }
		public string? ImageUrl { get; set; }

		public Question(string text)
		{
			Text = text;
		}

		public void Add(Answer answer)
		{
			throw new NotImplementedException();
		}

		public void GetAnswer(int index)
		{
			throw new NotImplementedException();
		}

		public override string ToString()
		{
			throw new NotImplementedException();
		}
	}
}
