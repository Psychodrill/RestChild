using System;
using System.ServiceProcess;
using System.Threading;
using log4net;
using RestChild.Comon.Tasks;

namespace RestChild.Booking
{
	public partial class MainService : ServiceBase
	{
		private static Thread _updater;
		private static Thread _cleaner;
		private readonly BookingServiceStarter _bookingServiceStarter;


		public MainService()
		{
			InitializeComponent();
			_bookingServiceStarter = new BookingServiceStarter();
		}

		protected override void OnStart(string[] args)
		{
			try
			{
				StartBookingRefresher();
				_bookingServiceStarter.OnStart();
			}
			catch (Exception ex)
			{
				LogManager.GetLogger(typeof (Logic.Booking)).Error("Ошибка старта сервиса", ex);
				throw;
			}
		}

		protected override void OnStop()
		{
			_bookingServiceStarter.OnStop();
			_updater.Abort();
		}

		private void StartBookingRefresher()
		{
			var maxThreadsCount = Environment.ProcessorCount*132;
			// Установим максимальное количество рабочих потоков
			ThreadPool.SetMaxThreads(maxThreadsCount, maxThreadsCount);
			// Установим минимальное количество рабочих потоков
			ThreadPool.SetMinThreads(2, 2);

			// запуск
			var scheduler = new OneDayTask(o =>
			{
				try
				{
					LogManager.GetLogger(typeof (Logic.Booking)).Info("Инициализация кэшей загрузки");
					Logic.Booking.LoadServices();
					LogManager.GetLogger(typeof (Logic.Booking)).Info("Загружено расписание приема");
				}
				catch (Exception ex)
				{
					LogManager.GetLogger(typeof (Logic.Booking)).Error("Ошибка загрузки слотов", ex);
					throw;
				}
			},
				new TimeSpan(5, 0, 0));

			_updater = new Thread(scheduler.Start);
			_updater.Start();

			var cleanerSheduler = new RepeaterTask(o =>
			{
				try
				{
					Logic.Booking.ReleaseAllOverduePreBooking();
				}
				catch (Exception ex)
				{
					LogManager.GetLogger(typeof (Logic.Booking))
						.Error("Ошибка запуска ReleaseAllOverduePreBooking", ex);
				}
			},
				new TimeSpan(0,0,8,0));

			_cleaner = new Thread(cleanerSheduler.Start);
			_cleaner.Start();
		}
	}
}
