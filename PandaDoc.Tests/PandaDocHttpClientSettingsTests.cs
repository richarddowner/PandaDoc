using System;
using NUnit.Framework;

namespace PandaDoc.Tests
{
    [TestFixture]
    public class PandaDocHttpClientSettingsTests : BaseTest
    {
        [Test]
        public void UseAppSettings()
        {
            var settings = new PandaDocHttpClientSettings();

            Assert.NotNull(settings.ClientId);
            Assert.NotNull(settings.ClientSecret);
            Assert.NotNull(settings.ApiUri);
            Assert.NotNull(settings.AuthUri);
        }

        [Test]
        public void UseUserSettings()
        {
            var settings = new PandaDocHttpClientSettings("clientid", "clientsecret", new Uri("https://api.test.com"), new Uri("https://auth.test.com"));

            Assert.AreEqual(settings.ClientId, "clientid");
            Assert.AreEqual(settings.ClientSecret, "clientsecret");
            
            Assert.AreEqual(settings.ApiUri, new Uri("https://api.test.com"));
            Assert.AreEqual(settings.AuthUri, new Uri("https://auth.test.com"));
        }
    }
}