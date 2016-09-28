using CoreProfiler;
using CoreProfiler.Web;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HelloMvc
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public async Task<ActionResult> Index()
        {
            await ProfilingSession.Current.WebTimingAsync("http://127.0.0.1/Net45Api/WcfDemoService.svc", async (correlationId) =>
            {
                var client = new Net45.Api.WcfDemoServiceClient(Net45.Api.WcfDemoServiceClient.EndpointConfiguration.BasicHttpBinding_IWcfDemoService);
                try
                {
                    await client.DoWorkAsync("from Core.UI");
                }
                finally
                {
                    await client.CloseAsync();
                }
            });

            return View();
        }
    }
}