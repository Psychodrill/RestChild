using MailingDemon.Common;

namespace MailingDemon.Scheduler
{
	using System;
	using System.Collections;
	using System.Reflection;
	using System.Threading;

	using log4net;

	using MailingDemon.Scheduler.Triggers;
	using MailingDemon.Tasks;

	/// <summary>
	///     Коллекция задач
	/// </summary>
	public class TaskCollection : BaseConfig, ITaskController
	{
		private static readonly ArrayList RegisteredTypes = new ArrayList();

		private readonly ILog _logger;

		private readonly ArrayList _supportedTasks;

		static TaskCollection()
		{
			RegisterKnownTypesFromAssembly(Assembly.GetAssembly(typeof(TaskCollection)));
		}

		/// <summary>
		///     Конструктор коллекции по набору тасков
		/// </summary>
		/// <param name="array">(Де)сериализуемый набор задач</param>
		private TaskCollection(TaskArray array)
		{
			_logger = LogManager.GetLogger(GetType());
			_supportedTasks = new ArrayList(0);
			foreach (var task in array.Tasks)
			{
				if (task.Active)
				{
					_supportedTasks.Add(task);
					task.OnDeserialize();
				}
			}

			if (_supportedTasks.Count == 0)
			{
				_logger.Info("Нет активных задач.");
			}
		}

		public static TaskCollection Load()
		{
			var array = (TaskArray)ConfigManager.AppSettings.Configure(typeof(TaskArray), RegisteredTypes);
			var instance = new TaskCollection(array);
			return instance;
		}

		/// <summary>
		///     Зарегистрировать тип для десериализации
		/// </summary>
		/// <param name="knownType"></param>
		public static void RegisterKnownType(Type knownType)
		{
			if (!RegisteredTypes.Contains(knownType))
			{
				RegisteredTypes.Add(knownType);
			}
		}

		public void Pause()
		{
			var pausingEventsList = new ArrayList(_supportedTasks.Count);
			foreach (BaseTask t in _supportedTasks)
			{
				try
				{
					pausingEventsList.Add(t.PausingEvent);
					t.Pause();
				}
				catch (Exception e)
				{
					_logger.ErrorFormat("Error during '{0}' task startup: {1}", t.Description, e);
				}
			}

			var paused = (WaitHandle[])pausingEventsList.ToArray(typeof(WaitHandle));

			// дождаться, пока все таски сообщат о том, что готовы приостановиться
			// примечание: это не значит, что они реально остановятся, но инструкция
			// остановки будет следующей
			WaitHandle.WaitAll(paused);
		}

		public void Resume()
		{
			foreach (BaseTask t in _supportedTasks)
			{
				try
				{
					t.Resume();
				}
				catch (Exception e)
				{
					_logger.ErrorFormat("Error during '{0}' task resuming: {1}", t.Description, e);
				}
			}
		}

		public void Start()
		{
			foreach (BaseTask t in _supportedTasks)
			{
				try
				{
					t.Start();
				}
				catch (Exception e)
				{
					_logger.ErrorFormat("Error during '{0}' task startup: {1}", t.Description, e);
				}
			}
		}

		public void Stop()
		{
			foreach (BaseTask t in _supportedTasks)
			{
				try
				{
					t.Stop();
				}
				catch (Exception e)
				{
					_logger.ErrorFormat("Error during '{0}' task termination: {1}", t.Description, e);
				}
			}
		}

		/// <summary>
		///     Зарегистрировать все известные типы из указанной сборки
		/// </summary>
		/// <param name="ass">Сборка</param>
		private static void RegisterKnownTypesFromAssembly(Assembly ass)
		{
			var trigger = typeof(TaskTrigger);
			var task = typeof(TaskAttribute);

			// найти все классы с установленными атрибутами TaskTrigger и Task
			Helpers.FindClassesWithAttributes(ass, RegisteredTypes, trigger, task);
		}
	}
}