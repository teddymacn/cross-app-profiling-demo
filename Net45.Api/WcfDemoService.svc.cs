using System.Threading;
using EF.Diagnostics.Profiling;

namespace Net45.Api
{
    public class WcfDemoService : IWcfDemoService
    {
        public void DoWork(string p1)
        {
            using (ProfilingSession.Current.Step(() => "Do some work: " + p1))
            {
                Thread.Sleep(200);
            }
        }
    }
}
