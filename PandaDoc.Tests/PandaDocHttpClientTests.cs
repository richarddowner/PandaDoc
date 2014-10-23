using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using NUnit.Framework;
using PandaDoc.Models.CreateDocument;
using PandaDoc.Models.GetDocument;
using PandaDoc.Models.GetDocuments;
using PandaDoc.Models.SendDocument;

namespace PandaDoc.Tests
{
    [TestFixture]
    public class PandaDocHttpClientTests : BaseTest
    {
        [Test]
        public void ConstructorWithoutSettings()
        {
            Assert.Throws<ArgumentNullException>(() => new PandaDocHttpClient(null));
        }

        [Test]
        public void ConstructWithSettings()
        {
            var settings = new PandaDocHttpClientSettings();
            var client = new PandaDocHttpClient(settings);
            Assert.NotNull(client.Settings);
        }

        [Test]
        public void SetSettings()
        {
            var settings = new PandaDocHttpClientSettings();

            var client = new PandaDocHttpClient(settings)
            {
                Settings = new PandaDocHttpClientSettings()
            };

            Assert.NotNull(client.Settings);
        }

        [Test]
        public void SetHttpClient()
        {
            var settings = new PandaDocHttpClientSettings();

            var client = new PandaDocHttpClient(settings)
            {
                HttpClient = new HttpClient()
            };

            Assert.NotNull(client.HttpClient);
        }

        [Test]
        public void SetJsonFormatter()
        {
            var settings = new PandaDocHttpClientSettings();

            var client = new PandaDocHttpClient(settings)
            {
                JsonFormatter = new JsonMediaTypeFormatter()
            };

            Assert.NotNull(client.JsonFormatter);
        }

        [Test]
        public void SetBearerToken()
        {
            var settings = new PandaDocHttpClientSettings();

            var client = new PandaDocHttpClient(settings)
            {
                BearerToken = new PandaDocBearerToken
                {
                    AccessToken = "TestAccessToken",
                    RefreshToken = "TestRefreshToken",
                }
            };

            Assert.NotNull(client.BearerToken);
        }

        [Test]
        public void SetBearerTokenWithNull()
        {
            var settings = new PandaDocHttpClientSettings();
            var client = new PandaDocHttpClient(settings);
            Assert.Throws<ArgumentNullException>(() => client.BearerToken = null);
            Assert.Throws<ArgumentNullException>(() => client.SetBearerToken(null));
        }

        [Test]
        public async void Login()
        {
            using (var client = new PandaDocHttpClient())
            {
                PandaDocHttpResponse<PandaDocBearerToken> response = await client.Login(username: Username, password: Password);

                Assert.NotNull(response);
                Assert.NotNull(response.Value);
                Assert.NotNull(response.Value.AccessToken);
                Assert.NotNull(response.Value.RefreshToken);
                Assert.IsTrue(response.IsSuccessStatusCode);
            }
        }

        [Test]
        public async void GetDocuments()
        {
            using (PandaDocHttpClient client = await EnsureLoggedIn())
            {
                PandaDocHttpResponse<GetDocumentsResponse> response = await client.GetDocuments();

                Assert.NotNull(response);
                Assert.NotNull(response.Value);
                Assert.IsTrue(response.IsSuccessStatusCode);
            }
        }

        [Test]
        public async void CreateDocument()
        {
            using (PandaDocHttpClient client = await EnsureLoggedIn())
            {
                CreateDocumentRequest request = CreateDocumentRequest();

                PandaDocHttpResponse<CreateDocumentResponse> response = await client.CreateDocument(request);

                Assert.NotNull(response);
                Assert.NotNull(response.Value);
                Assert.IsTrue(response.IsSuccessStatusCode);
            }
        }

        [Test]
        public async void GetDocument()
        {
            using (PandaDocHttpClient client = await EnsureLoggedIn())
            {
                var createRequest = CreateDocumentRequest();
                var createResponse = await client.CreateDocument(createRequest);

                PandaDocHttpResponse<GetDocumentResponse> response = await client.GetDocument(createResponse.Value.Uuid);
                
                Assert.NotNull(response);
                Assert.NotNull(response.Value);
                Assert.IsTrue(response.IsSuccessStatusCode);
            }
        }

        [Test]
        public async void SendDocument()
        {
            using (PandaDocHttpClient client = await EnsureLoggedIn())
            {
                var createRequest = CreateDocumentRequest();
                var createResponse = await client.CreateDocument(createRequest);

                var sendRequest = new SendDocumentRequest
                {
                    Message = "plz sign doge."
                };

                PandaDocHttpResponse<SendDocumentResponse> response = await client.SendDocument(createResponse.Value.Uuid, sendRequest);
                
                Assert.NotNull(response);
                Assert.NotNull(response.Value);
                Assert.IsTrue(response.IsSuccessStatusCode);
            }
        }

        private CreateDocumentRequest CreateDocumentRequest()
        {
            var request = new CreateDocumentRequest
            {
                Name = "Sample Document",
                Url = SampleDocUrl,
                Recipients = new[]
                {
                    new Models.CreateDocument.Recipient
                    {
                        Email = "jake.net@gmail.com",
                        FirstName = "Jake",
                        LastName = "Scott",
                        Role = "u1",
                    }
                },
                Fields = new Dictionary<string, Field>
                {
                    {"optId", new Field {Title = "Field 1"}}
                }
            };
            return request;
        }
    }
}