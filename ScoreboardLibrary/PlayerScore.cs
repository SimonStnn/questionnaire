using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreboardLibrary
{
    public class PlayerScore
    {
        private string name;
        private int score;

        public string Name
        {
            get => name;
            set => name = value;
        }
        public int Score
        {
            get => score;
            set => score = value;
        }

        public PlayerScore(string name, int score)
        {
            Name = name;
            Score = score;
        }

        public override string ToString()
        {
            return $"{Name} ({Score})";
        }
    }
}
