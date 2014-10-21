using System;
using System.Configuration;
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

            Assert.AreEqual(TestClient, settings.ClientId);
            Assert.AreEqual(TestSecret, settings.ClientSecret);
            Assert.AreEqual(TestBaseUri, settings.BaseUri);
        }

        [Test]
        public void UseAppSettingsWithoutClientId()
        {
            ConfigurationManager.AppSettings[PandaDocHttpClientSettings.ClientIdKey] = null;
            Assert.Throws<ArgumentNullException>(() => new PandaDocHttpClientSettings());
        }

        [Test]
        public void UseAppSettingsWithoutClientSecret()
        {
            ConfigurationManager.AppSettings[PandaDocHttpClientSettings.ClientSecretKey] = null;
            Assert.Throws<ArgumentNullException>(() => new PandaDocHttpClientSettings());
        }

        [Test]
        public void UseExplictSettings()
        {
            var settings = new PandaDocHttpClientSettings(TestClient, TestSecret);

            Assert.AreEqual(TestClient, settings.ClientId);
            Assert.AreEqual(TestSecret, settings.ClientSecret);
            Assert.AreEqual(TestBaseUri, settings.BaseUri);
        }

        [Test]
        public void UseEnvironmentSettings()
        {
            Environment.SetEnvironmentVariable(PandaDocHttpClientSettings.EnvironmentClientIdKey, "Env-ClientId");
            Environment.SetEnvironmentVariable(PandaDocHttpClientSettings.EnvironmentClientSecretKey, "Env-ClientSecret");

            var settings = new PandaDocHttpClientSettings();

            Assert.AreEqual("Env-ClientId", settings.ClientId);
            Assert.AreEqual("Env-ClientSecret", settings.ClientSecret);
        }
    }
}