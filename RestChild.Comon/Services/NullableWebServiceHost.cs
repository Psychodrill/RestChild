using System;
using System.ServiceModel;

namespace RestChild.Comon.Services
{
    public sealed class NullableWebServiceHost : ServiceHost
    {
        public NullableWebServiceHost()
        {
        }

        public NullableWebServiceHost(object singletonInstance, params Uri[] baseAddresses)
            : base(singletonInstance, baseAddresses)
        {
        }

        public NullableWebServiceHost(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
        }

        protected override void OnOpening()
        {
            if (Description != null)
            {
                foreach (var endpoint in Description.Endpoints)
                {
                    if (endpoint.Binding != null)
                    {
                        var webHttpBinding = endpoint.Binding as WebHttpBinding;

                        if (webHttpBinding != null)
                        {
                            endpoint.Behaviors.Add(new NullableWebHttpBehavior());
                        }
                    }
                }
            }

            base.OnOpening();
        }
    }
}
