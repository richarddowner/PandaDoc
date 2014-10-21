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
    }
}