using System;

namespace RestChild.Comon.Services.Executor
{
    public class ServiceExecutor<TService> : IDisposable
    {
        private readonly ChannelWrapper<TService> _channelWrapper;

        public ServiceExecutor(ChannelWrapper<TService> channelWrapper)
        {
            _channelWrapper = channelWrapper;
        }

        public void Dispose()
        {
            _channelWrapper?.Dispose();
        }

        public void Execute(Action<TService> action)
        {
            using (var channelWrapper = _channelWrapper.OpenChannel())
            {
                action(channelWrapper.Channel);
            }
        }

        public TResult Execute<TResult>(Func<TService, TResult> func)
        {
            using (var channelWrapper = _channelWrapper.OpenChannel())
            {
                return func(channelWrapper.Channel);
            }
        }
    }
}
