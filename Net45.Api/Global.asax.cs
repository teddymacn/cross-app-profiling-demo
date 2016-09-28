using System;
using EF.Diagnostics.Profiling;
using EF.Diagnostics.Profiling.Web.Import.Handlers;

namespace Net45.Api
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            NanoProfilerImportModule.TryToImportDrillDownResult = true;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // for web applications, profiling is started ad stopped automatically via NanoProfilerModule by default
            // you could add addtional tags or fields like below
            ProfilingSession.Current.AddTag("session tag 1");
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
