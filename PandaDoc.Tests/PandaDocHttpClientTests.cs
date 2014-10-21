using System;
using System.Net.Http;
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
            var client = new PandaDocHttpClient(settings);
            Assert.NotNull(client.Settings);
            client.Settings = new PandaDocHttpClientSettings();
            Assert.NotNull(client.Settings);
        }

        [Test]
        public void SetHttpClient()
        {
            var settings = new PandaDocHttpClientSettings();
            var client = new PandaDocHttpClient(settings);
            Assert.NotNull(client.HttpClient);
            client.HttpClient = new HttpClient();
            Assert.NotNull(client.HttpClient);
        }
    }
}