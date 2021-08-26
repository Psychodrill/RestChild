using MailingDemon.Common;

namespace MailingDemon.Scheduler
{
	using System;

	/// <summary>
	///     Класс-планировщик. Предназначен для инициализации и запуска по рассписанию заданий.
	///     Поддерживает только одно расписание задач
	/// </summary>
	public class TaskScheduler
	{
		public static TaskCollection Schedule;

		public static void Initialize()
		{
			if (Schedule != null)
			{
				throw new Exception("Double initialization of schedules!");
			}

			Schedule = (TaskCollection)ConfigManager.AppSettings.Configure(typeof(TaskCollection));
		}
	}
}