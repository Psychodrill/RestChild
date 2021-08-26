using System.Xml.Serialization;
using MailingDemon.Tasks;

namespace MailingDemon.Scheduler
{
	/// <summary>
	///     Представление коллекции задач, доступное для (де)сериализации
	/// </summary>
	[XmlType("tasks")]
	public class TaskArray : BaseConfig
	{
		[XmlElement("task")]
		public BaseTask[] Tasks { get; set; }
	}
}