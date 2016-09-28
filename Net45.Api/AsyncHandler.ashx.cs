using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

using EF.Diagnostics.Profiling;
using EF.Diagnostics.Profiling.Web;
using Net45.Api.DemoService;

namespace Net45.Api
{
    public class AsyncHandler : HttpTaskAsyncHandler
    {
        public override bool IsReusable
        {
            get { return true; }
        }

        public override async Task ProcessRequestAsync(HttpContext context)
        {
            using (ProfilingSession.Current.Step("ProcessRequestAsync"))
            {
                context.Response.Write("Running now!");

                using (var client = new WcfDemoServiceClient())
                {
                    await client.DoWorkAsync("from Net45.Api.AsyncHandler.ashx");
                }

                await CallWebRequest("http://127.0.0.1:3002/?from-Net45Api");
            }
        }

        private static async Task CallWebRequest(string url)
        {
            var profilingSession = ProfilingSession.Current;
            if (profilingSession != null && profilingSession.Profiler != null)
            {
                var webTiming = new WebTiming(profilingSession.Profiler, url);
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Add("X-ET-Correlation-Id", webTiming.CorrelationId);
                        await httpClient.GetAsync(url);
                    }
                }
                finally
                {
                    webTiming.Stop();
                }
            }
        }
    }
}
