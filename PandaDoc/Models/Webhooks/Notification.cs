using Newtonsoft.Json;

namespace PandaDoc.Models.Webhooks
{
    public class Notification
    {
        [JsonProperty("document")]
        public Document Document { get; set; }

        [JsonProperty("event")]
        public string Event { get; set; }
    }
}