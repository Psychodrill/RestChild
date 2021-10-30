using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RestChild.Comon;
using RestChild.Comon.Dto;
using RestChild.Comon.Dto.Commercial;
using RestChild.Comon.Enumeration;
using RestChild.Domain;
using RestChild.Extensions.Extensions;
using RestChild.Extensions.Filter;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Models.BaseRegistry;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers
{
    /// <summary>
    /// базовый регистр
    /// </summary>
    public class BaseRegistryController : BaseController
    {
        public const string SendRequestInBr = "9B109AD2-B3CE-4776-85C4-D7A8F76AA8E3";
        public const string GetPassportBySnilsInBr = "C1C55BEA-A6D9-4AE4-A5FD-03190E4A2B86";
        public const string GetSnilsByData = "0DC806DA-6C5C-4B56-B410-3F2E1765AAFB";
        public const string GetRelativesSmev = "49C8F097-361C-48F5-A70A-909C0B89F061";
        public const string GetRelatives = "4AFEE4AC-9251-45F7-ADC1-17F0AC41345A";
        /// <summary>
        ///     Проверка ЦПМПК
        /// </summary>
        public const string GetCPMPK = "EDC78633-AC2A-41E6-BD7E-5E6FFF695346";
        /// <summary>
        ///     Проверка адреса регистрации
        /// </summary>
        public const string GetRegistrationAddress = "38D6E2D8-CE98-4916-A267-D4469FDE6295";
        /// <summary>
        /// Проверка в ФГИС ФРИ инвалида
        /// </summary>
        public const string GetExtractFromFGISFRI = "3e8fe0c1-1501-477a-b492-5fa1037d1d97";

        public WebExchangeController ExchangeController { get; set; }

        public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
        {
            ExchangeController.SetUnitOfWorkInRefClass(unitOfWork);
            base.SetUnitOfWorkInRefClass(unitOfWork);
        }

        /// <summary>
        /// список для управления
        /// </summary>
        /// <returns></returns>
        public ActionResult ManageExchangeBaseRegistry()
        {
            if (!Security.HasRight(AccessRightEnum.ManageExchangeBaseRegistry))
            {
                return RedirectToAvalibleAction();
            }

            var notBRExchange = new long[] { (long)ExchangeBaseRegistryTypeEnum.CpmpkExchange, (long)ExchangeBaseRegistryTypeEnum.AisoLegalRepresentationCheck };

            var types = UnitOfWork.GetSet<ExchangeBaseRegistryType>().Where(e => !e.IsDeleted || notBRExchange.Contains(e.Id)).OrderBy(e => e.Name)
                .ToList();

            return View(types);
        }

        /// <summary>
        /// сохранение элементов
        /// </summary>
        public ActionResult SaveManage(IList<ExchangeBaseRegistryType> model)
        {
            if (!Security.HasRight(AccessRightEnum.ManageExchangeBaseRegistry) || model == null)
            {
                return RedirectToAvalibleAction();
            }

            var ids = model.Select(m => m.Id);

            var persisted = UnitOfWork.GetSet<ExchangeBaseRegistryType>().Where(t => ids.Contains(t.Id)).ToList();

            foreach (var item in persisted)
            {
                var data = model.FirstOrDefault(m => m.Id == item.Id);
                if (data != null)
                {
                    item.SendMessage = data.SendMessage;
                }
            }

            UnitOfWork.SaveChanges();

            return RedirectToAction("ManageExchangeBaseRegistry");
        }

        /// <summary>
        /// поиск по БР
        /// </summary>
        public ActionResult List(BaseRegistrySearch model)
        {
            SetUnitOfWorkInRefClass();

            if (!Security.HasRight(AccessRightEnum.RetryRequestInBaseRegistry))
            {
                return RedirectToAvalibleAction();
            }

            model = model ?? new BaseRegistrySearch();

            if (model.RequestBlock != null && !string.IsNullOrWhiteSpace(model.ActionString))
            {
                BaseResponse res = null;
                if (model.ActionString == SendRequestInBr)
                {
                    res = ExchangeController.AdditionallyCheckBenefit(model.RequestBlock);
                }

                if (model.ActionString == GetPassportBySnilsInBr)
                {
                    res = ExchangeController.AdditionallyGetPassport(model.RequestBlock);
                }

                if (model.ActionString == GetSnilsByData)
                {
                    res = ExchangeController.AdditionallyGetSnils(model.RequestBlock);
                }

                if (model.ActionString == GetRelativesSmev)
                {
                    res = ExchangeController.AdditionalRelationshipSmev(model.RequestBlock);
                }

                if (model.ActionString == GetRelatives)
                {
                    res = ExchangeController.AdditionallyCheckRelative(model.RequestBlock);
                }

                if (model.ActionString == GetCPMPK)
                {
                    res = ExchangeController.AdditionallyCheckCPMPK(model.RequestBlock);
                }

                if (model.ActionString == GetRegistrationAddress)
                {
                    res = ExchangeController.AdditionallyCheckRegistrationAddress(model.RequestBlock);
                }

                //if (model.ActionString == GetExtractFromFGISFRI)
                //{
                //    res = ExchangeController.ExtractFromFGISFRI(model.RequestBlock);
                //}

                if (res?.HasError ?? false)
                {
                    SetErrors(new[] {res.ErrorMessage});
                }
                else if (!string.IsNullOrWhiteSpace(res?.Name))
                {
                    SetMessages(new[] {res.Name ?? ""});
                }

                return RedirectToAction("List");
            }

            model.RequestBlock = null;
            model.ActionString = null;

            var pageSize = Settings.Default.TablePageSize;
            var pageNumber = model.PageNumber;
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            var startRecord = (pageNumber - 1) * pageSize;

            model.DocumentTypes =
                UnitOfWork.GetSet<DocumentType>().Where(d => d.ForChild && !d.ForForeign).OrderBy(d => d.Name).ToList();

            var query = UnitOfWork.GetSet<ExchangeBaseRegistry>().Where(e => e.IsAddonRequest);

            if (!string.IsNullOrWhiteSpace(model.RegistryNumber))
            {
                query = query.Where(q => q.ServiceNumber == model.RegistryNumber);
            }
            else if (!string.IsNullOrWhiteSpace(model.LastName))
            {
                var ssString =
                    $"{model.LastName?.ToLower().Trim()}|{model.FirstName?.ToLower().Trim()}|{model.MiddleName?.ToLower().Trim()}";

                query = query.Where(q => q.SearchField == ssString);
            }

            var totalCount = query.Count();
            var entity =
                query.OrderByDescending(t => t.Id)
                    .Skip(startRecord)
                    .Take(pageSize)
                    .ToList().Select(b => b.Parse()).ToList();

            model.Result = new CommonPagedList<BaseRegistryCheckResult>(entity, pageNumber, pageSize, totalCount);

            return View(model);
        }
    }
}
