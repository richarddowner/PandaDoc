using System;
using NUnit.Framework;

namespace PandaDoc.Tests
{
    public static class PandaDocHttpResponseAssertions
    {
        public static void AssertOk(this PandaDocHttpResponse response)
        {
            Assert.NotNull(response);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content);
            }
            
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        public static void AssertOk<T>(this PandaDocHttpResponse<T> response)
        {
            Assert.NotNull(response);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content);
            }

            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.NotNull(response.Value);
        }
    }
}