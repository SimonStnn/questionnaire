using System.Text.Json;

namespace ScoreboardLibrary
{
    public class Scoreboard
    {
        private readonly List<PlayerScore> scoreList = new();
        private string saveFile = "scoreboard.json";

        public List<PlayerScore> PlayerScores { get => scoreList; }

        public Scoreboard()
        {
        }

        public Scoreboard(string saveFile) : this()
        {
            this.saveFile = saveFile;
        }

        public void AddPlayer(PlayerScore player)
        {
            scoreList.Add(player);
        }

        public void AddPlayer(string name, int score)
        {
            scoreList.Add(new PlayerScore(name, score));
        }

        public void SortScoreBoard()
        {
            PlayerScores.Sort((player1, player2) => player2.Score.CompareTo(player1.Score));
        }

        public void Clear(bool clearSaveFile = true)
        {
            scoreList.Clear();
            if (clearSaveFile)
                DeleteSaveFile();
        }

        private bool SaveFileExists()
        {
            return File.Exists(saveFile);
        }

        public void DeleteSaveFile()
        {
            if (SaveFileExists())
                File.Delete(saveFile);
        }

        public List<PlayerScore> Load()
        {
            scoreList.Clear();
            if (SaveFileExists())
            {
                using StreamReader reader = new(saveFile);
                string json = reader.ReadToEnd();
                scoreList.AddRange(JsonSerializer.Deserialize<List<PlayerScore>>(json));
            }
            return scoreList;
        }

        public void Save()
        {
            DeleteSaveFile();
            using StreamWriter writer = new(saveFile);
            writer.Write(JsonSerializer.Serialize(scoreList));
        }

        public override string ToString()
        {
            return $"Scoreboard: {string.Join(", ", scoreList)}";
        }
    }
}
