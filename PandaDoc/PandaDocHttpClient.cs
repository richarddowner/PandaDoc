using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using PandaDoc.Models.CreateDocument;
using PandaDoc.Models.GetDocuments;

namespace PandaDoc
{
    public class PandaDocHttpClient : IDisposable
    {
        private PandaDocHttpClientSettings settings;
        private HttpClient httpClient;
        private JsonMediaTypeFormatter jsonFormatter;
        private PandaDocBearerToken bearerToken;

        public PandaDocHttpClient()
            : this(new PandaDocHttpClientSettings())
        {
        }

        public PandaDocHttpClient(PandaDocHttpClientSettings settings)
        {
            Settings = settings;
            HttpClient = new HttpClient();
            JsonFormatter = new JsonMediaTypeFormatter();
        }

        public void Dispose()
        {
            httpClient.Dispose();
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

        public PandaDocBearerToken BearerToken
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

        public void SetBearerToken(PandaDocBearerToken value)
        {
            BearerToken = value;
        }

        public async Task<PandaDocHttpResponse<PandaDocBearerToken>> Login(string username, string password)
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

            HttpResponseMessage httpResponse = await httpClient.PostAsync(settings.AuthUri + "oauth2/access_token", content);

            PandaDocHttpResponse<PandaDocBearerToken> response = await httpResponse.ToPandaDocResponseAsync<PandaDocBearerToken>();

            return response;
        }

        public async Task<PandaDocHttpResponse<GetDocumentsResponse>> Documents()
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync(settings.ApiUri + "public/v1/documents");

            PandaDocHttpResponse<GetDocumentsResponse> response = await httpResponse.ToPandaDocResponseAsync<GetDocumentsResponse>();

            return response;
        }

        public async Task<PandaDocHttpResponse<CreateDocumentResponse>> CreateDocument(CreateDocumentRequest request)
        {
            HttpContent httpContent = new ObjectContent<CreateDocumentRequest>(request, JsonFormatter);
            
            HttpResponseMessage httpResponse = await httpClient.PostAsync(settings.ApiUri + "public/v1/documents", httpContent);

            PandaDocHttpResponse<CreateDocumentResponse> response = await httpResponse.ToPandaDocResponseAsync<CreateDocumentResponse>();

            return response;
        }
    }
}