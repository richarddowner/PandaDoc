using System;
using Owin;
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
                var notification = await context.ReadBodyAsJsonAsync<Notification>() ?? new Notification();

                Console.WriteLine(notification.Document.Id);

                await context.ReturnJsonAsync(notification);
            });
        }
    }
}