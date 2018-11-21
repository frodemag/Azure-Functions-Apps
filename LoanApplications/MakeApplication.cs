using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace BrokerApplication
{
    public static class MakeApplication
    {
        [FunctionName("MakeApplication")]
        [return: ServiceBus("broker-status")]
        public static async Task<BrokerEvent> Run(
            [HttpTrigger(AuthorizationLevel.Function, 
            "post", 
            Route = null)]
            HttpRequestMessage req, 
            [Queue("brokerevent-status")] IAsyncCollector<BrokerEvent> messageQueue,
            TraceWriter log)
        {
            var application = await req.Content.ReadAsAsync<BrokerEvent>();
            log.Info($"MakeApplication sent status to ServiceBusBrokerId: {application.BrokerId} CorrelationId: {application.CorrelationId}");
            await messageQueue.AddAsync(application);
            //Returnerer BrokerEvent 
            return application;
            //try
            //{
            //    await messageQueue.AddAsync(application);
            //}
            //catch (System.Exception e)
            //{
            //    return req.CreateResponse(HttpStatusCode.BadRequest, $"Cant send application to status queue. Error: {e.Message}");
            //}
           
            //return req.CreateResponse(HttpStatusCode.OK, 
            //    $"Status message submitted for BrokerId: {application.BrokerId} CorrelationId: {application.CorrelationId}");
        }
    }
}
