using NUnit.Framework;

namespace PandaDoc.Tests
{
    [TestFixture]
    public class BearerTokenTests
    {
        [Test]
        public void AccessToken()
        {
            var token = new BearerToken { AccessToken = "test" };
            
            Assert.AreEqual("test", token.AccessToken);
        }

        [Test]
        public void RefreshToken()
        {
            var token = new BearerToken { RefreshToken = "test" };

            Assert.AreEqual("test", token.RefreshToken);
        }
    }
}
