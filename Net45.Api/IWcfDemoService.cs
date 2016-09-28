using System.ServiceModel;

namespace Net45.Api
{
    [ServiceContract]
    public interface IWcfDemoService
    {
        [OperationContract]
        void DoWork(string p1);
    }
}
