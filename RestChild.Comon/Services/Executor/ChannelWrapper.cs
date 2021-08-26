using System;
using System.ServiceModel;

namespace RestChild.Comon.Services.Executor
{
    public class ChannelWrapper<TChannel> : IDisposable
    {
        private readonly TChannel _channel;
        private bool _isOpen;

        public ChannelWrapper(TChannel channel)
        {
            _channel = channel;
            _isOpen = ((ICommunicationObject) _channel).State == CommunicationState.Opened;
        }

        public TChannel Channel
        {
            get
            {
                if (!_isOpen)
                {
                    OpenChannel();
                }

                return _channel;
            }
        }

        public void Dispose()
        {
            var communicationObject = (ICommunicationObject) _channel;
            try
            {
                communicationObject.Close();
            }
            catch (Exception)
            {
                communicationObject.Abort();
            }

            _isOpen = false;
        }

        public ChannelWrapper<TChannel> OpenChannel()
        {
            ((ICommunicationObject) _channel).Open();
            _isOpen = true;
            return this;
        }
    }
}
