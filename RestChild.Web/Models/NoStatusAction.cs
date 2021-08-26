namespace RestChild.Web.Models
{
	/// <summary>
	///     не статусные дествия
	/// </summary>
	public class NoStatusAction
	{
		/// <summary>
		///     имя кнопки
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///     класс кнопки
		/// </summary>
		public string ButtonClass { get; set; }

		/// <summary>
		/// нужно ли иконку для печати
		/// </summary>
		public string IconClass { get; set; }

		/// <summary>
		///     Js функция если нужно
		/// </summary>
		public string JsFunction { get; set; }

		/// <summary>
		///   что то что дописать.
		/// </summary>
		public string SomeAddon { get; set; }

		/// <summary>
		///     контроллер
		/// </summary>
		public string Controller { get; set; }

		/// <summary>
		///     дейтсвие
		/// </summary>
		public string Action { get; set; }

		/// <summary>
		///     параметры действия
		/// </summary>
		public object ActionParameters { get; set; }
	}
}