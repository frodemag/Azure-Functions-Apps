using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace LoanApplications
{
    public static class Function1
    {
        [FunctionName("MakeApplication")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, 
            "get", 
            "post", 
            Route = "loanapplications/{id}")]HttpRequestMessage req, 
            string id,
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // parse query parameter
            //string name = req.GetQueryNameValuePairs()
                //.FirstOrDefault(q => string.Compare(q.Key, "id", true) == 0)
                //.Value;

            if (id == null)
            {
                // Get request body
                dynamic data = await req.Content.ReadAsAsync<object>();
                id = data?.id;
            }

            return id == null
                ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass an id on the query string or in the request body")
                : req.CreateResponse(HttpStatusCode.OK, "Hello " + id);
        }
    }
}
