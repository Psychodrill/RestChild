namespace MailingDemon.Scheduler.Triggers
{
	using System.Threading;
	using System.Xml.Serialization;

	/// <summary>
	///     Базовый класс триггера запуска задачи. Основан на AutoResetEvent
	/// </summary>
	[XmlType("trigger")]
	public abstract class BaseTrigger
	{
		private AutoResetEvent _triggerEvent;

		/// <summary>
		///     Начальное состояние события. true -- сигналит, false -- не сигналит
		/// </summary>
		public bool InitialState { get; set; }

		[XmlIgnore]
		public WaitHandle WaitableHandle
		{
			get
			{
				return _triggerEvent;
			}
		}

		/// <summary>
		///     Метод, вызываемый на десериализации триггера
		/// </summary>
		public virtual void OnDeserialize()
		{
			_triggerEvent = new AutoResetEvent(InitialState);
		}

		public void Trigger()
		{
			_triggerEvent.Set();
		}
	}
}