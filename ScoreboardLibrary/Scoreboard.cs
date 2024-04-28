namespace ScoreboardLibrary
{
    public class Scoreboard
    {
        private readonly List<PlayerScore> scoreList = new();

        public List<PlayerScore> PlayerScores { get => scoreList; }

        public Scoreboard()
        {
        }

        public void AddPlayer(PlayerScore player)
        {
            scoreList.Add(player);
        }

        public void AddPlayer(string name, int score)
        {
            scoreList.Add(new PlayerScore(name, score));
        }

        public override string ToString()
        {
            return $"Scoreboard: {string.Join(", ", scoreList)}";
        }

        public void SortScoreBoard()
        {
            scoreList.Sort();
        }
    }
}
