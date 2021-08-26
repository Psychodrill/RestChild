using log4net;
using UtilsSmev.Interface;

namespace UtilsSmev
{
	public class Logging : ILoggingSmevRequest
	{
		public void SaveMessage(bool input, string message, string name)
		{
			LogManager.GetLogger("requestLogging").Info($"{name}-{input}-{message}");
		}
	}
}