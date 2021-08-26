using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace RestChild.Comon.Services
{
    public sealed class NullableWebServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new NullableWebServiceHost(serviceType, baseAddresses);
        }
    }
}
