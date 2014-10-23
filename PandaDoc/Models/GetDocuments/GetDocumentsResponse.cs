using System;
using Newtonsoft.Json;

namespace PandaDoc.Models.GetDocuments
{
    public class GetDocumentsResponse
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

    public class ModifiedBy
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

    public class Recipient
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("document")]
        public string Document { get; set; }

        [JsonProperty("contact")]
        public Contact Contact { get; set; }

        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("is_done")]
        public bool IsDone { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }
    }

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
        public DateTime DateCreated { get; set; }

        [JsonProperty("date_modified")]
        public DateTime DateModified { get; set; }

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