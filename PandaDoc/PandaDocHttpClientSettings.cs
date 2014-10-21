using System;
using System.Configuration;

namespace PandaDoc
{
    public class PandaDocHttpClientSettings
    {
        public const string ClientIdKey = "pandadoc:clientid";
        public const string ClientSecretKey = "pandadoc:clientsecret";

        public const string EnvironmentClientIdKey = "PANDADOC_CLIENTID";
        public const string EnvironmentClientSecretKey = "PANDADOC_CLIENTSECRET";

        private string clientId;
        private string clientSecret;
        private Uri baseUri;

        public PandaDocHttpClientSettings(string clientId = null, string clientSecret = null, Uri baseUri = null)
        {
            BaseUri = baseUri ?? new Uri("https://app.pandadoc.com");

            var environmentClientId = Environment.GetEnvironmentVariable(EnvironmentClientIdKey);
            var environmentClientSecret = Environment.GetEnvironmentVariable(EnvironmentClientSecretKey);

            if (!string.IsNullOrEmpty(environmentClientId) && !string.IsNullOrEmpty(environmentClientSecret))
            {
                ClientId = environmentClientId;
                ClientSecret = environmentClientSecret;
            }
            else if (!string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(clientSecret))
            {
                ClientId = clientId;
                ClientSecret = clientSecret;
            }
            else
            {
                ClientId = ConfigurationManager.AppSettings[ClientIdKey];
                ClientSecret = ConfigurationManager.AppSettings[ClientSecretKey];
            }
        }

        public string ClientId
        {
            get { return clientId; }
            private set
            {
                if (value == null) throw new ArgumentNullException("value");
                clientId = value;
            }
        }

        public string ClientSecret
        {
            get { return clientSecret; }
            private set
            {
                if (value == null) throw new ArgumentNullException("value");
                clientSecret = value;
            }
        }

        public Uri BaseUri
        {
            get { return baseUri; }
            private set
            {
                if (value == null) throw new ArgumentNullException("value");
                baseUri = value;
            }
        }
    }
}