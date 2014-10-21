using System;
using System.Configuration;
using NUnit.Framework;

namespace PandaDoc.Tests
{
    [TestFixture]
    public abstract class BaseTest
    {
        protected const string TestClient = "TestClient";
        protected const string TestSecret = "TestSecret";
        
        protected static readonly Uri TestBaseUri = new Uri("https://app.pandadoc.com");

        [SetUp]
        public void SetUp()
        {
            Environment.SetEnvironmentVariable(PandaDocHttpClientSettings.EnvironmentClientIdKey, null);
            Environment.SetEnvironmentVariable(PandaDocHttpClientSettings.EnvironmentClientSecretKey, null);

            ConfigurationManager.AppSettings[PandaDocHttpClientSettings.ClientIdKey] = TestClient;
            ConfigurationManager.AppSettings[PandaDocHttpClientSettings.ClientSecretKey] = TestSecret;
        } 
    }
}