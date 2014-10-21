using System;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace PandaDoc
{
    public class PandaDocHttpClient
    {
        private PandaDocHttpClientSettings settings;
        private HttpClient httpClient;
        private JsonMediaTypeFormatter jsonFormatter;

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
    }
}