using System.ServiceProcess;
using log4net;

namespace RestChild.Booking
{
	partial class IndexerService : ServiceBase
	{
		private readonly IndexerServiceStarter _starter;


		public IndexerService()
		{
			AutoLog = true;
			LogManager.GetLogger(typeof (IndexerService)).Info("Init service");
			InitializeComponent();
			_starter = new IndexerServiceStarter();
		}

		protected override void OnStart(string[] args)
		{
			_starter.OnStart();
		}

		protected override void OnStop()
		{
			_starter.StopHosts();
		}
	}
}