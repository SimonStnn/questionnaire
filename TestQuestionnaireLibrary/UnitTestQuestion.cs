using QuestionnaireLibrary;

namespace TestQuestionnaireLibrary
{
	public class UnitTestQuestion
	{
		[Fact]
		public void TestConstructor()
		{
			Question question = new("Are you reading this?");
			Assert.NotNull(question);
			Assert.Equal("Are you reading this?", question.Text);
		}
	}
}