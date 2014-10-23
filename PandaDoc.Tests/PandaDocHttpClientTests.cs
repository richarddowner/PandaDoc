using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using NUnit.Framework;
using PandaDoc.Models.CreateDocument;
using PandaDoc.Models.GetDocuments;

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
                PandaDocHttpResponse<PandaDocBearerToken> bearerToken = await client.Login(username: Username, password: Password);

                Assert.NotNull(bearerToken);
                Assert.NotNull(bearerToken.Value);
                Assert.NotNull(bearerToken.Value.AccessToken);
                Assert.NotNull(bearerToken.Value.RefreshToken);
            }
        }

        [Test]
        public async void GetDocuments()
        {
            using (PandaDocHttpClient client = await EnsureLoggedIn())
            {
                PandaDocHttpResponse<GetDocumentsResponse> response = await client.Documents();

                Assert.NotNull(response);
                Assert.NotNull(response.Value);
            }
        }

        [Test]
        public async void CreateDocument()
        {
            using (PandaDocHttpClient client = await EnsureLoggedIn())
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

                PandaDocHttpResponse<CreateDocumentResponse> response = await client.CreateDocument(request);

                Assert.NotNull(response);
                Assert.NotNull(response.Value);
            }
        }
    }
}