using System;
using System.Threading;

namespace RestChild.Comon.Tasks
{
    /// <summary>
    ///     таск для повторения задач
    /// </summary>
    public class RepeaterTask : BaseTask
    {
        public RepeaterTask(WaitCallback callback, TimeSpan interval) : base(callback, interval)
        {
        }

        protected override void InitTimer(TimeSpan timeSpan)
        {
            _triggerEvent = new AutoResetEvent(true);
            Timer = new Timer(o => _triggerEvent.Set(), null, Convert.ToInt64(timeSpan.TotalMilliseconds),
                Convert.ToInt64(timeSpan.TotalMilliseconds));
        }
    }
}
