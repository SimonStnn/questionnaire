using ScoreboardLibrary;

namespace TestScoreboardLibrary
{
    public class UnitTestScoreboard
    {
        private readonly PlayerScore playerScore;

        UnitTestScoreboard()
        {
            playerScore = new("John", 10);
        }

        [Fact]
        public void TestAddPlayerScore()
        {
            Scoreboard scoreboard = new();
            scoreboard.AddPlayer(playerScore);
            Assert.Contains(playerScore, scoreboard.PlayerScores);
        }

        [Fact]
        public void TestPlayerScoreConstructor()
        {
            PlayerScore playerScore = new("John", 10);
            Assert.Equal("John", playerScore.Name);
            Assert.Equal(10, playerScore.Score);
            PlayerScore playerScore2 = new("Jane", 8);
            Assert.Equal("Jane", playerScore2.Name);
            Assert.Equal(8, playerScore2.Score);
        }

        [Fact]
        public void TestPlayerScoreToString()
        {
            Assert.Equal($"John (10)", playerScore.ToString());
            Assert.Equal($"John (10)", $"{playerScore}");
        }
    }
}