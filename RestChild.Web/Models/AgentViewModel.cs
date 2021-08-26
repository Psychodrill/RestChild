using System;
using System.ComponentModel.DataAnnotations;
//using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Domain;
using RestChild.Web.Common;

namespace RestChild.Web.Models
{
	public class AgentViewModel : ViewModelBase<Agent>
	{
		public AgentViewModel() : base(new Agent())
		{
			Guid = System.Guid.NewGuid();
		}

		public AgentViewModel(Agent entity, Applicant subEntity) : base(entity)
		{
			Guid = System.Guid.NewGuid();
			if (entity == null) return;
			DataApplicant = subEntity ?? new Applicant {IsAgent = true };
		}

		public Applicant DataApplicant { get; set; }

		public Applicant BuidApplicant()
		{
			if (DataApplicant == null || !DataApplicant.IsAccomp)
			{
				return null;
			}

			DataApplicant.LastName = Data.LastName;
			DataApplicant.FirstName = Data.FirstName;
			DataApplicant.MiddleName = Data.MiddleName;
			DataApplicant.HaveMiddleName = Data.HaveMiddleName;
			DataApplicant.DocumentSeria = Data.DocumentSeria;
			DataApplicant.DocumentCode = Data.DocumentCode;
			DataApplicant.DateOfBirth = Data.DateOfBirth;
			DataApplicant.Male = Data.Male;
			DataApplicant.PlaceOfBirth = Data.PlaceOfBirth;
			DataApplicant.DocumentNumber = Data.DocumentNumber;
			DataApplicant.DocumentDateOfIssue = Data.DocumentDateOfIssue;
			DataApplicant.DocumentSubjectIssue = Data.DocumentSubjectIssue;
			DataApplicant.Phone = Data.Phone;
			DataApplicant.Email = Data.Email;
			DataApplicant.Snils = Data.Snils;
			DataApplicant.IsApplicant = false;
			DataApplicant.IsLast = Data.IsLast;
			DataApplicant.IsAgent = true;
			DataApplicant.DocumentTypeId = Data.DocumentTypeId;
			DataApplicant.LastUpdateTick = Data.LastUpdateTick;
			DataApplicant.Eid = Data.Eid;
			DataApplicant.EidSendStatus = Data.EidSendStatus;
			DataApplicant.EidSyncDate = Data.EidSyncDate;
			DataApplicant.IsProxy = true;
			return DataApplicant;
		}

		#region Проверка формы

		public Guid? Guid { get; set; }

		/// <summary>
		///     Фамилия
		/// </summary>
		[Display(Description = "Фамилия")]
		public virtual string LastNameEm { get; set; }

		/// <summary>
		///     Имя
		/// </summary>
		[Display(Description = "Имя")]
		public virtual string FirstNameEm { get; set; }

		/// <summary>
		///     Отчество
		/// </summary>
		[Display(Description = "Отчество")]
		public virtual string MiddleNameEm { get; set; }

		/// <summary>
		///     Серия документа
		/// </summary>
		[Display(Description = "Серия документа удостоверяющего личность")]
		public virtual string DocumentSeriaEm { get; set; }

		/// <summary>
		///     Номер документа
		/// </summary>
		[Display(Description = "Номер документа удостоверяющего личность")]
		public virtual string DocumentNumberEm { get; set; }

		/// <summary>
		///     Дата выдачи документа
		/// </summary>
		[Display(Description = "Когда выдан документ удостоверяющей личность")]
		public virtual string DocumentDateOfIssueEm { get; set; }

		/// <summary>
		///     Кем выдан документ
		/// </summary>
		[Display(Description = "Кем выдан документ удостоверяющий личность")]
		public virtual string DocumentSubjectIssueEm { get; set; }

        /// <summary>
        ///     СНИЛС
        /// </summary>
        [Display(Description = "СНИЛС")]
        public virtual string SnilsEm { get; set; }

		/// <summary>
		///     Телефон
		/// </summary>
		[Display(Description = "Телефон")]
		public virtual string PhoneEm { get; set; }

		/// <summary>
		///     Дата рождения
		/// </summary>
		[Display(Description = "Дата рождения")]
		public virtual string DateOfBirthEm { get; set; }

		/// <summary>
		///     Представляет интересы
		/// </summary>
		[Display(Description = "Представляет интересы")]
		public virtual string RepresentInterestEm { get; set; }

		/// <summary>
		///     Электронная почта
		/// </summary>
		[Display(Description = "Электронная почта")]
		public virtual string EmailEm { get; set; }

		/// <summary>
		///     Вид документа представителя
		/// </summary>
		[Display(Description = "Вид документа удостоверяющего личность")]
		public virtual string DocumentTypeEm { get; set; }

		/// <summary>
		/// Дата выдачи доверенности
		/// </summary>
		[Display(Description = "Дата выдачи доверенности (на сопровождение)")]
		public virtual string ApplicantProxyDateOfIssureEm { get; set; }

		/// <summary>
		/// Имя натариуса
		/// </summary>
		[Display(Description = "ФИО нотариуса (на сопровождение)")]
		public virtual string ApplicantNotaryNameEm { get; set; }

		/// <summary>
		/// Дата окончания действия доверенности
		/// </summary>
		[Display(Description = "Дата окончания срока действия доверенности (на сопровождение)")]
		public virtual string ApplicantProxyEndDateEm { get; set; }

