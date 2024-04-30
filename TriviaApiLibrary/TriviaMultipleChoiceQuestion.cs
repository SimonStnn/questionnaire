using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaApiLibrary
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TriviaMultipleChoiceQuestion
    {
        [JsonProperty("question")]
        public TriviaQuestion Question { get; set; }

        [JsonProperty("correctAnswer")]
        public string CorrectAnswer { get; set; }

        [JsonProperty("incorrectAnswers")]
        public List<string> IncorrectAnswers { get;set; }

        [JsonProperty("category")]
        public string Category { get; set; }


        [JsonProperty("difficulty")]
        public string Difficulty { get; set; }
    }
}
