using System;
using System.ServiceModel;
using RestChild.Comon.Services;
using RestChild.Comon.Services.Executor;

namespace RestChild.Booking.Logic.Services
{
	public class ServiceFactory
	{
		private static readonly string ConnectionString;

		static ServiceFactory()
		{
			ConnectionString = Settings.Default.RestManIndexServiceConnectionString;
		}

		public static ServiceExecutor<IRestChildrenService> GetRestChildrenService()
		{
			var channeFactory = new ChannelFactory<IRestChildrenService>(new NetTcpBinding(SecurityMode.None) {MaxReceivedMessageSize = Int32.MaxValue} );
			channeFactory.Endpoint.Address = new EndpointAddress(new Uri(ConnectionString));
			return new ServiceExecutor<IRestChildrenService>(new ChannelWrapper<IRestChildrenService>(channeFactory.CreateChannel()));
		}
	}
}
