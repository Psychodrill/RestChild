using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class BoutModel : ViewModelBase<Bout>
	{
		public ViewModelState State { get; set; }

		public string StateMachineActionString { get; set; }

		public string ActiveTab { get; set; }

		public bool IsEditable { get; set; }

		public int ChildrenCount { get; set; }

		public int VacantChildrenCount { get; set; }

		[AllowHtml]
		public string Comment { get; set; }

		/// <summary>
		/// Пользователь системы являеся администратором смены в заезде
		/// </summary>
		public bool IsBoutAdministartor { get; set; }

		public BoutModel()
			: this(new Bout())
		{

		}

		public BoutModel(Bout model)
			:base(model)
		{
			TransportInfoFrom = model.TransportInfoFrom != null ? new TransportInfoModel(model.TransportInfoFrom) : null;
			TransportInfoTo = model.TransportInfoTo != null ? new TransportInfoModel(model.TransportInfoTo) : null;
			Comment = model.Comment;
		}

		public ICollection<TimeOfRest> TimesOfRest { get; set; }

		public TransportInfoModel TransportInfoFrom { get; set; }

		public TransportInfoModel TransportInfoTo { get; set; }
		/// <summary>
		/// используемые заявления
		/// </summary>
		public List<Request> Requests { get; set; }

		/// <summary>
		/// Причины отсутствия необходимости в обратном билете
		/// </summary>
		public List<NotNeedTicketReason> NotNeedTicketReasons { get; set; }

		#region Транспорт
		/// <summary>
		/// Транспорт для администраторов
		/// </summary>
		public Dictionary<long, LinkToPeople> AdministratorTransportForward;

		/// <summary>
		/// Обратный транспорт для администраторов
		/// </summary>
		public Dictionary<long, LinkToPeople> AdministratorTransportBackward;

		/// <summary>
		/// Транспорт для вожатых
		/// </summary>
		public Dictionary<long, LinkToPeople> CounselorTransportForward;

		/// <summary>
		/// Обратный транспорт для вожатых
		/// </summary>
		public Dictionary<long, LinkToPeople> CounselorTransportBackward;

		/// <summary>
		/// Транспорт для сопровождающих
		/// </summary>
		public Dictionary<long, LinkToPeople> AttendantTransportForward;

		/// <summary>
		/// Обратный транспорт для сопровождающих
		/// </summary>
		public Dictionary<long, LinkToPeople> AttendantTransportBackward;
		#endregion

		/// <summary>
		/// Проверка возможности перейти в статус "Подтверждено"
		/// </summary>
		/// <returns></returns>
		public bool CanTransferToConfirmedState()
		{
			if (Data.StateId != StateMachineStateEnum.Bout.Confirmed)
			{
				var dateIncome = GetDateOfBoutStart();

				return DateTime.Today.Subtract(dateIncome).Days >= 3;
			}

			return false;
		}

		/// <summary>
		/// Получение даты начала заезда
		/// </summary>
		/// <returns></returns>
		public DateTime GetDateOfBoutStart()
		{
			DateTime dateIncome;
			if (Data.DateIncome.HasValue)
			{
				dateIncome = Data.DateIncome.Value.Date;
			}
			else
			{
				dateIncome = (Data.Tours ?? new List<Tour>()).Select(t => t.DateIncome).DefaultIfEmpty().Max()?.Date??DateTime.MinValue;
			}
			return dateIncome;
		}

		public override Bout BuildData()
		{
			var data = base.BuildData();
			data.TransportInfoFrom = TransportInfoFrom.BuildData();
			data.TransportInfoTo = TransportInfoTo.BuildData();
			data.Comment = Comment;

			return data;
		}
	}
}
