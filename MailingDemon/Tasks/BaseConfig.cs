using System;
using System.Xml.Serialization;

namespace MailingDemon.Tasks
{
	[Serializable]
	public abstract class BaseConfig
	{
		public delegate bool OnUpdateHandler(BaseConfig updatedSettings);

		protected string EntityName = null;

		[XmlAttribute("ObjectName")]
		public string Name
		{
			get { return EntityName; }
			set { EntityName = value; }
		}

		public event OnUpdateHandler OnUpdate;

		public void Update(BaseConfig updated)
		{
			bool flag = true;
			if (OnUpdate != null)
			{
				flag &= OnUpdate(updated);
			}
			if (flag)
			{
				AcceptUpdate(updated);
			}
		}

		internal virtual void AcceptUpdate(BaseConfig updatedSettings)
		{
		}

		internal virtual void Validate(BaseConfig questionable)
		{
		}

		public virtual void OnRestore()
		{
		}
	}
}