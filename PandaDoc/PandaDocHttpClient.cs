using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace PandaDoc
{
    public class PandaDocHttpClient
    {
        private PandaDocHttpClientSettings settings;
        private HttpClient httpClient;
        private JsonMediaTypeFormatter jsonFormatter;
        private BearerToken bearerToken;

        public PandaDocHttpClient(PandaDocHttpClientSettings settings)
        {
            Settings = settings;
            HttpClient = new HttpClient();
            JsonFormatter = new JsonMediaTypeFormatter();
        }

        public PandaDocHttpClientSettings Settings
        {
            get { return settings; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                settings = value;
            }
        }

        public HttpClient HttpClient
        {
            get { return httpClient; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                httpClient = value;
            }
        }

        public JsonMediaTypeFormatter JsonFormatter
        {
            get { return jsonFormatter; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                jsonFormatter = value;
            }
        }

        public BearerToken BearerToken
        {
            get { return bearerToken; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                bearerToken = value;

                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearerToken.AccessToken);
            }
        }

        public async Task<PandaDocHttpResponse<BearerToken>> Login(string username, string password)
        {
            if (username == null) throw new ArgumentNullException("username");
            if (password == null) throw new ArgumentNullException("password");

            var values = new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"username", username},
                {"password", password},
                {"client_id", settings.ClientId},
                {"client_secret", settings.ClientSecret}
            };

            var content = new FormUrlEncodedContent(values);

            HttpResponseMessage httpResponse = await httpClient.PostAsync(settings.BaseUri + "/oauth2/access_token", content);

            PandaDocHttpResponse<BearerToken> response = await httpResponse.ToPandaDocResponseAsync<BearerToken>();

            return response;
        }
    }
}