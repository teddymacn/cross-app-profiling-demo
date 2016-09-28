using CoreProfiler.Wcf;
using System.ServiceModel.Description;

namespace Net45.Api
{
    public partial class WcfDemoServiceClient
    {
        static partial void ConfigureEndpoint(ServiceEndpoint serviceEndpoint, ClientCredentials clientCredentials)
        {
            serviceEndpoint.EndpointBehaviors.Add(new WcfProfilingBehavior());
        }
    }
}
