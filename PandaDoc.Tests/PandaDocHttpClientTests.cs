using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using NUnit.Framework;

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
                BearerToken = new BearerToken
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
        }

        [Test, Ignore("Test hits the PandaDoc API")]
        public async void Login()
        {
            var settings = new PandaDocHttpClientSettings
            (
                clientId: "YOUR_CLIENT_ID_HERE",
                clientSecret: "YOUR_CLIENT_SECRET_HERE"
            );

            var client = new PandaDocHttpClient(settings);

            PandaDocHttpResponse<BearerToken> login = await client.Login
            (
                username: "YOUR_EMAIL_HERE@YOU.COM", 
                password: "YOUR_PASSWORD"
            );

            Assert.NotNull(login);
            Assert.NotNull(login.Value);
            Assert.NotNull(login.Value.AccessToken);
            Assert.NotNull(login.Value.RefreshToken);
        }
    }
}