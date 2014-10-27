using Owin;
using System;
using System.Collections.Generic;
using PandaDoc.Models.Webhooks;

namespace PandaDoc.WebhookTests
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseErrorPage();

            app.Run(async context =>
            {
                var notifications = await context.ReadBodyAsJsonAsync<List<Notification>>() ?? new List<Notification>();

                foreach (var notification in notifications)
                {
                    Console.WriteLine("Notification: {0}", notification.Data.Id);
                }

                await context.ReturnJsonAsync(notifications);
            });
        }
    }
}