		/// <summary>
		/// Номер доверенности
		/// </summary>
		[Display(Description = "Номер доверенности (на сопровождение)")]
		public virtual string ApplicantProxyNumberEm { get; set; }

		/// <summary>
		/// Дата выдачи доверенности
		/// </summary>
		[Display(Description = "Дата выдачи доверенности (на подачу)")]
		public virtual string ProxyDateOfIssureEm { get; set; }

		/// <summary>
		/// Имя натариуса
		/// </summary>
		[Display(Description = "ФИО нотариуса (на подачу)")]
		public virtual string NotaryNameEm { get; set; }

		/// <summary>
		/// Дата окончания действия доверенности
		/// </summary>
		[Display(Description = "Дата окончания срока действия доверенности (на подачу)")]
		public virtual string ProxyEndDateEm { get; set; }

		/// <summary>
		/// Номер доверенности
		/// </summary>
		[Display(Description = "Номер доверенности (на подачу)")]
		public virtual string ProxyNumberEm { get; set; }

		#endregion

		public bool CheckModel(string action = null, RequestViewModel model = null)
		{
			IsValid = true;

			if (Data == null)
			{
				return IsValid.Value;
			}

			if (string.IsNullOrWhiteSpace(Data.LastName))
			{
				IsValid = false;
				LastNameEm = RequaredField;
			}

			if (string.IsNullOrWhiteSpace(Data.FirstName))
			{
				IsValid = false;
				FirstNameEm = RequaredField;
			}

			if (string.IsNullOrWhiteSpace(Data.MiddleName) && !Data.HaveMiddleName)
			{
				IsValid = false;
				MiddleNameEm = RequaredField;
			}

			if (string.IsNullOrWhiteSpace(Data.Phone))
			{
				IsValid = false;
				PhoneEm = RequaredField;
			}
			if (!Data.DocumentTypeId.HasValue)
			{
				IsValid = false;
				DocumentTypeEm = RequaredField;
			}

            if (string.IsNullOrWhiteSpace(Data.Snils))
            {
                IsValid = false;
                SnilsEm = RequaredField;
            }

			if (string.IsNullOrWhiteSpace(Data.DocumentSeria) && (Data.DocumentTypeId != 23 && !DocumentTypeHelper.IsPassportOfForeignCountry(Data.DocumentTypeId ?? 0)))
			{
				IsValid = false;
				DocumentSeriaEm = RequaredField;
			}
			else
			{
				if (!DocumentTypeHelper.IsDocumentSeriaValid(Data.DocumentTypeId ?? 0, Data.DocumentSeria))
				{
					IsValid = false;
					DocumentSeriaEm = "Введены некорректные данные";
				}
			}
			if (string.IsNullOrWhiteSpace(Data.DocumentNumber))
			{
				IsValid = false;
				DocumentNumberEm = RequaredField;
			}
			else
			{
				if (!DocumentTypeHelper.IsDocumentNumberValid(Data.DocumentTypeId ?? 0, Data.DocumentNumber))
				{
					IsValid = false;
					DocumentSeriaEm = "Введены некорректные данные";
				}
			}
			if (string.IsNullOrWhiteSpace(Data.DocumentSubjectIssue))
			{
				IsValid = false;
				DocumentSubjectIssueEm = RequaredField;
			}
			if (!Data.DocumentDateOfIssue.HasValue)
			{
				IsValid = false;
				DocumentDateOfIssueEm = RequaredField;
			}

			//if (string.IsNullOrWhiteSpace(Data.NotaryName))
			//{
			//	IsValid = false;
			//	NotaryNameEm = RequaredField;
			//}

			//if (string.IsNullOrWhiteSpace(Data.ProxyNumber))
			//{
			//	IsValid = false;
			//	ProxyNumberEm = RequaredField;
			//}

			//if (!Data.ProxyDateOfIssure.HasValue)
			//{
			//	IsValid = false;
			//	ProxyDateOfIssureEm = RequaredField;
			//}

			//if (!Data.ProxyEndDate.HasValue)
			//{
			//	IsValid = false;
			//	ProxyEndDateEm = RequaredField;
			//}
			//else
			//{
			//	if (Data.ProxyEndDate.Value < (model.NullSafe(d=>d.Data.DateRequest) ?? DateTime.Now).Date)
			//	{
			//		IsValid = false;
			//		ProxyEndDateEm = "Доверенность истекла";
			//	}
			//}

			/*if (model != null && model.Data.SourceId == (long)SourceEnum.Mpgu)
			{
				if (string.IsNullOrWhiteSpace(Data.Email))
				{
					IsValid = false;
					EmailEm = RequaredField;
				}
			}*/

			if (Data.DateOfBirth.HasValue)
			{
				var age = StaticHelpers.GetAgeInYears(Data.DateOfBirth, DateTime.Today);
				if (age < 18)
				{
					DateOfBirthEm = "Доверенному лицу не может быть меньше 18 лет";
					IsValid = false;
				}
			}
			else if (model?.Data?.IsFirstCompany ?? false)
			{
				IsValid = false;
				DateOfBirthEm = RequaredField;
			}

			if (model?.Data?.RepresentInterestId == null)
			{
				IsValid = false;
				RepresentInterestEm = RequaredField;
			}

			return IsValid.Value;
		}

		public override string ToString()
		{
			return
				$"Сведения о представителе заявителя {Data?.LastName} {Data?.FirstName} {Data?.MiddleName}";
		}
	}
}
