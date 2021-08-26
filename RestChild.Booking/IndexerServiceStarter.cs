using System;
using System.ServiceModel;
using log4net;
using RestChild.Booking.Logic.Services;

namespace RestChild.Booking
{
	public class IndexerServiceStarter
	{
		private ServiceHost _indexServiceHost;
		private ServiceHost _restChildindexServiceHost;

		public IndexerServiceStarter()
		{
			LogManager.GetLogger(typeof (IndexerService)).Info("Init service");
		}

		public void OnStart()
		{
			try
			{
				LogManager.GetLogger(typeof (IndexerService)).Info("Start service host");
				StartHost();
			}
			catch (Exception ex)
			{
				LogManager.GetLogger(typeof (IndexerService)).Error("Ошибка старта сервиса", ex);
				throw;
			}
		}

		private void StartHost()
		{
			
			_restChildindexServiceHost?.Close();

			_restChildindexServiceHost = new ServiceHost(typeof (RestChildrenService));

			_restChildindexServiceHost.Open();
		}

		public void StopHosts()
		{
			TryStopHost(_indexServiceHost);
			_indexServiceHost = null;

			TryStopHost(_restChildindexServiceHost);
			_restChildindexServiceHost = null;
		}

		private void TryStopHost(ServiceHost indexServiceHost)
		{
			if (indexServiceHost != null)
			{
				try
				{
					indexServiceHost.Close();
				}
				catch (Exception)
				{
					indexServiceHost.Abort();
				}
			}
		}
	}
}
