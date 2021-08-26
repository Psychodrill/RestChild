using System;
using System.Threading;

namespace RestChild.Comon.Tasks
{
    public abstract class BaseTask
    {
        protected readonly WaitCallback _callback;
        protected readonly TimeSpan _interval;
        protected AutoResetEvent _triggerEvent;
        protected Timer Timer;

        public BaseTask(WaitCallback callback, TimeSpan interval)
        {
            _callback = callback;
            _interval = interval;
        }

        public void Start()
        {
            InitTimer(_interval);

            while (true)
            {
                _triggerEvent.WaitOne();
                _triggerEvent.Reset();
                _callback(null);
            }

// ReSharper disable FunctionNeverReturns
        }

// ReSharper restore FunctionNeverReturns
        protected abstract void InitTimer(TimeSpan timeSpan);
    }
}
