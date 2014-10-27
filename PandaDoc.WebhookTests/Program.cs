using System;
using Microsoft.Owin.Hosting;

namespace PandaDoc.WebhookTests
{
    public class Program
    {
        private const string url = "http://localhost:9000";

        public static void Main()
        {
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Listening on {0}", url);
                Console.ReadLine();
            }
        }
    }
}