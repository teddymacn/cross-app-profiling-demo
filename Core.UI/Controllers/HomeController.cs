using CoreProfiler;
using CoreProfiler.Web;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HelloMvc
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public async Task<ActionResult> Index()
        {
            var sb = new StringBuilder();

            WriteLog(sb, "<a href=\"coreprofiler/view\">View Profiling Results</a><br />");

            WriteLog(sb, "Calling wcf service of Net45.Api: WcfDemoService.DoWork()...");
            var client = new Net45.Api.WcfDemoServiceClient(Net45.Api.WcfDemoServiceClient.EndpointConfiguration.BasicHttpBinding_IWcfDemoService);
            //client.ChannelFactory.
            try
            {
                await client.DoWorkAsync("from Core.UI");
                WriteLog(sb, "Success!");
            }
            catch
            {
                WriteLog(sb, "Failed!");
            }
            finally
            {
                await client.CloseAsync();
            }

            var url = "http://127.0.0.1/Net45Api/AsyncHandler.ashx";
            WriteLog(sb, "Calling web request of Net45.Api: " + url + "...");
            await ProfilingSession.Current.WebTimingAsync(url, async (correlationId) =>
            {
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        // to be able to drill down a custom web request, you need to set the XCorrelationId request header
                        httpClient.DefaultRequestHeaders.Add(CoreProfilerMiddleware.XCorrelationId, correlationId);
                        await httpClient.GetAsync(url);
                        WriteLog(sb, "Success!");
                    }
                }
                catch
                {
                    WriteLog(sb, "Failed!");
                }
            });

            url = "http://127.0.0.1:3002/?from-CoreUI";
            WriteLog(sb, "Calling web request of Core.Api: " + url + "...");
            await ProfilingSession.Current.WebTimingAsync(url, async (correlationId) =>
            {
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        // to be able to drill down a custom web request, you need to set the XCorrelationId request header
                        httpClient.DefaultRequestHeaders.Add(CoreProfilerMiddleware.XCorrelationId, correlationId);
                        await httpClient.GetAsync(url);
                        WriteLog(sb, "Success!");
                    }
                }
                catch
                {
                    WriteLog(sb, "Failed!");
                }
            });

            return View(sb);
        }

        private void WriteLog(StringBuilder sb, string msg)
        {
            sb.AppendLine(msg);
            sb.Append("<br />");
        }
    }
}