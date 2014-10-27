using System;
using Newtonsoft.Json;

namespace PandaDoc.Models.Webhooks
{
    public class Data
    {
        /// <summary>
        /// DOCUMENT_UUID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Notification
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("triggered_at")]
        public DateTime TriggeredAt { get; set; }
    }
}