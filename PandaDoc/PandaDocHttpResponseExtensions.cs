using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PandaDoc
{
    public static class PandaDocHttpResponseExtensions
    {
        public static async Task<PandaDocHttpResponse> ToPandaDocResponseAsync(this HttpResponseMessage httpResponse)
        {
            if (httpResponse == null) throw new ArgumentNullException("httpResponse");

            var responseContent = await httpResponse.Content.ReadAsStringAsync();

            var response = new PandaDocHttpResponse
            {
                Content = responseContent,
                IsSuccessStatusCode = httpResponse.IsSuccessStatusCode,
                StatusCode = httpResponse.StatusCode,
                Headers = httpResponse.Headers,
                HttpResponse = httpResponse,
                Errors = new Dictionary<string, string>()
            };

            if (!httpResponse.IsSuccessStatusCode)
            {
                ExtractErrors(responseContent, response);
            }

            return response;
        }

        public static async Task<PandaDocHttpResponse<T>> ToPandaDocResponseAsync<T>(this HttpResponseMessage httpResponse)
        {
            if (httpResponse == null) throw new ArgumentNullException("httpResponse");

            var responseContent = await httpResponse.Content.ReadAsStringAsync();

            var response = new PandaDocHttpResponse<T>
            {
                Content = responseContent,
                IsSuccessStatusCode = httpResponse.IsSuccessStatusCode,
                StatusCode = httpResponse.StatusCode,
                Headers = httpResponse.Headers,
                HttpResponse = httpResponse,
                Errors = new Dictionary<string, string>()
            };

            if (httpResponse.IsSuccessStatusCode)
            {
                response.Value = await httpResponse.Content.ReadAsAsync<T>();
            }
            else
            {
                ExtractErrors(responseContent, response);
            }

            return response;
        }

        private static void ExtractErrors(string responseContent, PandaDocHttpResponse response)
        {
            var data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(responseContent);

            if (data.ContainsKey("type"))
            {
                var errorType = data["type"].ToString();

                if (data.ContainsKey("details"))
                {
                    var errorDetails = data["details"].ToString();
                    response.Errors.Add(errorType, errorDetails);    
                }

                if (data.ContainsKey("detail"))
                {
                    var detail = data["detail"].ToString();
                    response.Errors.Add(errorType, detail);
                }
            }
        }
    }
}