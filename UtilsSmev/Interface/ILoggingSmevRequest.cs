namespace UtilsSmev.Interface
{
	public interface ILoggingSmevRequest
	{
		/// <summary> сохранение запроса. </summary>
		void SaveMessage(bool input, string message, string name);
	}
}
