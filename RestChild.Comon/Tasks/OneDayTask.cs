using System;
using System.Threading;

namespace RestChild.Comon.Tasks
{
    /// <summary>
    ///     класс для выполнения периодических заданий (раз в сутки).
    /// </summary>
    public class OneDayTask : BaseTask
    {
        public OneDayTask(WaitCallback callback, TimeSpan interval) : base(callback, interval)
        {
        }

        protected override void InitTimer(TimeSpan timeSpan)
        {
            var now = DateTime.Now;
            long differ;

            if (now.TimeOfDay >= timeSpan)
            {
                differ = (long) Math.Floor(86400 - now.TimeOfDay.TotalSeconds + timeSpan.TotalSeconds + 1) * 1000;
            }
            else
            {
                differ = (long) Math.Floor(timeSpan.TotalSeconds - now.TimeOfDay.TotalSeconds + 1) * 1000;
            }

            _triggerEvent = new AutoResetEvent(true);
            Timer = new Timer(o => _triggerEvent.Set(), null, differ, 86401000);
        }
    }
}
