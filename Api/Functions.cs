using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Newtonsoft.Json;
using ServerlessStreamR.Shared;

namespace ServerlessStreamR.Api
{
    public static class Functions
    {
        [FunctionName("negotiate")]
        public static SignalRConnectionInfo Negotiate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            [SignalRConnectionInfo(HubName = "streamrblazor")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }

       [FunctionName("sendframe")]
        public static async Task SendFrameAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            [SignalR(HubName = "streamrblazor")] IAsyncCollector<SignalRMessage> signalRMessages)
        {
            string username = "test-user";
            if (req.Headers.ContainsKey("x-ms-client-principal"))
            {
                var claimsPrincipal = StaticWebAppsAuth.Parse(req);
                username = claimsPrincipal.Identity.Name;
            }

            var frameJson = await new StreamReader(req.Body).ReadToEndAsync();
            var frame = JsonConvert.DeserializeObject<Frame>(frameJson);
            frame.Username = username;

            await signalRMessages.AddAsync(new SignalRMessage
            {
                Target = "newFrame",
                Arguments = new object[] { frame }
            });
        }
    }
}