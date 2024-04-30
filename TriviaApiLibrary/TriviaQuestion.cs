using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaApiLibrary
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TriviaQuestion
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
