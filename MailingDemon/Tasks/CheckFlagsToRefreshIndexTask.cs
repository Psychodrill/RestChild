using System;
using RestChild.Booking.Logic.Services;

namespace MailingDemon.Tasks
{
	[Task]
	public class CheckFlagsToRefreshIndexTask : BaseTask
	{
		protected override void Execute()
		{
			using (var service = ServiceFactory.GetRestChildrenService())
			{
				try
				{
					service.Execute(s => s.CheckFlagsToRefreshIndex());
				}
				catch (Exception e)
				{
					Logger.Error($"Ошибка при вызове сервиса в {this.GetType()}", e);
				}
			}
		}
	}
}