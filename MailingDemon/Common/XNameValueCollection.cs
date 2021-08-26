using System.Collections.Specialized;
using System.Xml.Serialization;
using MailingDemon.Tasks;

namespace MailingDemon.Common
{
	[XmlType("appSettings")]
	public class XNameValueCollection : BaseConfig
	{
		private NameValueCollectionItem[] _addedItems;
		private NameValueCollection _data;
		private NameValueCollectionItem[] _removeItems;

		public XNameValueCollection()
		{
		}

		public XNameValueCollection(NameValueCollection data)
		{
			_data = new NameValueCollection(data);
		}

		[XmlElement("add")]
		public NameValueCollectionItem[] AddedItems
		{
			get { return _addedItems; }
			set { _addedItems = value; }
		}

		[XmlElement("remove")]
		public NameValueCollectionItem[] RemoveItems
		{
			get { return _removeItems; }
			set { _removeItems = value; }
		}

		public string this[string name]
		{
			get { return _data[name]; }
		}

		public void Apply(XNameValueCollection applicable)
		{
			NameValueCollectionItem[] array = applicable._removeItems;
			foreach (NameValueCollectionItem nameValueCollectionItem in array)
			{
				_data.Remove(nameValueCollectionItem.Name);
			}
			array = applicable._addedItems;
			foreach (NameValueCollectionItem nameValueCollectionItem2 in array)
			{
				_data[nameValueCollectionItem2.Name] = nameValueCollectionItem2.Value;
			}
		}

		public override void OnRestore()
		{
			base.OnRestore();
			_data = new NameValueCollection(_addedItems.Length);
			NameValueCollectionItem[] array = _addedItems;
			foreach (NameValueCollectionItem nameValueCollectionItem in array)
			{
				_data.Add(nameValueCollectionItem.Name, nameValueCollectionItem.Value);
			}
		}

		public class NameValueCollectionItem
		{
			[XmlAttribute("key")] public string Name;
			[XmlAttribute("value")] public string Value;
		}
	}
}