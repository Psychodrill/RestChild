using System;
using System.IO;
using System.Reflection;

namespace RestChild.Booking
{
	using System.ServiceProcess;

	internal static class Program
	{
		/// <summary>
		///     The main entry point for the application.
		/// </summary>
		private static void Main()
		{
			var executingAssembly = Assembly.GetExecutingAssembly();
			var location = executingAssembly.Location;
			var directoryName = Path.GetDirectoryName(location);

			Directory.SetCurrentDirectory(directoryName);

			if (Environment.UserInteractive)
			{
				Console.WriteLine("Bootstrap started");
				Logic.Booking.LoadServices();
				var indexerServiceStarter = new IndexerServiceStarter();
				indexerServiceStarter.OnStart();

				var bookingServiceStarter = new BookingServiceStarter();
				bookingServiceStarter.OnStart();

				Console.WriteLine("Service started");
				Console.ReadKey();

				indexerServiceStarter.StopHosts();

			}
			else
			{
				var servicesToRun = new ServiceBase[]
								{
												new MainService(),
												new IndexerService()
								};

				ServiceBase.Run(servicesToRun);
			}
		}
	}
}
