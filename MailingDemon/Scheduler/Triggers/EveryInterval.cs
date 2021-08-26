namespace MailingDemon.Scheduler.Triggers
{
	using System;
	using System.Threading;

	/// <summary>
	///     Триггер с повторением каждый промежуток времени
	/// </summary>
	[TaskTrigger]
	public class EveryInterval : TimerBasedTrigger
	{
		protected override void Init(TimeSpan timeSpan)
		{
			Timer = new Timer(TriggerCallback, null, 5000, (long)Math.Floor(timeSpan.TotalSeconds) * 1000);
		}
	}
}