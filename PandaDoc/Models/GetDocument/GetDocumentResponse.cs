using System;
using Newtonsoft.Json;

namespace PandaDoc.Models.GetDocument
{
    public enum DocumentStatus
    {
        Uploaded,
        Draft,
        Sent,
        Completed,
    }

    public class GetDocumentResponse
    {
        public GetDocumentResponse()
        {
            Recipients = new Recipient[] { };
        }

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

        public DocumentStatus DocumentStatus
        {
            get
            {
                switch (Status)
                {
                    case "document.uploaded":
                        return DocumentStatus.Uploaded;

                    case "document.draft":
                        return DocumentStatus.Draft;

                    case "document.sent":
                        return DocumentStatus.Sent;

                    case "document.completed":
                        return DocumentStatus.Completed;

                    default:
                        throw new ArgumentOutOfRangeException(string.Format("Status was '{0}'. This is a bug and a new DocumentStatus should be added", Status));
                }
            }
        }
    }

    public class Recipient
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("recipient_type")]
        public string RecipientType { get; set; }

        [JsonProperty("has_completed")]
        public bool HasCompleted { get; set; }
    }
}