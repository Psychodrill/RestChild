// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using log4net;

namespace RestChild.Comon.Services
{
    public class Log4NetTraceListener : TraceListener
    {
        private readonly ILog log;

        public Log4NetTraceListener()
        {
            log = LogManager.GetLogger("System.Diagnostics.Redirection");
        }

        public Log4NetTraceListener(ILog log)
        {
            this.log = log;
        }

        public override void Write(string message)
        {
            if (log != null)
            {
                log.Debug(message);
            }
        }

        public override void WriteLine(string message)
        {
            if (log != null)
            {
                log.Debug(message);
            }
        }
    }

    public class Log4NetErrorHandler : IErrorHandler
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public bool HandleError(Exception error)
        {
            log.Error("An unexpected has occurred.", error);

            return false; // Exception has to pass the stack further
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
        }
    }

    public class Log4NetServiceBehavior : IServiceBehavior
    {
        /// <summary>
        ///     Provides the ability to pass custom data to binding elements to support the contract implementation.
        /// </summary>
        /// <param name="serviceDescription">The service description of the service.</param>
        /// <param name="serviceHostBase">The host of the service.</param>
        /// <param name="endpoints">The service endpoints.</param>
        /// <param name="bindingParameters">Custom objects to which binding elements have access.</param>
        public void AddBindingParameters(ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            var errorHandler = new Log4NetErrorHandler();

            foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
            {
                channelDispatcher.ErrorHandlers.Add(errorHandler);
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }

    public class Log4NetBehaviorExtensionElement : BehaviorExtensionElement
    {
        public override Type BehaviorType => typeof(Log4NetServiceBehavior);

        protected override object CreateBehavior()
        {
            return new Log4NetServiceBehavior();
        }
    }
}
