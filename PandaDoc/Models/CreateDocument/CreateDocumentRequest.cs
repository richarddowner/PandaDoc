using System.Collections.Generic;
using Newtonsoft.Json;

namespace PandaDoc.Models.CreateDocument
{
    public class Recipient
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }
    }

    public class Field
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }

    public class CreateDocumentRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("recipients")]
        public Recipient[] Recipients { get; set; }

        [JsonProperty("fields")]
        public Dictionary<string, Field> Fields { get; set; }
    }
}
