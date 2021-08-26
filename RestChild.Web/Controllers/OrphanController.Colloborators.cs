using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Extensions;
using RestChild.Web.Models.Orphans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RestChild.Web.Controllers
{
    public partial class OrphanController
    {
        /// <summary>
        ///     Сотрудник -> поиск
        /// </summary>
        [Route("Orphanage/Collaborators/Search/")]
        public ActionResult OrphanageCollaboratorsSearch(OrphanageCollaboratorsFilterModel filter)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.Orphans.Main, filter.OrphanageId))
            {
                return RedirectToAvalibleAction();
            }

            filter.Collaborators = ApiController.GetOrphanageCollaborators(filter);

            return PartialView("Partials/PersonalList", filter);
        }

        /// <summary>
        ///     Сотрудник -> новый
        /// </summary>
        [Route("Orphanage/Collaborators/New/{orphanageId}")]
        public ActionResult OrphanageCollaboratorNew(long orphanageId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.Orphans.Main, orphanageId))
            {
                return RedirectToAvalibleAction();
            }

            ViewbagCollaboratorsSet(orphanageId);
            return View("OrphanageCollaboratorEdit", new OrganisatorCollaboratorModel(new OrganisatorCollaborator
            {
                Organisaton = UnitOfWork.GetById<Organization>(orphanageId),
                OrganisatonId = orphanageId,
                Applicant = new Applicant
                {
                    Address = new Address()
                }
            }));
        }

        /// <summary>
        ///     Сотрудник -> редактирование
        /// </summary>
        [Route("Orphanage/Collaborators/Edit/{collaboratorId}")]
        public ActionResult OrphanageCollaboratorEdit(long collaboratorId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRightForSomeOrganization(AccessRightEnum.Orphans.Main))
            {
                return RedirectToAvalibleAction();
            }

            var res = UnitOfWork.GetById<OrganisatorCollaborator>(collaboratorId);
            ViewbagCollaboratorsSet(res.OrganisatonId);

            if(res.Applicant?.Address?.BtiDistrictId.HasValue ?? false)
            {
                res.Applicant.Address.Region = res.Applicant.Address.BtiDistrict.Name;
            }

            if (res.Applicant?.Address?.BtiRegionId.HasValue ?? false)
            {
                res.Applicant.Address.District = res.Applicant.Address.BtiRegion.Name;
            }


            return View("OrphanageCollaboratorEdit", new OrganisatorCollaboratorModel(res));
        }

        /// <summary>
        ///     Сотрудник -> сохранение
        /// </summary>
        [Route("Orphanage/Collaborators/Save/")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult OrphanageCollaboratorSave(OrganisatorCollaboratorModel model)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.Orphans.Main, model.Data.OrganisatonId))
            {
                return RedirectToAvalibleAction();
            }

            using (var tran = UnitOfWork.GetTransactionScope())
            {
                if (!string.IsNullOrWhiteSpace(model.Data.Applicant?.Address?.Region))
                {
                    var reg = UnitOfWork.GetSet<BtiDistrict>().Where(ss => ss.Name == model.Data.Applicant.Address.Region)
                        .Select(ss => ss.Id).FirstOrDefault();
                    if (reg > 0)
                    {
                        model.Data.Applicant.Address.BtiDistrictId = reg;
                    }
                }

                if (!string.IsNullOrWhiteSpace(model.Data.Applicant?.Address?.District))
                {
                    var dis = UnitOfWork.GetSet<BtiRegion>().Where(ss => ss.Name == model.Data.Applicant.Address.District)
                        .Select(ss => ss.Id).FirstOrDefault();
                    if (dis > 0)
                    {
                        model.Data.Applicant.Address.BtiRegionId = dis;
                    }
                }

                model.Data.Applicant.HaveMiddleName = !model.NoMiddleName;

                CollaboratorIsFilledSet(model.Data);

                if (model.Data.Id < 1)
                {
                    model.Data.HistoryLink = ApiController.WriteHistory(model.Data.HistoryLink, "Создание сотрудника учреждения социальной защиты", string.Empty);
                    model.Data.HistoryLinkId = model.Data.HistoryLink.Id;
                    var orgId = model.Data.OrganisatonId;
                    model.Data.Organisaton = null;
                    if (!orgId.HasValue || orgId.Value < 1)
                    {
                        if (model.Data.OrganisatonAddressId.HasValue && model.Data.OrganisatonAddressId.Value > 0)
                        {
                            model.Data.OrganisatonId = UnitOfWork.GetById<OrphanageAddress>(model.Data.OrganisatonAddressId)?.OrganisationId;
                        }
                    }
                    else
                    {
                        model.Data.OrganisatonId = orgId;
                    }

                    UnitOfWork.AddEntity(model.Data);

                    WriteHistory(model.Data.Id, "создал сотрудника учреждения социальной защиты");

                    UnitOfWork.SaveChanges();

                    tran.Complete();
                }
                else
                {
                    if (UnitOfWork.GetLastUpdateTickById<OrganisatorCollaborator>(model.Data.Id) != model.Data.LastUpdateTick)
                    {
                        SetRedicted();
                        return RedirectToAction(nameof(OrphanageCollaboratorEdit), new { collaboratorId = model.Data.Id });
                    }

                    var data = UnitOfWork.GetById<OrganisatorCollaborator>(model.Data.Id);

                    model.Data.HistoryLinkId = data.HistoryLinkId;

                    var dif = GetDiff(model.Data, data);

                    if (!string.IsNullOrWhiteSpace(dif))
                    {
                        data.HistoryLink = ApiController.WriteHistory(data.HistoryLink, "Обновление сведений о сотруднике учреждения социальной защиты", dif);
                        data.HistoryLinkId = data.HistoryLink?.Id;

                        data.CopyEntity(model.Data);
                        data.Applicant.CopyEntity(model.Data.Applicant);
                        data.Applicant.Address.CopyEntity(model.Data.Applicant.Address);

                        WriteHistory(model.Data.Id, "изменил данные сотрудника учреждения социальной защиты");

                        UnitOfWork.SaveChanges();

                        tran.Complete();
                    }
                }
            }

            return RedirectToAction(nameof(OrphanageCollaboratorEdit), new { collaboratorId = model.Data.Id });
        }

        /// <summary>
        ///     Разница в сотруднике приюта
        /// </summary>
        private string GetDiff(OrganisatorCollaborator entity, OrganisatorCollaborator persisted)
        {
            var sb = new StringBuilder();

            if (persisted.Applicant?.FirstName != entity.Applicant?.FirstName)
            {
                sb.AppendLine($"<li>Изменено имя сотрудника, старое значение:'{persisted.Applicant?.FirstName.FormatEx(string.Empty)}', новое значение:'{entity.Applicant?.FirstName.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Applicant?.LastName != entity.Applicant?.LastName)
            {
                sb.AppendLine($"<li>Изменена фамилия сотрудника, старое значение:'{persisted.Applicant?.LastName.FormatEx(string.Empty)}', новое значение:'{entity.Applicant?.LastName.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Applicant?.MiddleName != entity.Applicant?.MiddleName || persisted.Applicant?.HaveMiddleName != entity.Applicant?.HaveMiddleName)
            {
                sb.AppendLine($"<li>Изменено отчество сотрудника, старое значение:'{persisted.Applicant?.MiddleName.FormatEx(string.Empty)}', новое значение:'{entity.Applicant?.MiddleName.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Applicant?.Phone != entity.Applicant?.Phone)
            {
                sb.AppendLine($"<li>Изменен телефон сотрудника, старое значение:'{persisted.Applicant?.Phone.FormatEx(string.Empty)}', новое значение:'{entity.Applicant?.Phone.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Applicant?.Snils != entity.Applicant?.Snils)
            {
                sb.AppendLine($"<li>Изменен СНИЛС сотрудника, старое значение:'{persisted.Applicant?.Snils.FormatEx(string.Empty)}', новое значение:'{entity.Applicant?.Snils.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Applicant?.DateOfBirth != entity.Applicant?.DateOfBirth)
            {
                sb.AppendLine($"<li>Изменена дата рождения сотрудника, старое значение:'{persisted.Applicant?.DateOfBirth.FormatEx(string.Empty)}', новое значение:'{entity.Applicant?.DateOfBirth.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Applicant?.Male != entity.Applicant?.Male)
            {
                sb.AppendLine($"<li>Изменен пол сотрудника, старое значение:'{(persisted.Applicant?.Male ?? false ? "Мужской" : "Женский")}', новое значение:'{(entity.Applicant?.Male ?? false ? "Мужской" : "Женский")}'</li>");
            }

            if (persisted.Applicant?.PlaceOfBirth != entity.Applicant?.PlaceOfBirth)
            {
                sb.AppendLine($"<li>Изменено место рождения сотрудника, старое значение:'{persisted.Applicant?.PlaceOfBirth.FormatEx(string.Empty)}', новое значение:'{entity.Applicant?.PlaceOfBirth.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Applicant?.Email != entity.Applicant?.Email)
            {
                sb.AppendLine($"<li>Изменен E-Mail сотрудника в оздоровительной организации, старое значение:'{persisted.Applicant?.Email.FormatEx(string.Empty)}', новое значение:'{entity.Applicant?.Email.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.PositionId != entity.PositionId)
            {
                var newValue = UnitOfWork.GetSet<OrganizationCollaboratorPostType>().Where(ss => ss.Id == entity.PositionId).Select(ss => ss.Name).FirstOrDefault();
                sb.AppendLine($"<li>Изменен тип сотрудника, старое значение:'{persisted.Position?.Name.FormatEx(string.Empty)}', новое значение:'{newValue.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.AditionalPhone != entity.AditionalPhone)
            {
                sb.AppendLine($"<li>Изменен телефон (моб) сотрудника, старое значение:'{persisted.AditionalPhone.FormatEx(string.Empty)}', новое значение:'{entity.AditionalPhone.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.OrganisationPosition != entity.OrganisationPosition)
            {
                sb.AppendLine($"<li>Изменена должность сотрудника в учреждении социальной защиты, старое значение:'{persisted.OrganisationPosition?.FormatEx(string.Empty)}', новое значение:'{entity.OrganisationPosition?.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.WellnessOrganisationPositionId != entity.WellnessOrganisationPositionId)
            {
                var newValue = UnitOfWork.GetSet<TypeOfLinkPeople>().Where(ss => ss.Id == entity.WellnessOrganisationPositionId).Select(ss => ss.Name).FirstOrDefault();
                sb.AppendLine($"<li>Изменена должность сотрудника в оздоровительной организации, старое значение:'{persisted.WellnessOrganisationPosition?.Name.FormatEx(string.Empty)}', новое значение:'{newValue.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Applicant.CountryId != entity.Applicant.CountryId)
            {
                var newValue = UnitOfWork.GetSet<Country>().Where(ss => ss.Id == entity.Applicant.CountryId).Select(ss => ss.Name).FirstOrDefault();
                sb.AppendLine($"<li>Изменено гражданство сотрудника, старое значение:'{persisted.Applicant?.Country?.Name.FormatEx(string.Empty)}', новое значение:'{newValue.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.OrganisatonAddressId != entity.OrganisatonAddressId)
            {
                var newValue = UnitOfWork.GetSet<OrphanageAddress>().Where(ss => ss.Id == entity.OrganisatonAddressId).Select(ss => ss.Address.Name).FirstOrDefault();
                sb.AppendLine($"<li>Изменен адрес учреждения сотрудника, старое значение:'{persisted.OrganisatonAddress?.Address?.Name.FormatEx(string.Empty)}', новое значение:'{newValue.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Applicant?.AddressId != entity.Applicant?.AddressId ||
                persisted.Applicant?.Address?.FiasId != entity.Applicant?.Address?.FiasId ||
                persisted.Applicant?.Address?.Name != entity.Applicant?.Address?.Name ||
                persisted.Applicant?.Address?.Appartment != entity.Applicant?.Address?.Appartment)
            {
                sb.AppendLine($"<li>Изменен адрес регистрации сотрудника, старое значение:'{persisted.Applicant?.Address?.Name.FormatEx(string.Empty)}, кв.{persisted.Applicant?.Address?.Appartment.FormatEx(string.Empty)}', новое значение:'{entity.Applicant?.Address?.Name.FormatEx(string.Empty)}, кв.{entity.Applicant?.Address?.Appartment.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Applicant?.DocumentTypeId != entity.Applicant?.DocumentTypeId ||
                persisted.Applicant?.DocumentSeria != entity.Applicant?.DocumentSeria ||
                persisted.Applicant?.DocumentNumber != entity.Applicant?.DocumentNumber ||
                persisted.Applicant?.DocumentDateOfIssue != entity.Applicant?.DocumentDateOfIssue ||
                persisted.Applicant?.DocumentSubjectIssue != entity.Applicant?.DocumentSubjectIssue)
            {
                sb.AppendLine($"<li>Изменен документ удостоверяющего личность сотрудника, старое значение:'{persisted.Applicant?.DocumentType?.Name.FormatEx(string.Empty)}, серия: {persisted.Applicant?.DocumentSeria.FormatEx(string.Empty)}, номер: {persisted.Applicant?.DocumentNumber.FormatEx(string.Empty)}, дата выдачи: {persisted.Applicant?.DocumentDateOfIssue.FormatEx("dd.MM.yyyy")}, выдан: {persisted.Applicant?.DocumentSubjectIssue.FormatEx(string.Empty)}', новое значение:'{entity.Applicant?.DocumentType?.Name.FormatEx(string.Empty)}, серия: {entity.Applicant?.DocumentSeria.FormatEx(string.Empty)}, номер: {entity.Applicant?.DocumentNumber.FormatEx(string.Empty)}, дата выдачи: {entity.Applicant?.DocumentDateOfIssue.FormatEx("dd.MM.yyyy")}, выдан: {entity.Applicant?.DocumentSubjectIssue.FormatEx(string.Empty)}'</li>");
            }

            if(persisted.Filled != entity.Filled)
            {
                sb.AppendLine($"<li>Изменён признак достаточной заполненности данных о сотруднике, старое значение:'{persisted.Filled.FormatEx(string.Empty)}', новое значение:'{entity.Filled.FormatEx(string.Empty)}'</li>");
            }

            var res = sb.ToString();
            if (string.IsNullOrWhiteSpace(res))
            {
                return null;
            }

            return $"<ul>{res}</ul>";
        }

        /// <summary>
        ///     Справочники для сотрудника приюта
        /// </summary>
        private void ViewbagCollaboratorsSet(long? orphanageId = null)
        {
            ViewBag.ColloboratorTypes =
                UnitOfWork.GetSet<OrganizationCollaboratorPostType>().Where(ss => ss.IsActive).ToList();
            ViewBag.WellnesOrganisatioColloboratorTypes =
                UnitOfWork.GetSet<TypeOfLinkPeople>().ToList();
            ViewBag.OrphanageAdresses =
                UnitOfWork.GetSet<OrphanageAddress>()
                    .Where(ss => ss.Organisation.Id == orphanageId && ss.Organisation.Orphanage == true).ToList();
            ViewBag.DocumentTypes =
                UnitOfWork.GetSet<DocumentType>().Where(ss => ss.ForApplicant).ToList();
            ViewBag.Countries =
                UnitOfWork.GetSet<Country>().ToList();
        }

        /// <summary>
        ///     Установка признака заполненности полей
        /// </summary>
        private static void CollaboratorIsFilledSet(OrganisatorCollaborator col)
        {
            col.Filled = !string.IsNullOrWhiteSpace(col.Applicant?.LastName)
                         && !string.IsNullOrWhiteSpace(col.Applicant?.FirstName)
                         && (!string.IsNullOrWhiteSpace(col.Applicant?.MiddleName) || (!col.Applicant?.HaveMiddleName ?? false))
                         && (col.Applicant?.DateOfBirth.HasValue ?? false)
                         && (col.Applicant?.Male.HasValue ?? false)
                         && !string.IsNullOrWhiteSpace(col.Applicant?.Snils)
                         && col.PositionId.HasValue
                         && !string.IsNullOrWhiteSpace(col.Applicant?.Phone)
                         && !string.IsNullOrWhiteSpace(col.Applicant?.PlaceOfBirth)
                         && (col.Applicant?.CountryId.HasValue ?? false)
                         && !string.IsNullOrWhiteSpace(col.OrganisationPosition)
                         && col.WellnessOrganisationPositionId.HasValue
                         && col.OrganisatonAddressId.HasValue
                         && (col.Applicant?.DocumentTypeId.HasValue ?? false)
                         && (Common.DocumentTypeHelper.IsPassportOfForeignCountry(col.Applicant.DocumentTypeId.Value) || !string.IsNullOrWhiteSpace(col.Applicant?.DocumentSeria)) 
                         && !string.IsNullOrWhiteSpace(col.Applicant?.DocumentNumber)
                         && (col.Applicant?.DocumentDateOfIssue.HasValue ?? false)
                         && !string.IsNullOrWhiteSpace(col.Applicant?.DocumentSubjectIssue)
                         && !string.IsNullOrWhiteSpace(col.Applicant?.Address?.FiasId)
                         && !string.IsNullOrWhiteSpace(col.Applicant?.Address?.Appartment);
        }
    }
}
