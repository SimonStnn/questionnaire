namespace QuestionnaireLibrary
{
	public class Answer
	{
		public string Text;
		public bool IsCorrect;

		public Answer(string text, bool isCorrect)
		{
			Text = text;
			IsCorrect = isCorrect;
		}
	}
}