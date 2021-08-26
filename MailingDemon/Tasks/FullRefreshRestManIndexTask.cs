using System;
using RestChild.Booking.Logic.Services;

namespace MailingDemon.Tasks
{
	[Task]
	public class FullRefreshRestManIndexTask : BaseTask
	{
		protected override void Execute()
		{
			var service = ServiceFactory.GetRestChildrenService();
			try
			{
				service.Execute(s => s.RebuildIndex());
			}
			catch (Exception e)
			{
				Logger.Error($"Ошибка при вызове сервиса в {this.GetType()}", e);
			}
		}
	}
}