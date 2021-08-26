namespace MailingDemon.Scheduler.Triggers
{
	using System;
	using System.Threading;
	using System.Xml.Serialization;

	/// <summary>
	///     Триггер, основанный на таймере
	/// </summary>
	public abstract class TimerBasedTrigger : BaseTrigger
	{
		protected TimeSpan IntervalInternal;

		protected Timer Timer;

		protected TimerCallback TriggerCallback;

		protected TimerBasedTrigger()
		{
			TriggerCallback = TimerTrigger;
		}

		[XmlAttribute("interval")]
		public string Interval
		{
			get
			{
				return IntervalInternal.ToString();
			}

			set
			{
				IntervalInternal = TimeSpan.Parse(value);
			}
		}

		public override void OnDeserialize()
		{
			base.OnDeserialize();
			Init(IntervalInternal);
		}

		protected abstract void Init(TimeSpan timeSpan);

		/// <summary>
		///     Обработчик коллбэка от таймера, который триггерит событие у класса BaseTrigger
		/// </summary>
		private void TimerTrigger(object state)
		{
			Trigger();
		}
	}
}