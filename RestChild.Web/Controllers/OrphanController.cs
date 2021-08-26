using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Extensions;
using RestChild.Web.Models;
using RestChild.Web.Models.Orphans;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers
{
    /// <summary>
    ///     Блок сирот
    /// </summary>
    [Authorize]
    public partial class OrphanController : BaseController
    {
        public WebOrphanController ApiController { get; set; }

        public WebExchangeController ExchangeController { get; set; }

        public StateController ApiStateController { get; set; }

        public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
        {
            base.SetUnitOfWorkInRefClass(unitOfWork);
            ApiController.SetUnitOfWorkInRefClass(unitOfWork);
            ApiStateController.SetUnitOfWorkInRefClass(unitOfWork);
            ExchangeController.SetUnitOfWorkInRefClass(unitOfWork);
        }

        /// <summary>
        ///     Разница в приютах
        /// </summary>
        private string GetDiff(Organization entity, Organization persisted)
        {
            var sb = new StringBuilder();

            if (persisted.Name != entity.Name)
            {
                sb.AppendLine(
                    $"<li>Изменено наименование учреждения, старое значение:'{persisted.Name.FormatEx(string.Empty)}', новое значение:'{entity.Name.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.ShortName != entity.ShortName)
            {
                sb.AppendLine(
                    $"<li>Изменено краткое наименование учреждения, старое значение:'{persisted.ShortName.FormatEx(string.Empty)}', новое значение:'{entity.ShortName.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Phone != entity.Phone)
            {
                sb.AppendLine(
                    $"<li>Изменен телефон, старое значение:'{persisted.Phone.FormatEx(string.Empty)}', новое значение:'{entity.Phone.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Email != entity.Email)
            {
                sb.AppendLine(
                    $"<li>Изменен E-mail, старое значение:'{persisted.Email.FormatEx(string.Empty)}', новое значение:'{entity.Email.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.HeadPerson != entity.HeadPerson)
            {
                sb.AppendLine(
                    $"<li>Изменен Генеральный директор, старое значение:'{persisted.HeadPerson.FormatEx(string.Empty)}', новое значение:'{entity.HeadPerson.FormatEx(string.Empty)}'</li>");
            }


            foreach (var a in persisted.OrphanageOrganizationAddresses?.Where(ss =>
                                  !(entity.OrphanageOrganizationAddresses?.Select(sx => sx.Id) ?? new List<long>(0))
                                      .Contains(ss.Id)) ?? new List<OrphanageAddress>(0))
            {
                sb.AppendLine($"<li>Адрес организации '{a.Address.Name.FormatEx(string.Empty)}' удален</li>");
            }

            foreach (var a in entity.OrphanageOrganizationAddresses?.Where(sx => sx.Id < 1) ??
                              new List<OrphanageAddress>(0))
            {
                sb.AppendLine($"<li>Адрес организации '{a.Address.Name.FormatEx(string.Empty)}' добавлен</li>");
            }

            foreach (var a in persisted.OrphanageOrganizationAddresses?.Where(ss =>
                                  (entity.OrphanageOrganizationAddresses?.Select(sx => sx.Id) ?? new List<long>(0))
                                  .Contains(ss.Id)) ?? new List<OrphanageAddress>(0))
            {
                var newAddressName = entity.OrphanageOrganizationAddresses.First(ss => ss.Id == a.Id).Address.Name;
                if (a.Address.Name != newAddressName)
                {
                    sb.AppendLine(
                        $"<li>Адрес организации изменён, старое значение: '{a.Address.Name.FormatEx(string.Empty)}', новое значение:'{newAddressName.FormatEx(string.Empty)}'</li>");
                }
            }

            var res = sb.ToString();
            if (string.IsNullOrWhiteSpace(res))
            {
                return null;
            }

            return $"<ul>{res}</ul>";
        }

        /// <summary>
        ///     Реестр учреждений социальной защиты -> поиск
        /// </summary>
        /// <param name="filter">Фильтр</param>
        [Route("Orphanage/Search")]
        public ActionResult OrphanageSearch(OrphanageFilterModel filter)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRightForSomeOrganization(AccessRightEnum.Orphans.Main))
            {
                return RedirectToAvalibleAction();
            }

            filter.Result = ApiController.GetOrphanage(filter);


            return View(filter);
        }

        /// <summary>
        ///     Реестр учреждений социальной защиты -> редактирование
        /// </summary>
        /// <param name="organizationId">Идентификатор организации</param>
        [Route("Orphanage/Edit/{organizationId}")]
        [HttpGet]
        public ActionResult OrphanageEdit(long? organizationId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.Orphans.Main, organizationId))
            {
                return RedirectToAvalibleAction();
            }

            //новый детский дом
            if (!organizationId.HasValue)
            {
                return View("OrphanageEdit", new OrphanageModel(new Organization()));
            }

            var org = UnitOfWork.GetSet<Organization>().FirstOrDefault(ss => ss.Id == organizationId && ss.Orphanage == true);
            if (org == null)
            {
                RedirectToAvalibleAction();
            }

            var result = new OrphanageModel(org);

            var errors = new List<string>();

            var errMsg = TempData[errorMessage] as string;
            if (!string.IsNullOrWhiteSpace(errMsg))
            {
                errors.AddRange(errMsg.Split(';').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray());
            }

            if (errors.Any())
            {
                result.ErrorMessage = $"<ul><li>{string.Join("</li><li>", errors)}</li></ul>";
                result.IsValid = false;
            }

            result.Groups = new OrphanageGroupsFilterModel(organizationId.Value);
            ApiController.FillGroups(result.Groups, Settings.Default.TablePageSize);

            result.Lists = new OrphanagePupilGroupListFilterModel(organizationId.Value);
            ApiController.FillPupilGroupList(result.Lists, Settings.Default.TablePageSize);

            return View(result);
        }

        /// <summary>
        ///     Реестр учреждений социальной защиты -> создание новой
        /// </summary>
        [Route("Orphanage/New")]
        [HttpGet]
        public ActionResult OrphanageNew()
        {
            return OrphanageEdit(null);
        }

        /// <summary>
        ///     Реестр учреждений социальной защиты -> сохранение
        /// </summary>
        [Route("Orphanage/Save")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrphanageSave(OrphanageModel model)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.Orphans.Main, model.Data?.Id))
            {
                return RedirectToAvalibleAction();
            }

            SetUnitOfWorkInRefClass(UnitOfWork);

            ValidateModel(model);

            if(!ModelState.IsValid)
            {
                return View("OrphanageEdit", model);
            }

            var entity = model.BuildData();
            using (var tran = UnitOfWork.GetTransactionScope())
            {
                if (entity.Id < 1)
                {
                    entity.HistoryLink = ApiController.WriteHistory(entity.HistoryLink, "Создание учреждения социальной защиты", string.Empty);
                    entity.HistoryLinkId = entity.HistoryLink.Id;

                    UnitOfWork.AddEntity(entity);

                    WriteHistory(entity.Id, "создал учреждение социальной защиты", entity.Name);

                    UnitOfWork.SaveChanges();

                    tran.Complete();

                    return RedirectToAction(nameof(OrphanageEdit), new {organizationId = entity.Id});
                }

                if (UnitOfWork.GetLastUpdateTickById<Organization>(entity.Id) != entity.LastUpdateTick)
                {
                    SetRedicted();
                    return RedirectToAction(nameof(OrphanageEdit), new {organizationId = entity.Id});
                }

                var data = UnitOfWork.GetById<Organization>(entity.Id);

                entity.HistoryLinkId = data.HistoryLinkId;

                data.HistoryLink = ApiController.WriteHistory(data.HistoryLink, "Обновление сведений о учреждении социальной защиты", GetDiff(entity, data));
                data.HistoryLinkId = data.HistoryLink?.Id;

                data.Email = entity.Email;
                data.Email = entity.Email;
                data.Phone = entity.Phone;
                data.HeadPerson = entity.HeadPerson;

                //обновление адресов
                var resMessage = OrphanageSaveAddress(model, entity, data);

                if(!string.IsNullOrWhiteSpace(resMessage))
                {
                    TempData[errorMessage] = resMessage;
                    return RedirectToAction(nameof(OrphanageEdit), new { organizationId = entity.Id });
                }

                WriteHistory(entity.Id, "изменил учреждение социальной защиты", entity.Name);
                UnitOfWork.SaveChanges();
                tran.Complete();
            }

            return RedirectToAction(nameof(OrphanageEdit), new {organizationId = entity.Id});
        }

        /// <summary>
        /// сохранение адреса
        /// </summary>
        private string OrphanageSaveAddress(OrphanageModel model, Organization entity, Organization data)
        {
            foreach (var a in data.OrphanageOrganizationAddresses?.Where(ss =>
                                  !(entity.OrphanageOrganizationAddresses?.Select(sx => sx.Id) ??
                                    new List<long>(0)).Contains(ss.Id)).ToList() ??
                              new List<OrphanageAddress>(0))
            {
                var addressId = a.AddressId;

                if(UnitOfWork.GetSet<Pupil>().Any(p => p.OrphanageAddressId == a.Id))
                {
                    return "Удаление адреса невозможно. К данному адресу привязаны воспитанники;";
                }

                UnitOfWork.SaveChanges();
                UnitOfWork.Delete(UnitOfWork.GetById<OrphanageAddress>(a.Id));
                UnitOfWork.Delete(UnitOfWork.GetById<Address>(addressId));
                UnitOfWork.SaveChanges();
            }

            foreach (var a in entity.OrphanageOrganizationAddresses?.Where(sx => sx.Id < 1) ??
                              new List<OrphanageAddress>(0))
            {
                var address = new Address
                {
                    Name = a.Address.Name,
                    FiasId = a.Address.FiasId,
                    Region = a.Address.Region,
                    District = a.Address.District
                };
                UnitOfWork.AddEntity(address);
                var oAddress = new OrphanageAddress
                {
                    Address = address,
                    OrganisationId = entity.Id,
                    FencedArea = a.FencedArea,
                    LargeParking = a.LargeParking
                };
                UnitOfWork.AddEntity(oAddress);
                UnitOfWork.SaveChanges();
            }

            foreach (var a in data.OrphanageOrganizationAddresses?.Where(ss =>
                (entity.OrphanageOrganizationAddresses?.Select(sx => sx.Id) ??
                 new List<long>(0)).Contains(ss.Id)) ?? new List<OrphanageAddress>(0))
            {
                var address = UnitOfWork.GetById<Address>(a.AddressId);
                var oAddress = UnitOfWork.GetById<OrphanageAddress>(a.Id);

                address.FiasId = model.OrphanageAddress[a.Id.ToString()].Address.FiasId;
                address.Name = model.OrphanageAddress[a.Id.ToString()].Address.Name;
                address.Region = model.OrphanageAddress[a.Id.ToString()].Address.Region;
                address.District = model.OrphanageAddress[a.Id.ToString()].Address.District;

                oAddress.FencedArea = model.OrphanageAddress[a.Id.ToString()].FencedArea;
                oAddress.LargeParking = model.OrphanageAddress[a.Id.ToString()].LargeParking;

                UnitOfWork.SaveChanges();
            }

            data.Address = data.OrphanageOrganizationAddresses?.Select(ss => ss.Address.Name).FirstOrDefault();

            return null;
        }

        /// <summary>
        ///     запись в историю
        /// </summary>
        private void WriteHistory(long id, string actionName, string message = null)
        {
            var user = Security.GetCurrentAccount();

            var brouser = System.Web.HttpContext.Current?.Request?.UserAgent;

            UnitOfWork.AddEntity(new SecurityJournal
            {
                SecurityJournalTypeId = (long)SecurityJournalEventType.Processes,
                EventName = "Изменение в блоке учреждений социальной защиты",
                UserName = $"{user?.Name} ({user?.Id})",
                DateEvent = DateTime.Now,
                Description = $"Пользователь {user?.Name} ({user?.Id}) {actionName} ({id}){(!string.IsNullOrWhiteSpace(message) ? $": {message}" : string.Empty)}",
                Brouser = brouser
            });
        }

        private void ValidateModel(OrphanageModel model)
        {
            ModelState.Clear();
            if(string.IsNullOrWhiteSpace(model?.Data?.Name))
            {
                ModelState.AddModelError("Data.Name", "Название учереждения должно быть заполнено");
            }
        }


    }
}
