using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using RestChild.Domain;

namespace RestChild.Web.Models
{
	/// <summary>
	///     модель для работы со статусами.
	/// </summary>
	[DataContract(Name = "stateViewModel")]
	public class ViewModelState
	{
		/// <summary>
		///     Статус
		/// </summary>
		[DataMember(Name = "state")]
		public StateMachineState State { get; set; }

		/// <summary>
		///     заголовок.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// нужна кнопка возврат.
		/// </summary>
		public bool CanReturn { get; set; }

		/// <summary>
		/// селектор для формы
		/// </summary>
		public string FormSelector { get; set; }

		/// <summary>
		/// код для селектора
		/// </summary>
		public string ActionSelector { get; set; }

		/// <summary>
		///     Действие для кнопки вернуться.
		/// </summary>
		public string ReturnAction { get; set; }

		/// <summary>
		///     контролер для кнопки вернуться
		/// </summary>
		public string ReturnController { get; set; }

		/// <summary>
		///     параметр для возврата
		/// </summary>
		public object ReturnParametr { get; set; }

		/// <summary>
		///     Нужно кнопку сохранить.
		/// </summary>
		public bool NeedSaveButton { get; set; }

		/// <summary>
		///     Нужно кнопку удалить.
		/// </summary>
		public bool NeedRemoveButton { get; set; }

		/// <summary>
		///     Js функция для изменения статусов.
		/// </summary>
		public string JsFunctionToAction { get; set; }

		/// <summary>
		/// код для селектора комментария
		/// </summary>
		public string CommentSelector { get; set; }

		/// <summary>
		/// действия с комментарием
		/// </summary>
		public List<string> ActionWithComment { get; set; }

		/// <summary>
		/// Не сохранено
		/// </summary>
		public bool NotSaved { get; set; }

		/// <summary>
		///     действия с сущностью
		/// </summary>
		[DataMember(Name = "actions")]
		public List<StateMachineAction> Actions { get; set; }

		/// <summary>
		/// дополнительные действия до действий
		/// </summary>
		[DataMember(Name = "actions")]
		public List<NoStatusAction> PreNoStatusActions { get; set; }

		/// <summary>
		/// дополнительные действия после действий
		/// </summary>
		[DataMember(Name = "actions")]
		public List<NoStatusAction> PostNoStatusActions { get; set; }

		/// <summary>
		/// Информация об ЭП.
		/// </summary>
		public SignInfo Sign { get; set; }

		/// <summary>
		/// получение комментария
		/// </summary>
		public string GetComment(StateMachineAction action)
		{
			if (action == null || ActionWithComment == null || string.IsNullOrEmpty(CommentSelector) || !ActionWithComment.Any(a=>action.ActionCode==a))
			{
				return string.Empty;
			}

			return $", '{CommentSelector}'";
		}

	}
}
