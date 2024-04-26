using QuestionnaireLibrary;

namespace TestQuestionnaireLibrary
{
	public class UnitTestAnswer
	{
		[Fact]
		public void TestConstructor()
		{
			Answer ans = new("This is a question", false);
			Assert.Equal("This is a question", ans.Text);
			Assert.False(ans.IsCorrect);

			Answer ans2 = new("this answer is correct", true);
			Assert.Equal("this answer is correct", ans2.Text);
			Assert.True(ans2.IsCorrect);
		}
	}
}