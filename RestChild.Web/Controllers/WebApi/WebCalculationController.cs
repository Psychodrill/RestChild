using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;

namespace RestChild.Web.Controllers.WebApi
{
	public class WebCalculationController : WebGenericRestController<Calculation>
	{
		public bool SetCalculationPaid(Calculation calc, bool isPaid, string action = null)
		{
			if (calc == null)
			{
				throw new ArgumentNullException("calc");
			}

			var oldStateName = calc.NullSafe(c => c.State.Name);
			var newStateName = UnitOfWork.GetById<StateMachineState>(StateMachineStateEnum.Calculation.Paid);

			if (calc.StateId == StateMachineStateEnum.Calculation.Unpaid && isPaid)
			{

				calc.StateId = StateMachineStateEnum.Calculation.Paid;
				if (calc.History != null)
				{
					calc.History = new HistoryLink();
					UnitOfWork.Context.Entry(calc.History).State = EntityState.Added;
					ChangeCalculationHistory(calc, newStateName, oldStateName);
				}

				return true;
			}

			if (calc.StateId == StateMachineStateEnum.Calculation.Paid && !isPaid)
			{
				calc.StateId = StateMachineStateEnum.Calculation.Unpaid;
				if (calc.History != null)
				{
					calc.History = new HistoryLink();
					UnitOfWork.Context.Entry(calc.History).State = EntityState.Added;
					ChangeCalculationHistory(calc, newStateName, oldStateName);
				}

				return true;
			}

			return false;
		}

		private static void ChangeCalculationHistory(Calculation calc, StateMachineState newStateName, string oldStateName)
		{
			calc.History.Historys = calc.History.Historys ?? new List<History>();
			calc.History.Historys.Add(
				new History()
					{
						AccountId = Security.GetCurrentAccountId(),
						EventCode = "Переход в статус " + newStateName.FormatEx("-", false),
						DateChange = DateTime.Now,
						Commentary =
							string.Format(
								"Переход из статуса {0} в {1}",
								oldStateName.FormatEx("-", false),
								newStateName.NullSafe(n => n.Name).FormatEx("-", false))
					});
		}

		private List<Calculation> UpdateCaluclationForLimitOnOrganization(LimitOnOrganization limitOnOrganization, string action)
		{
			var calculations = new List<Calculation>();
			var lists = UnitOfWork.GetSet<ListOfChilds>().Where(o => o.LimitOnOrganizationId == limitOnOrganization.Id).ToList();

			foreach (var list in lists)
			{
				var newCalculations = UpdateCalculationForListOfChilds(list, action);
				calculations.AddRange(newCalculations);
			}

			if (calculations.Any())
			{
				UnitOfWork.SaveChanges();
			}

			return calculations;
		}

		private List<Calculation> UpdateCalculationForListOfChilds(ListOfChilds list, string action)
		{
			var calculations = new List<Calculation>();
			if (list.Childs != null)
			{
				var childsIds = list.Childs.Select(c => c.Id).ToList();
				var services = UnitOfWork.GetSet<AddonServicesLink>().Where(l => l.ChildId.HasValue && childsIds.Contains(l.ChildId.Value)).ToList();
				foreach (var child in list.Childs)
				{
					if (child.Calculations == null)
					{
						child.Calculations = new List<Calculation>();
					}

					if (!child.Calculations.Any())
					{
						var addonServicesLinks = services.Where(s => s.ChildId == child.Id).ToList();
						var calculation = new Calculation()
											{
												Children = new List<Child>() { child },
												AddonServicesLinks = addonServicesLinks,
												DateCreate = DateTime.Now,
												DateCalculation = DateTime.Now,
												Summa = addonServicesLinks.Select(l => l.Price).DefaultIfEmpty(0).Sum(),
												History = CreateHistoryLink(),
												AccountId = Security.GetCurrentAccountId(),
												StateId = StateMachineStateEnum.Calculation.Unpaid
											};
						child.Calculations.Add(calculation);
						calculations.Add(calculation);
					}
				}


			}

			if (list.Attendants != null)
			{
				foreach (var attendant in list.Attendants)
				{
					var attendantsIds = list.Attendants.Select(c => c.Id).ToList();
					var services = UnitOfWork.GetSet<AddonServicesLink>().Where(l => l.ApplicantId.HasValue && attendantsIds.Contains(l.ApplicantId.Value)).ToList();

					if (attendant.Calculations == null)
					{
						attendant.Calculations = new List<Calculation>();
					}

					if (!attendant.Calculations.Any())
					{
						var addonServicesLinks = services.Where(s => s.ApplicantId == attendant.Id).ToList();
						var calculation = new Calculation()
						{
							Attendants = new List<Applicant>() { attendant },
							AddonServicesLinks = addonServicesLinks,
							DateCreate = DateTime.Now,
							DateCalculation = DateTime.Now,
							Summa = addonServicesLinks.Select(l => l.Price).DefaultIfEmpty(0).Sum(),
							History = CreateHistoryLink(),
							AccountId = Security.GetCurrentAccountId(),
							StateId = StateMachineStateEnum.Calculation.Unpaid
						};
						attendant.Calculations.Add(calculation);
						calculations.Add(calculation);
					}
				}
			}
			return calculations;
		}

		private HistoryLink CreateHistoryLink()
		{
			return new HistoryLink()
						{
							Historys =
								new List<History>()
									{
										new History()
											{
												AccountId = Security.GetCurrentAccountId(),
												EventCode = "Первое сохранение",
												DateChange = DateTime.Now,
												Commentary = string.Empty
											}
									}
						};
		}
	}
}
