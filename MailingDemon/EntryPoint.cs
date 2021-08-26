using System.ComponentModel;
using MailingDemon.Common;
using RestChild.DAL;

namespace MailingDemon
{
	using System;
	using System.Diagnostics;
	using System.Reflection;
	using System.ServiceProcess;
	using System.Threading;

	using log4net;
	using log4net.Config;

	using Scheduler;

	public class EntryPoint : ServiceBase
	{
		/// <summary>
		///     Required designer variable.
		/// </summary>
        // ReSharper disable once InconsistentNaming
        private Container components;

		#region Main

		//// The main entry point for the process
		//private static void Main()
		//{
		//    // More than one user Service may run within the same process. To add
		//    // another service to this process, change the following line to
		//    // create a second service object. For example,
		//    //
		//    //   ServicesToRun = new System.ServiceProcess.ServiceBase[] {new Service1(), new MySecondUserService()};
		//    //
		//    ServiceBase[] ServicesToRun = new ServiceBase[] { new EntryPoint() };

		//    Run(ServicesToRun);
		//}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				components?.Dispose();
			}

			base.Dispose(disposing);
		}

		private static void Main(string[] args)
		{
			if (Environment.UserInteractive)
			{
				Console.WriteLine("RestChild data synchronization starting...");
			}

			var serviceToRun = new EntryPoint();

			if (Environment.UserInteractive)
			{
				serviceToRun.OnStart(args);
				Console.WriteLine("RestChild data synchronization process started.");
				Console.WriteLine("Press any key to stop process...");
				Console.CancelKeyPress += delegate { serviceToRun.OnStop(); };
				Console.ReadKey();
				serviceToRun.OnStop();
				Console.WriteLine();
				Console.WriteLine("RestChild data synchronization has been stopped.");
			}
			else
			{
				// все сертификаты ок внутри MD
				System.Net.ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
				Run(serviceToRun);
			}
		}

		#endregion

		#region Constructor

		public EntryPoint()
		{
			// This call is required by the Windows.Forms Component Designer.
			InitializeComponent();
		}

		#endregion

		#region Fields

		private ILog _logger;
		private TaskCollection _tasks;

		#endregion

		#region Properties

		#endregion

		#region Events

		/// <summary>
		///     Set things in motion so your service can do its work.
		/// </summary>
		protected override void OnStart(string[] args)
		{
			//Debugger.Break();
			_logger = LogManager.GetLogger("Execution engine");
			try
			{
				var dc = (DebugConfig) ConfigManager.AppSettings.Configure(typeof (DebugConfig));
				_logger.Info("Service started.");
				using (var uw = new UnitOfWork())
				{
					RestChild.Booking.Logic.Booking.FillDecodeTypeOfRest(uw);
				}
#if DEBUG
				if (dc.DebugBreak)
					Debugger.Break();
				else
					Thread.Sleep(dc.SleepTime);
#endif
				_tasks = TaskCollection.Load();
				_tasks.Start();
			}
			catch (Exception ex)
			{
				_logger.Fatal("An exception has occured during scheduler initialization.", ex);
				throw;
			}
		}

		/// <summary>
		///     Stop this service.
		/// </summary>
		protected override void OnStop()
		{
			_tasks.Stop();
			_logger.Info("Service stopped.");
		}

		protected override void OnContinue()
		{
			_tasks.Resume();
		}

		protected override void OnPause()
		{
			_tasks.Pause();
		}

		#endregion

		#region Helpers

		/// <summary>
		///     Required method for Designer support - do not modify
		///     the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			//
			// EntryPoint
			//
			ServiceName = "Mailing Demon RestChild";

			XmlConfigurator.Configure();
			CanPauseAndContinue = true;
			Assembly asm = Assembly.GetExecutingAssembly();
			object[] attrList = asm.GetCustomAttributes(false);
			foreach (object o in attrList)
			{
				var attribute = o as AssemblyTitleAttribute;
				if (attribute != null)
					ServiceName = attribute.Title;
			}
		}

		#endregion
	}
}
