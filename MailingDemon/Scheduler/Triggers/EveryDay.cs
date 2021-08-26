namespace MailingDemon.Scheduler.Triggers
{
	using System;
	using System.Threading;

	/// <summary>
	///     Триггер, который триггерится каждый день в указанное время
	/// </summary>
	[TaskTrigger]
	public class EveryDay : TimerBasedTrigger
	{
		protected override void Init(TimeSpan timeSpan)
		{
			var now = DateTime.Now;
			long differ;

			if (now.TimeOfDay >= timeSpan)
			{
				differ = (long)Math.Floor(86400 - now.TimeOfDay.TotalSeconds + timeSpan.TotalSeconds + 1) * 1000;
			}
			else
			{
				differ = (long)Math.Floor(timeSpan.TotalSeconds - now.TimeOfDay.TotalSeconds + 1) * 1000;
			}

			Timer = new Timer(TriggerCallback, null, differ, 86401000);
		}
	}
}