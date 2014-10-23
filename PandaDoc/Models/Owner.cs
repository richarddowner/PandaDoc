using Newtonsoft.Json;

namespace PandaDoc.Models
{
    public class Owner
    {
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        [JsonProperty("is_suspended")]
        public bool IsSuspended { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("iid")]
        public int Iid { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("signup_source")]
        public string SignupSource { get; set; }

        [JsonProperty("phone_number")]
        public object PhoneNumber { get; set; }
    }
}