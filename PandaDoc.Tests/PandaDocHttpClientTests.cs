using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using NUnit.Framework;
using PandaDoc.Models.CreateDocument;
using PandaDoc.Models.GetDocument;
using PandaDoc.Models.GetDocuments;
using PandaDoc.Models.SendDocument;
using Recipient = PandaDoc.Models.GetDocument.Recipient;

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
        public async void LoginAsync()
        {
            using (var client = new PandaDocHttpClient())
            {
                PandaDocHttpResponse<PandaDocBearerToken> response = await client.Login(username: Username, password: Password);

                response.AssertOk();

                Assert.NotNull(response.Value.AccessToken);
                Assert.NotNull(response.Value.RefreshToken);
            }
        }

        [Test]
        public void Login()
        {
            using (var client = new PandaDocHttpClient())
            {
                var response = client.Login(username: Username, password: Password).Result;

                response.AssertOk();

                Assert.NotNull(response.Value.AccessToken);
                Assert.NotNull(response.Value.RefreshToken);
            }
        }

        [Test]
        public async void GetDocumentsAsync()
        {
            using (PandaDocHttpClient client = await EnsureLoggedIn())
            {
                PandaDocHttpResponse<GetDocumentsResponse> response = await client.GetDocuments();
                response.AssertOk();
            }
        }

        [Test]
        public void GetDocuments()
        {
            using (PandaDocHttpClient client = EnsureLoggedIn().Result)
            {
                PandaDocHttpResponse<GetDocumentsResponse> response = client.GetDocuments().Result;
                response.AssertOk();
            }
        }

        [Test]
        public async void CreateDocumentAsync()
        {
            using (PandaDocHttpClient client = await EnsureLoggedIn())
            {
                CreateDocumentRequest request = CreateDocumentRequest();

                PandaDocHttpResponse<CreateDocumentResponse> response = await client.CreateDocument(request);

                response.AssertOk();
            }
        }

        [Test]
        public void CreateDocument()
        {
            using (PandaDocHttpClient client = EnsureLoggedIn().Result)
            {
                CreateDocumentRequest request = CreateDocumentRequest();

                PandaDocHttpResponse<CreateDocumentResponse> response = client.CreateDocument(request).Result;

                response.AssertOk();
            }
        }

        [Test, Ignore("DELETE not supported yet")]
        public async void DeleteDocument()
        {
            using (PandaDocHttpClient client = await EnsureLoggedIn())
            {
                string uuid = "FRkUcECJzFtCBTSFugsaF5";

                PandaDocHttpResponse response = await client.DeleteDocument(uuid);

                response.AssertOk();
            }
        }

        [Test]
        public async void GetDocument()
        {
            using (PandaDocHttpClient client = await EnsureLoggedIn())
            {
                //var createRequest = CreateDocumentRequest();
                //var createResponse = await client.CreateDocument(createRequest);
                //var uuid = createResponse.Value.Uuid;
                var uuid = "9x5fr622ME6rpcG277Nx8C";

                PandaDocHttpResponse<GetDocumentResponse> response = await client.GetDocument(uuid);

                response.AssertOk();

                Console.WriteLine("Document '{0}' has status '{1}'", response.Value.Uuid, response.Value.Status);

                foreach (Recipient recipient in response.Value.Recipients)
                {
                    Console.WriteLine("Recipient '{0} {1}' completed: {2}", recipient.FirstName, recipient.LastName, recipient.HasCompleted);
                }
            }
        }

        [Test]
        public async void SendDocument()
        {
            using (PandaDocHttpClient client = await EnsureLoggedIn())
            {
                var createRequest = CreateDocumentRequest();
                var createResponse = await client.CreateDocument(createRequest);

                Console.WriteLine("Document '{0}' was uploaded", createResponse.Value.Uuid);

                // we have to wait for the document to move from document.uploaded to document.draft before you can send it.
                var attempts = 0;
                while (true)
                {
                    var getResponse = await client.GetDocument(createResponse.Value.Uuid);

                    if (getResponse.Value.DocumentStatus == DocumentStatus.Draft)
                    {
                        Console.WriteLine("Document '{0}' has moved to draft", createResponse.Value.Uuid);
                        break;
                    }

                    await Task.Delay(1000);
                    attempts++;

                    if (attempts == 5)
                    {
                        Assert.Fail();
                    }
                }

                var sendRequest = new SendDocumentRequest
                {
                    Message = "Please sign this document"
                };

                PandaDocHttpResponse<SendDocumentResponse> response = await client.SendDocument(createResponse.Value.Uuid, sendRequest);

                response.AssertOk();

                Console.WriteLine("Document '{0}' was sent", response.Value.Uuid);
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