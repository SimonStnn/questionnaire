using QuestionnaireLibrary;

namespace TestQuestionnaireLibrary
{
	public class UnitTestQuestion
	{
		Question question = new("Are you reading this?");

		[Fact]
		public void TestConstructor()
		{
			Question question1 = new("Are you reading this?");
			Assert.NotNull(question1);
			Assert.Equal("Are you reading this?", question1.Text);
			Assert.Throws<ArgumentOutOfRangeException>(() => question1.GetAnswer(0));
		}

		[Fact]
		public void TestAdd()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => question.GetAnswer(0));
			Answer answer = new("Some answer", true);
			question.Add(answer);
			Assert.Equal(answer, question.GetAnswer(0));
			Answer answer2 = new("Some answer", false);
			question.Add(answer2);
			Assert.Equal(answer2, question.GetAnswer(1));
			Assert.Throws<ArgumentOutOfRangeException>(() => question.GetAnswer(2));
		}

        [Fact]
        public void TestGetAnswer()
        {
			Answer answer = new("Some answer", true);
            question.Add(answer);
            Assert.Equal(answer, question.GetAnswer(0));
            Answer answer2 = new("Some answer", false);
            question.Add(answer2);
            Assert.Equal(answer2, question.GetAnswer(1));
            Assert.Throws<ArgumentOutOfRangeException>(() => question.GetAnswer(2));
        }


		[Fact]
		public void TestToString()
		{
			Assert.Equal("Are you reading this?", question.ToString());
			Assert.Equal("Are you reading this?", $"{question}");
        }
    }
}