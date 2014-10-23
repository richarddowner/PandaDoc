using Newtonsoft.Json;

namespace PandaDoc.Models
{
    public class Documents
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("next")]
        public object Next { get; set; }

        [JsonProperty("previous")]
        public object Previous { get; set; }

        [JsonProperty("results")]
        public Result[] Results { get; set; }
    }
}
