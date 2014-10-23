using Newtonsoft.Json;

namespace PandaDoc.Models
{
    public class Contact
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("company")]
        public object Company { get; set; }

        [JsonProperty("is_internal")]
        public bool IsInternal { get; set; }

        [JsonProperty("integrations")]
        public object[] Integrations { get; set; }

        [JsonProperty("removed")]
        public bool Removed { get; set; }
    }
}