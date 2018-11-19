using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace LoanApplications
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
            //[Queue("brokerevent-status")] IAsyncCollector<LoanApplication> messageQueue,
            TraceWriter log)
        {
            log.Info("HTTP trigger function MakeApplication processed a request.");

            var application = await req.Content.ReadAsAsync<BrokerEvent>();
            log.Info($"MakeApplication send status to BrokerId: {application.BrokerId} CorrelationId: {application.CorrelationId}");

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
