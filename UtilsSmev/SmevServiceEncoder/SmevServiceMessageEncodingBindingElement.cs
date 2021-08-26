using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Xml;
using UtilsSmev.Interface;

namespace UtilsSmev.SmevServiceEncoder
{
	public sealed class SmevServiceMessageEncodingBindingElement : MessageEncodingBindingElement, IWsdlExportExtension
	{
		private readonly MessageVersion _vers = MessageVersion.CreateVersion(EnvelopeVersion.Soap11, AddressingVersion.None);
		private MessageEncodingBindingElement _innerBindingElement;
		private string _senderActor = "http://smev.gosuslugi.ru/actors/smev";

		public SmevServiceMessageEncodingBindingElement(ILoggingSmevRequest logger)
			: this(new TextMessageEncodingBindingElement())
		{
			Logger = logger;
		}

		public SmevServiceMessageEncodingBindingElement(MessageEncodingBindingElement messageEncoderBindingElement)
		{
			_innerBindingElement = messageEncoderBindingElement;
			_innerBindingElement.MessageVersion = _vers;
		}

		public MessageEncodingBindingElement InnerMessageEncodingBindingElement
		{
			get { return _innerBindingElement; }
			set { _innerBindingElement = value; }
		}

		public string SenderActor
		{
			get { return _senderActor; }
			set { _senderActor = value; }
		}

		public override MessageVersion MessageVersion
		{
			get { return _innerBindingElement.MessageVersion; }
			set { _innerBindingElement.MessageVersion = value; }
		}

		private ILoggingSmevRequest Logger { get; set; }

		public void ExportContract(WsdlExporter exporter, WsdlContractConversionContext context)
		{
		}

		public void ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
		{
			((IWsdlExportExtension)_innerBindingElement).ExportEndpoint(exporter, context);
		}

		public override MessageEncoderFactory CreateMessageEncoderFactory()
		{
			return new SmevServiceMessageEncoderFactory("text/xml", "utf-8", _vers, _innerBindingElement.CreateMessageEncoderFactory(), SenderActor, Logger);
		}

		public override BindingElement Clone()
		{
			return new SmevServiceMessageEncodingBindingElement(_innerBindingElement)
			{
				SenderActor = SenderActor,
				Logger = Logger
			};
		}

		public override T GetProperty<T>(BindingContext context)
		{
			return typeof(T) == typeof(XmlDictionaryReaderQuotas)
					   ? _innerBindingElement.GetProperty<T>(context)
					   : base.GetProperty<T>(context);
		}

		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}

			context.BindingParameters.Add(this);
			return context.BuildInnerChannelFactory<TChannel>();
		}

		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}

			context.BindingParameters.Add(this);
			return context.BuildInnerChannelListener<TChannel>();
		}

		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			context.BindingParameters.Add(this);
			return context.CanBuildInnerChannelListener<TChannel>();
		}
	}
}
