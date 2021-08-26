using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using RestChild.Booking.Logic.Services;

namespace RestChild.Booking
{
	class BookingServiceStarter
	{
		public ServiceHost BookingServiceHost;
		public ServiceHost InternalBookingServiceHost;

		public void OnStart()
		{
			StartBookingServiceHost();
			StartInternalBookingServiceHost();
		}

		public void OnStop()
		{
			StopBookingServiceHost();
			StopInternalBookingServiceHost();
		}

		private void StartInternalBookingServiceHost()
		{
			if (InternalBookingServiceHost != null)
			{
				InternalBookingServiceHost.Close();
			}

			InternalBookingServiceHost = new ServiceHost(typeof(InternalBookingService));
			InternalBookingServiceHost.Open();
		}

		private void StartBookingServiceHost()
		{
			if (BookingServiceHost != null)
			{
				BookingServiceHost.Close();
			}

			BookingServiceHost = new ServiceHost(typeof(BookingService));
			BookingServiceHost.Open();
		}

		private void StopBookingServiceHost()
		{
			if (BookingServiceHost != null)
			{
				BookingServiceHost.Close();
				BookingServiceHost = null;
			}
		}

		private void StopInternalBookingServiceHost()
		{
			if (InternalBookingServiceHost != null)
			{
				InternalBookingServiceHost.Close();
				InternalBookingServiceHost = null;
			}
		}

	}
}
