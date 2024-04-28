using ScoreboardLibrary;

namespace TestScoreboardLibrary
{
    public class UnitTestScoreboard
    {
        private readonly PlayerScore playerScore;
        private readonly Scoreboard scoreboard;

        public UnitTestScoreboard()
        {
            playerScore = new("John", 10);
            scoreboard = new Scoreboard();
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

        [Fact]
        public void TestAddPlayerScore()
        {
            Scoreboard scoreboard = new();
            scoreboard.AddPlayer(playerScore);
            Assert.Contains(playerScore, scoreboard.PlayerScores);
        }

        [Fact]
        public void TestAddPlayerWithNameAndScore()
        {
            scoreboard.AddPlayer("Jane", 8);
            Assert.Contains(scoreboard.PlayerScores, player => player.Name == "Jane" && player.Score == 8);
        }

        [Fact]
        public void TestSortScoreboard()
        {
            scoreboard.AddPlayer("John", 10);
            scoreboard.AddPlayer("Jane", 8);
            scoreboard.SortScoreBoard();
            Assert.Equal(10, scoreboard.PlayerScores[0].Score);
            Assert.Equal(8, scoreboard.PlayerScores[1].Score);
        }

        [Fact]
        public void TestClear()
        {
            scoreboard.AddPlayer("John", 10);
            scoreboard.Clear(false);
            Assert.Empty(scoreboard.PlayerScores);
        }

        [Fact]
        public void TestSave()
        {
            string filePath = "test.json";
            Scoreboard scoreboard = new(filePath);

            // Ensure the file does not exist before the test
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            scoreboard.AddPlayer(new PlayerScore("John", 10));
            scoreboard.AddPlayer(new PlayerScore("Jane", 8));
            scoreboard.Save();

            // Check that the file now exists
            Assert.True(File.Exists(filePath));

            // Clean up after the test
            File.Delete(filePath);
        }
    }
}