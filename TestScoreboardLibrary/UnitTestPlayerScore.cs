using ScoreboardLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestScoreboardLibrary
{
    public class UnitTestPlayerScore
    {
        private readonly PlayerScore playerscore;

        public UnitTestPlayerScore()
        {
            playerscore = new("John", 10);
        }

        [Fact]
        public void TestPlayerScoreConstructor()
        {
            Assert.Equal("John", playerscore.Name);
            Assert.Equal(10, playerscore.Score);
        }

        [Fact]
        public void TestPlayerScoreToString()
        {
            Assert.Equal("John (10)", playerscore.ToString());
            Assert.Equal("John (10)", $"{playerscore}");
        }

        [Fact]
        public void TestPlayerScoreNameSetter()
        {
            playerscore.Name = "Jane";
            Assert.Equal("Jane", playerscore.Name);
        }

        [Fact]
        public void TestPlayerScoreScoreSetter()
        {
            playerscore.Score = 15;
            Assert.Equal(15, playerscore.Score);
        }
    }
}
