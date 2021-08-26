namespace MailingDemon.Scheduler.Triggers
{
	using System;
	using System.Threading;
	using System.Xml.Serialization;

	/// <summary>
	///     Триггер, который триггерится в указанное время определённого дня недели
	/// </summary>
	[TaskTrigger]
	public class EveryWeek : TimerBasedTrigger
	{
		private int _dayOfWeek = -1;

		/// <summary>
		///     День недели, в который срабатывает триггер. 0 -- понедельник
		/// </summary>
		[XmlAttribute("day")]
		public string Day
		{
			get
			{
				return ((DayOfWeek)_dayOfWeek).ToString();
			}

			set
			{
				_dayOfWeek = (int)Enum.Parse(typeof(DayOfWeek), value, true);
			}
		}

		/// <summary>
		///     Тип расписания -- время от начала недели
		/// </summary>
		protected override void Init(TimeSpan timeSpan)
		{
			var adjustedTimer = timeSpan;

			// если день указан явно, то составить timespan из него, иначе
			// считать, что таймспэн задан с учётом номера дня
			if (_dayOfWeek > -1)
			{
				adjustedTimer = new TimeSpan(_dayOfWeek, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
			}

			var now = DateTime.Now;

			// время, прошедшее с начала недели
			var timePassedSinceStartOfWeek = new TimeSpan((int)now.DayOfWeek, now.Hour, now.Minute, now.Second);
			var wholeWeek = new TimeSpan(7, 0, 0, 0);

			TimeSpan dueTime;

			// время до наступления желаемого момента
			if (timePassedSinceStartOfWeek < adjustedTimer)
			{
				// если момент времени ещё не наступил на этой неделе
				dueTime = adjustedTimer - timePassedSinceStartOfWeek;
			}
			else
			{
				// если момент времени на данной неделе уже прошёл
				dueTime = wholeWeek + adjustedTimer - timePassedSinceStartOfWeek;
			}

			Timer = new Timer(TriggerCallback, null, dueTime, wholeWeek);
		}
	}
}