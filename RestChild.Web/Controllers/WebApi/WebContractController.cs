using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Extensions.Filter;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Extensions;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers.WebApi
{
	[Authorize]
	public class WebContractController : WebGenericRestController<Contract>
	{
		public StateController ApiStateController { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiStateController.SetUnitOfWorkInRefClass(unitOfWork);
		}

		[Route("api/suppliers")]
		public ICollection<Organization> GetSuppliers(string term)
		{

			var contracts = UnitOfWork.GetSet<Contract>().Where(c=>c.SupplierId.HasValue).Select(c=>c.SupplierId);
			var organizationQuery = UnitOfWork.GetSet<Organization>().Where(o=>contracts.Contains(o.Id));
			if (!string.IsNullOrEmpty(term))
			{
				term = term.ToLower();
				organizationQuery = organizationQuery.Where(o => o.Name.ToLower().Contains(term));
			}

			return organizationQuery.OrderBy(o=>o.Name.Length).ThenBy(o=>o.Name).Take(100).ToArray().Select(i => new Organization(i))
				.ToArray();
		}

		public CommonPagedList<Contract> Get(ContractFilterModel filter)
		{
			var pageSize = Settings.Default.TablePageSize;
			var pageNumber = filter != null ? filter.PageNumber : 1;
			var startRecord = (pageNumber - 1)*pageSize;
			var query = UnitOfWork.GetSet<Contract>().Where(c => c.StateId != null && c.StateId != StateMachineStateEnum.Deleted);
			if (filter != null)
			{
				if (!string.IsNullOrEmpty(filter.SignNumber))
				{
					query = query.Where(c => c.SignNumber.Contains(filter.SignNumber));
				}
				if (filter.SignDate.HasValue)
				{
					query = query.Where(c => DbFunctions.DiffDays(c.SignDate, filter.SignDate.Value) == 0);
				}
				if (filter.OrganizationId.HasValue)
				{
					query = query.Where(c => c.OrganizationId == filter.OrganizationId.Value);
				}
				if (filter.YearOfRestId.HasValue)
				{
					query = query.Where(c => c.YearOfRestId == filter.YearOfRestId.Value);
				}
				if (filter.StateId.HasValue)
				{
					query = query.Where(c => c.StateId == filter.StateId.Value);
				}
			}

			var totalCount = query.Count();
			var entity =
				query.OrderByDescending(t => t.SignDate)
					.Skip(startRecord)
					.Take(pageSize)
					.ToList()
					.Select(c => new Contract(c, 2))
					.ToList();
			return new CommonPagedList<Contract>(entity, pageNumber, pageSize, totalCount);
		}

		public List<string> GetErrorsOfChageStatus(long id, string actionCode)
		{
			var res = new List<string>();
			var contract = UnitOfWork.GetById<Contract>(id);
			if (actionCode == AccessRightEnum.Contract.Register)
			{
				if (!contract.OnRest && !contract.OnTransport)
				{
					res.Add("Необходимо указать признак \"Проживание\" или \"Транспорт\"");
				}

				if (contract.OnRest && contract.OnTransport)
				{
					res.Add("Нельзя создавать контракт одновременно на \"Проживание\" и \"Транспорт\"");
				}

			}

			return res;
		}

		public override Contract Post(Contract entity)
		{
			if (entity == null)
			{
				return null;
			}
			using (var transaction = UnitOfWork.GetTransactionScope())
			{
				entity.HistoryLink = UnitOfWork.AddEntity(new HistoryLink
															{
																Historys =
																	new List<History>
																		{
																			UnitOfWork.AddEntity(new History
																									{
																										AccountId = Security.GetCurrentAccountId(),
																										EventCode = "Создание контракта",
																										DateChange = DateTime.Now,
																										Commentary = string.Empty
																									})
																		}
															});

				var agreements = entity.AddonAgreements?.ToList() ?? new List<ContractAddonAgreement>();
				entity.AddonAgreements = null;

				var result = base.Post(entity);

				UpdateAgreements(result, agreements);

				transaction.Complete();
				return result;
			}
		}

		/// <summary>
		/// сохранение доп соглашения.
		/// </summary>
		private void UpdateAgreements(Contract persited, List<ContractAddonAgreement> agreements)
		{
			if (agreements == null)
			{
				return;
			}
			var agreementsIds = agreements.Select(a => a.Id).ToList();

			foreach (var agreement in persited.AddonAgreements.Where(a => !agreementsIds.Contains(a.Id) && a.LastUpdateTick != 0).ToList())
			{
				persited.AddonAgreements.Remove(agreement);
				UnitOfWork.Delete(agreement);
			}

			foreach (var agreement in agreements)
			{
				if (agreement.Id == 0)
				{
					agreement.ContractId = persited.Id;
					agreement.LastUpdateTick = DateTime.Now.Ticks;

					persited.AddonAgreements.Add(UnitOfWork.AddEntity(agreement, false));
				}
				else
				{
					var saved = persited.AddonAgreements.FirstOrDefault(a => a.Id == agreement.Id);
					agreement.LastUpdateTick = DateTime.Now.Ticks;
					saved?.CopyEntity(agreement);
				}
			}
		}

		public override Contract Put(long id, Contract entity)
		{
			if (entity == null)
			{
				return null;
			}

			using (var transaction = UnitOfWork.GetTransactionScope())
			{
				var agreements = entity.AddonAgreements?.ToList() ?? new List<ContractAddonAgreement>();
				entity.AddonAgreements = null;
				base.Put(id, entity);
				var persisted = UnitOfWork.GetById<Contract>(id);
				UpdateAgreements(persisted, agreements);

				UnitOfWork.SaveChanges();
				transaction.Complete();
				return persisted;
			}
		}

		public bool ChangeState(long id, string stateMachineActionString)
		{
			var result = false;
			var contract = UnitOfWork.GetById<Contract>(id);
			if (contract == null)
			{
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found"));
			}

			using (var transaction = UnitOfWork.GetTransactionScope())
			{
				if (stateMachineActionString == "Delete")
				{
					contract.HistoryLink = ApiStateController.WriteHistory(contract.HistoryLink, "Удаление контракта",string.Empty, StateMachineStateEnum.Deleted, contract.StateId);
					contract.HistoryLinkId = contract.HistoryLink?.Id;
					contract.StateId = StateMachineStateEnum.Deleted;
					UnitOfWork.SaveChanges();
				}
				else
				{
                    if (GetErrorsOfChageStatus(id, stateMachineActionString).Any())
					{
						return false;
					}

					var action = ApiStateController.GetAction(stateMachineActionString);
					if (action?.ToStateId != null)
					{
						contract.HistoryLink = ApiStateController.WriteHistory(contract.HistoryLink, "Изменение статуса контракта", $"Изменение статуса контракта с \"{contract.NullSafe(c => c.State.Name)}\" на \"{action.NullSafe(a => a.ToState.Name)}\"", action.ToStateId, contract.StateId);
						contract.HistoryLinkId = contract.HistoryLink?.Id;
						contract.StateId = action.ToStateId;
					}
				}

				UnitOfWork.SaveChanges();

				transaction.Complete();
				return result;
			}
		}

		public List<StateMachineState> GetStates()
		{
			return
				UnitOfWork.GetSet<StateMachineState>()
					.Where(s => s.StateMachineId == (long) StateMachineEnum.ContractState)
					.ToList()
					.Select(s => new StateMachineState(s))
					.ToList();
		}

		/// <summary>
		/// контракты.
		/// </summary>
		public List<Contract> GetByOrganization(long? yearOfRestId, long? organizationId, string signNumber, bool? onTransport, bool? onRest)
		{
			var query = UnitOfWork.GetSet<Contract>().Where(c=>c.StateId.HasValue && c.StateId != StateMachineStateEnum.Deleted && c.StateId == StateMachineStateEnum.Contract.Active) ;

			if (!Security.HasRight(AccessRightEnum.Contract.Manage) && !Security.HasRight(AccessRightEnum.Contract.View) && !Security.HasRight(AccessRightEnum.Contract.ViewCommercial))
			{
				var orgs = AccessRightEnum.Contract.View.GetSecurityOrganiztion();
				query = query.Where(c => orgs.Contains(c.SupplierId));
			}

			if (yearOfRestId.HasValue)
			{
				query = query.Where(c => c.YearOfRestId == yearOfRestId);
			}

			if (organizationId.HasValue)
			{
				query = query.Where(c => c.OrganizationId == organizationId);
			}

			if (!string.IsNullOrEmpty(signNumber))
			{
				var sn = signNumber.ToLower();
				query = query.Where(c => c.SignNumber.ToLower().Contains(sn) || c.Supplier.Name.ToLower().Contains(sn));
			}

			if (onTransport ?? false)
			{
				query = query.Where(c => c.OnTransport);
			}

			if (onRest ?? false)
			{
				query = query.Where(c => c.OnRest);
			}

			return
				query.OrderBy(c => c.SignNumber.Length).ThenBy(c=>c.SignNumber)
					.Take(Settings.Default.VocabularyResponseSize)
					.ToList()
					.Select(c => new Contract(c, 1))
					.ToList();
		}
	}
}
