using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Newtonsoft.Json;

namespace PandaDoc.WebhookTests
{
    public static class Extensions
    {
        public static async Task<T> ReadBodyAsJsonAsync<T>(this IOwinContext context)
        {
            var body = await context.ReadBodyAsStringAsync();
            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(body));
        }

        public static async Task<string> ReadBodyAsStringAsync(this IOwinContext context)
        {
            var sb = new StringBuilder();
            var buffer = new byte[8000];
            var read = 0;

            read = await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
            while (read > 0)
            {
                sb.Append(Encoding.UTF8.GetString(buffer));
                buffer = new byte[8000];
                read = await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
            }

            return sb.ToString();
        }

        public static async Task ReturnJsonAsync(this IOwinContext context, object model)
        {
            context.Response.Headers.Set("Content-Type", "application/json");
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject((model)));
        }
    }
}