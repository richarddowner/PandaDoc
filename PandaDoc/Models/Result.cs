using Newtonsoft.Json;

namespace PandaDoc.Models
{
    public class Result
    {
        [JsonProperty("owner")]
        public Owner Owner { get; set; }

        [JsonProperty("modified_by")]
        public ModifiedBy ModifiedBy { get; set; }

        [JsonProperty("revision_number")]
        public int RevisionNumber { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("revision")]
        public object Revision { get; set; }

        [JsonProperty("folder")]
        public string Folder { get; set; }

        [JsonProperty("recipients")]
        public Recipient[] Recipients { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("date_created")]
        public string DateCreated { get; set; }

        [JsonProperty("date_modified")]
        public string DateModified { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("removed")]
        public bool Removed { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("parent")]
        public int Parent { get; set; }

        [JsonProperty("uuid")]
        public string Uuid { get; set; }
    }
}