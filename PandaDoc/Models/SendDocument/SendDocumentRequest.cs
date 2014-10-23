using System;
using Newtonsoft.Json;
using PandaDoc.Models.GetDocument;

namespace PandaDoc.Models.SendDocument
{
    public class SendDocumentRequest
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class SendDocumentResponse
    {
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("recipients")]
        public Recipient[] Recipients { get; set; }

        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("date_modified")]
        public DateTime DateModified { get; set; }
    }
}