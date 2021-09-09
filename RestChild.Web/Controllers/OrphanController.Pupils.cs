using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Configuration;
using System.Web.Mvc;
using RestChild.Booking.Logic.Extensions;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Common;
using RestChild.Web.Extensions;
using RestChild.Web.Models.Orphans;

namespace RestChild.Web.Controllers
{
    public partial class OrphanController
    {
        /// <summary>
        ///     Воспитанники -> поиск
        /// </summary>
        [Route("Orphanage/Pupils/Search/")]
        public ActionResult OrphanagePupilsSearch(OrphanagePupilsFilterModel filter)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.Orphans.Main, filter.OrphanageId))
            {
                return RedirectToAvalibleAction();
            }

            filter.Results = ApiController.GetPupils(filter);

            return PartialView("Partials/PupilList", filter);
        }

        /// <summary>
        ///     Воспитанник -> новый
        /// </summary>
        [Route("Orphanage/Pupils/New/{orphanageId}")]
        public ActionResult OrphanagePupilNew(long orphanageId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.Orphans.Main, orphanageId))
            {
                return RedirectToAvalibleAction();
            }

            ViewbagPupilsSet(orphanageId);
            return View("OrphanagePupilEdit", new OrphanagePupilModel(
                new Pupil
                {
                    Child = new Child
                    {
                        Address = new Address()
                    }
                })
            {
                OrphanageName = UnitOfWork.GetById<Organization>(orphanageId)?.Name,
                OrphanageId = orphanageId,
                NoMiddleName = false
            });
        }

        /// <summary>
        ///     Воспитанник -> редактирование
        /// </summary>
        [Route("Orphanage/Pupils/Edit/{pupilId}")]
        public ActionResult OrphanagePupilEdit(long pupilId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            var res = UnitOfWork.GetById<Pupil>(pupilId);

            if (!Security.HasRight(AccessRightEnum.Orphans.Main, res.OrphanageAddress.OrganisationId))
            {
                return RedirectToAvalibleAction();
            }

            ViewbagPupilsSet(res.OrphanageAddress.OrganisationId);
            return View("OrphanagePupilEdit", new OrphanagePupilModel(res));
        }

        /// <summary>
        ///     Воспитанник -> сохранение
        /// </summary>
        [Route("Orphanage/Pupils/Save/")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult OrphanagePupilSave(OrphanagePupilModel model)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.Orphans.Main, model.OrphanageId))
            {
                return RedirectToAvalibleAction();
            }

            using (var tran = UnitOfWork.GetTransactionScope())
            {
                if (!string.IsNullOrWhiteSpace(model.Data.Child?.Address?.Region))
                {
                    var reg = UnitOfWork.GetSet<BtiDistrict>().Where(ss => ss.Name == model.Data.Child.Address.Region)
                        .Select(ss => ss.Id).FirstOrDefault();
                    if (reg > 0)
                    {
                        model.Data.Child.Address.BtiDistrictId = reg;
                    }
                }

                if (!string.IsNullOrWhiteSpace(model.Data.Child?.Address?.District))
                {
                    var dis = UnitOfWork.GetSet<BtiRegion>().Where(ss => ss.Name == model.Data.Child.Address.District)
                        .Select(ss => ss.Id).FirstOrDefault();
                    if (dis > 0)
                    {
                        model.Data.Child.Address.BtiRegionId = dis;
                    }
                }

                model.Data.Child.HaveMiddleName = !model.NoMiddleName;
                PupilFilledSet(model.Data);

                if (model.Data.Id < 1)
                {
                    var data = UnitOfWork.GetById<Pupil>(model.Data.Id);
                    var entity = model.BuildData();


                    entity.HistoryLink = ApiController.WriteHistory(entity.HistoryLink,
                        "Создание воспитанника учреждения социальной защиты", string.Empty);
                    entity.HistoryLinkId = entity.HistoryLink.Id;

                    UnitOfWork.AddEntity(entity);

                    RiseDrugs(data, entity, UnitOfWork);

                    WriteHistory(entity.Id, "создал воспитанника учреждения социальной защиты");

                    UnitOfWork.SaveChanges();

                    tran.Complete();
                }
                else
                {
                    if (UnitOfWork.GetLastUpdateTickById<Pupil>(model.Data.Id) != model.Data.LastUpdateTick)
                    {
                        SetRedicted();
                        return RedirectToAction(nameof(OrphanagePupilEdit), new {pupilId = model.Data.Id});
                    }

                    var data = UnitOfWork.GetById<Pupil>(model.Data.Id);
                    var entity = model.BuildData();

                    entity.HistoryLinkId = data.HistoryLinkId;

                    var diff = GetDiff(entity, data);

                    diff += RiseDrugs(data, entity, UnitOfWork);

                    var filediff = data.LinkToFiles.Diff(entity.LinkToFiles, UnitOfWork);
                    if (!string.IsNullOrWhiteSpace(filediff))
                    {
                        diff += $"<li>{filediff}</li>";
                        var link = data.LinkToFiles.SaveFiles(entity.LinkToFiles, UnitOfWork);
                        data.LinkToFilesId = link.Id;
                        entity.LinkToFilesId = data.LinkToFilesId;
                    }

                    if (!string.IsNullOrWhiteSpace(diff))
                    {
                        data.HistoryLink = ApiController.WriteHistory(data.HistoryLink,
                            "Обновление сведений о воспитаннике учреждения социальной защиты", $"<ul>{diff}</ul>");
                        data.HistoryLinkId = data.HistoryLink?.Id;


                        data.CopyEntity(entity);
                        data.Child.CopyEntity(entity.Child);
                        data.Child.Address.CopyEntity(entity.Child.Address);

                        WriteHistory(entity.Id, "обновил данные воспитанника учреждения социальной защиты");

                        UnitOfWork.SaveChanges();
                    }

                    tran.Complete();
                }
            }

            return RedirectToAction(nameof(OrphanagePupilEdit), new {pupilId = model.Data.Id});
        }

        /// <summary>
        ///     отправить СНИЛС воспитанника на проверку
        /// </summary>
        [Route("Orphanage/Pupils/SNILSCheck/{pupilId}")]
        public ActionResult OrphanagePupilSNILSCheck(long pupilId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            var res = UnitOfWork.GetById<Pupil>(pupilId);

            if (!Security.HasRight(AccessRightEnum.Orphans.Main, res.OrphanageAddress.OrganisationId))
            {
                return RedirectToAvalibleAction();
            }

            var exchangeBaseRegistryCode = WebConfigurationManager.AppSettings["exchangeBaseRegistryCode"];
            var requestNumber = ExchangeController.GetServiceNumber(exchangeBaseRegistryCode);

            ExchangeController.CheckChildForSnils(requestNumber, res.ChildId.Value, 1);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        ///     добавить наркотик воспитаннику
        /// </summary>
        [Route("Orphanage/Pupils/AddDrug")]
        public ActionResult AddDrug()
        {
            ViewData.TemplateInfo.HtmlFieldPrefix = string.Format("Doses[{0}]", Guid.NewGuid().ToString());

            ViewBag.DrugTypes = UnitOfWork.GetSet<TypeOfDrug>().ToList();

            return PartialView("EditorTemplates/PupilDrugs", new PupilDose());
        }

        /// <summary>
        ///     Разница в воспитаннике приюта
        /// </summary>
        private static string GetDiff(Pupil entity, Pupil persisted)
        {
            var sb = new StringBuilder();

            if (persisted.Child?.FirstName != entity.Child?.FirstName)
            {
                sb.AppendLine(
                    $"<li>Изменено имя воспитанника, старое значение:'{persisted.Child?.FirstName.FormatEx(string.Empty)}', новое значение:'{entity.Child?.FirstName.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Child?.LastName != entity.Child?.LastName)
            {
                sb.AppendLine(
                    $"<li>Изменена фамилия воспитанника, старое значение:'{persisted.Child?.LastName.FormatEx(string.Empty)}', новое значение:'{entity.Child?.LastName.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Child?.MiddleName != entity.Child?.MiddleName ||
                persisted.Child?.HaveMiddleName != entity.Child?.HaveMiddleName)
            {
                sb.AppendLine(
                    $"<li>Изменено отчество воспитанника, старое значение:'{persisted.Child?.MiddleName.FormatEx(string.Empty)}', новое значение:'{entity.Child?.MiddleName.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Child?.Snils != entity.Child?.Snils)
            {
                sb.AppendLine(
                    $"<li>Изменен СНИЛС воспитанника, старое значение:'{persisted.Child?.Snils.FormatEx(string.Empty)}', новое значение:'{entity.Child?.Snils.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Child?.DateOfBirth != entity.Child?.DateOfBirth)
            {
                sb.AppendLine(
                    $"<li>Изменена дата рождения воспитанника, старое значение:'{persisted.Child?.DateOfBirth.FormatEx(string.Empty)}', новое значение:'{entity.Child?.DateOfBirth.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Child?.Male != entity.Child?.Male)
            {
                sb.AppendLine(
                    $"<li>Изменена пол воспитанника, старое значение:'{(persisted.Child?.Male ?? false ? "Мужской" : "Женский")}', новое значение:'{(entity.Child?.Male ?? false ? "Мужской" : "Женский")}'</li>");
            }

            if (persisted.Child?.PlaceOfBirth != entity.Child?.PlaceOfBirth)
            {
                sb.AppendLine(
                    $"<li>Изменено место рождения воспитанника, старое значение:'{persisted.Child?.PlaceOfBirth.FormatEx(string.Empty)}', новое значение:'{entity.Child?.PlaceOfBirth.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.OrphanageAddressId != entity.OrphanageAddressId)
            {
                sb.AppendLine(
                    $"<li>Изменен адрес учреждения воспитанника, старое значение:'{persisted.OrphanageAddress?.Address?.Name.FormatEx(string.Empty)}', новое значение:'{entity.OrphanageAddress?.Address?.Name.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Child?.AddressId != entity.Child?.AddressId ||
                persisted.Child?.Address?.FiasId != entity.Child?.Address?.FiasId ||
                persisted.Child?.Address?.Name != entity.Child?.Address?.Name ||
                persisted.Child?.Address?.Appartment != entity.Child?.Address?.Appartment)
            {
                sb.AppendLine(
                    $"<li>Изменен адрес регистрации воспитанника, старое значение:'{persisted.Child?.Address?.Name.FormatEx(string.Empty)}, кв.{persisted.Child?.Address?.Appartment.FormatEx(string.Empty)}', новое значение:'{entity.Child?.Address?.Name.FormatEx(string.Empty)}, кв.{entity.Child?.Address?.Appartment.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Child?.DateOfBirth != entity.Child?.DateOfBirth)
            {
                sb.AppendLine(
                    $"<li>Изменена дата рождения воспитанника, старое значение:'{persisted.Child?.DateOfBirth.FormatEx(string.Empty)}', новое значение:'{entity.Child?.DateOfBirth.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Child?.DocumentTypeId != entity.Child?.DocumentTypeId ||
                persisted.Child?.DocumentSeria != entity.Child?.DocumentSeria ||
                persisted.Child?.DocumentNumber != entity.Child?.DocumentNumber ||
                persisted.Child?.DocumentDateOfIssue != entity.Child?.DocumentDateOfIssue ||
                persisted.Child?.DocumentSubjectIssue != entity.Child?.DocumentSubjectIssue)
            {
                sb.AppendLine(
                    $"<li>Изменен документ удостоверяющего личность воспитанника, старое значение:'{persisted.Child?.DocumentType?.Name.FormatEx(string.Empty)}, серия: {persisted.Child?.DocumentSeria.FormatEx(string.Empty)}, номер: {persisted.Child?.DocumentNumber.FormatEx(string.Empty)}, дата выдачи: {persisted.Child?.DocumentDateOfIssue.FormatEx("dd.MM.yyyy")}, выдан: {persisted.Child?.DocumentSubjectIssue.FormatEx(string.Empty)}', новое значение:'{entity.Child?.DocumentType?.Name.FormatEx(string.Empty)}, серия: {entity.Child?.DocumentSeria.FormatEx(string.Empty)}, номер: {entity.Child?.DocumentNumber.FormatEx(string.Empty)}, дата выдачи: {entity.Child?.DocumentDateOfIssue.FormatEx("dd.MM.yyyy")}, выдан: {entity.Child?.DocumentSubjectIssue.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Child?.TypeOfRestrictionId != entity.Child?.TypeOfRestrictionId)
            {
                sb.AppendLine(
                    $"<li>Изменен вид ограничения воспитанника, старое значение:'{persisted.Child?.TypeOfRestriction?.Name.FormatEx(string.Empty)}', новое значение:'{entity.Child?.TypeOfRestriction?.Name.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Child?.TypeOfSubRestrictionId != entity.Child?.TypeOfSubRestrictionId)
            {
                sb.AppendLine(
                    $"<li>Изменен подвид ограничения воспитанника, старое значение:'{persisted.Child?.TypeOfSubRestriction?.Name.FormatEx(string.Empty)}', новое значение:'{entity.Child?.TypeOfSubRestriction?.Name.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.SchoolId != entity.SchoolId)
            {
                sb.AppendLine(
                    $"<li>Изменено образовательное учереждение воспитанника, старое значение:'{persisted.School?.Name.FormatEx(string.Empty)}', новое значение:'{entity.School?.Name.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.SchoolName != entity.SchoolName)
            {
                sb.AppendLine(
                    $"<li>Изменено образовательное учереждение воспитанника, старое значение:'{persisted.SchoolName.FormatEx(string.Empty)}', новое значение:'{entity.SchoolName.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.DateIn != entity.DateIn)
            {
                sb.AppendLine(
                    $"<li>Изменена дата зачисления воспитанника, старое значение:'{persisted.DateIn.FormatEx(string.Empty)}', новое значение:'{entity.DateIn.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.DateOut != entity.DateOut)
            {
                sb.AppendLine(
                    $"<li>Изменена дата зачисления воспитанника, старое значение:'{persisted.DateOut.FormatEx(string.Empty)}', новое значение:'{entity.DateOut.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.OrganisationOut != entity.OrganisationOut)
            {
                sb.AppendLine(
                    $"<li>Изменена дата зачисления воспитанника, старое значение:'{persisted.OrganisationOut.FormatEx(string.Empty)}', новое значение:'{entity.OrganisationOut.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.GlutenFreeFood != entity.GlutenFreeFood)
            {
                sb.AppendLine(
                    $"<li>Изменена особенность в питании воспитанника (безглютеновое), старое значение:'{persisted.GlutenFreeFood.FormatEx(string.Empty) ?? "Нет"}', новое значение:'{entity.GlutenFreeFood.FormatEx(string.Empty) ?? "Нет"}'</li>");
            }

            if (persisted.PureedFood != entity.PureedFood)
            {
                sb.AppendLine(
                    $"<li>Изменена особенность в питании воспитанника (протертое), старое значение:'{persisted.PureedFood.FormatEx(string.Empty) ?? "Нет"}', новое значение:'{entity.PureedFood.FormatEx(string.Empty) ?? "Нет"}'</li>");
            }

            if (persisted.FoodAditionals != entity.FoodAditionals)
            {
                sb.AppendLine(
                    $"<li>Изменена дополнительная информация об особенности в питании воспитанника, старое значение:'{persisted.FoodAditionals.FormatEx(string.Empty)}', новое значение:'{entity.FoodAditionals.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Foul != entity.Foul)
            {
                sb.AppendLine(
                    $"<li>Изменена информация о нарушении воспитанником правил, старое значение:'{persisted.Foul.FormatEx(string.Empty) ?? "Нет"}', новое значение:'{entity.Foul.FormatEx(string.Empty) ?? "Нет"}'</li>");
            }

            if (persisted.FoulRegionRestriction != entity.FoulRegionRestriction)
            {
                sb.AppendLine(
                    $"<li>Изменена информация о нарушение воспитанником правил с ограничением региона, старое значение:'{persisted.FoulRegionRestriction.FormatEx(string.Empty)}', новое значение:'{entity.FoulRegionRestriction.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.FoulRegionRestrictionFrom != entity.FoulRegionRestrictionFrom)
            {
                sb.AppendLine(
                    $"<li>Изменена информация периоде ограничения, в связи с нарушением воспитанником правил с ограничением региона, старое значение (с даты):'{persisted.FoulRegionRestrictionFrom.FormatEx(string.Empty)}', новое значение:'{entity.FoulRegionRestrictionFrom.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.FoulRegionRestrictionTo != entity.FoulRegionRestrictionTo)
            {
                sb.AppendLine(
                    $"<li>Изменена информация периоде ограничения, в связи с нарушением воспитанником правил с ограничением региона, старое значение (по дату):'{persisted.FoulRegionRestrictionTo.FormatEx(string.Empty)}', новое значение:'{entity.FoulRegionRestrictionTo.FormatEx(string.Empty)}'</li>");
            }


            if (persisted.Filled != entity.Filled)
            {
                sb.AppendLine(
                    $"<li>Изменён признак достаточной заполненности данных о воспитаннике, старое значение:'{persisted.Filled.FormatEx(string.Empty)}', новое значение:'{entity.Filled.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.Child?.IsDeleted != entity.Child?.IsDeleted)
            {
                sb.AppendLine(
                    $"<li>Изменён признак того, что воспитанник удалён, старое значение:'{persisted.Child?.IsDeleted.FormatEx()}', новое значение:'{entity.Child?.IsDeleted.FormatEx()}'</li>");
            }

            var res = sb.ToString();
            if (string.IsNullOrWhiteSpace(res))
            {
                return null;
            }

            return res;
        }

        /// <summary>
        ///     Справочники ограничений
        /// </summary>
        private void ViewbagTypeOfRestrictionsSet()
        {
            var typeOfRestrictions = UnitOfWork.GetSet<TypeOfRestriction>().Where(t => t.IsActive).OrderBy(p => p.Name)
                .ToList();
            ViewBag.RestrictionTypes =
                typeOfRestrictions;
            ViewBag.TypeOfRestrictionSubs =
                typeOfRestrictions.Where(t => t.Subs != null && t.Subs.Any()).Select(t => t.Id).ToArray();
        }

        /// <summary>
        ///     Справочники для воспитанника приюта
        /// </summary>
        private void ViewbagPupilsSet(long? orphanageId = null)
        {
            ViewBag.OrphanageAdresses =
                UnitOfWork.GetSet<OrphanageAddress>()
                    .Where(ss => ss.Organisation.Id == orphanageId && ss.Organisation.Orphanage == true).ToList();
            ViewBag.DocumentTypes =
                UnitOfWork.GetSet<DocumentType>().Where(ss => ss.ForChild).ToList();

            ViewBag.DrugTypes = UnitOfWork.GetSet<TypeOfDrug>().ToList();

            ViewbagTypeOfRestrictionsSet();
        }

        /// <summary>
        ///     Сохранить наркотики
        /// </summary>
        private static string RiseDrugs(Pupil data, Pupil entity, IUnitOfWork unitOfWork)
        {
            var result = string.Empty;
            var drugs = new List<string>();
            foreach (var dose in data?.Drugs?.Where(ss => !ss.IsDeleted).ToList() ?? new List<PupilDose>(0))
            {
                if (entity.Drugs == null || !entity.Drugs.Any(ss => ss.Id == dose.Id))
                {
                    dose.IsDeleted = true;
                    drugs.Add(dose.Drug.Name);
                    unitOfWork.SaveChanges();
                }
            }

            if (drugs.Count > 0)
            {
                result += $"<li>Удалены препараты: {string.Join(", ", drugs)}</li>";
                drugs.Clear();
            }


            foreach (var dose in entity.Drugs?.ToList() ?? new List<PupilDose>(0))
            {
                if (dose.Id > 0)
                {
                    continue;
                }

                var drug = unitOfWork.GetSet<Drug>().FirstOrDefault(ss =>
                    ss.Name == dose.Drug.Name && ss.Storage == dose.Drug.Storage &&
                    ss.DrugTypeId == dose.Drug.DrugTypeId);
                if (drug == null)
                {
                    drug = new Drug
                    {
                        Name = dose.Drug.Name,
                        Storage = dose.Drug.Storage,
                        DrugTypeId = dose.Drug.DrugTypeId
                    };
                    unitOfWork.AddEntity(drug);
                    unitOfWork.SaveChanges();
                }

                var new_drug_dose = new PupilDose
                {
                    PupilId = data.Id,
                    Dose = dose.Dose ?? string.Empty,
                    DrugId = drug.Id
                };

                unitOfWork.AddEntity(new_drug_dose);
                unitOfWork.SaveChanges();

                drugs.Add(dose.Drug.Name);
            }

            if (drugs.Count > 0)
            {
                result += $"<li>Добавлены препараты: {string.Join(", ", drugs)}</li>";
            }

            return result;
        }

        /// <summary>
        ///     Установка признака заполненности
        /// </summary>
        private void PupilFilledSet(Pupil pupil)
        {
            var subsValid = true;
            if ((pupil.Child?.IsInvalid ?? false) && (pupil.Child?.TypeOfRestrictionId.HasValue ?? false))
            {
                var subIds = UnitOfWork.GetSet<TypeOfRestriction>().Where(ss => ss.Id == pupil.Child.TypeOfRestrictionId.Value).SelectMany(ss => ss.Subs).Select(ss => ss.Id).ToList();
                if (subIds.Count > 0 && !subIds.Contains(pupil.Child?.TypeOfSubRestrictionId ?? 0))
                {
                    subsValid = false;
                }
            }

            pupil.Filled = !string.IsNullOrWhiteSpace(pupil.Child?.LastName)
                           && !string.IsNullOrWhiteSpace(pupil.Child?.FirstName)
                           && (!string.IsNullOrWhiteSpace(pupil.Child?.MiddleName) ||
                               (!pupil.Child?.HaveMiddleName ?? false))
                           && (pupil.Child?.DateOfBirth.HasValue ?? false)
                           && !string.IsNullOrWhiteSpace(pupil.Child?.Snils)
                           && !string.IsNullOrWhiteSpace(pupil.Child?.PlaceOfBirth)
                           && ((!pupil.Child?.IsInvalid ?? false) || ((pupil.Child?.IsInvalid ?? false) && (pupil.Child?.TypeOfRestrictionId.HasValue ?? false) && subsValid))
                           && pupil.OrphanageAddressId.HasValue
                           && (pupil.Child?.DocumentTypeId.HasValue ?? false)
                           && (DocumentTypeHelper.IsForeignBirthCert(pupil.Child?.DocumentTypeId) || DocumentTypeHelper.IsPassportOfForeignCountry(pupil.Child?.DocumentTypeId) || !string.IsNullOrWhiteSpace(pupil.Child?.DocumentSeria))
                           && !string.IsNullOrWhiteSpace(pupil.Child?.DocumentNumber)
                           && (pupil.Child?.DocumentDateOfIssue.HasValue ?? false)
                           && !string.IsNullOrWhiteSpace(pupil.Child?.DocumentSubjectIssue)
                           && (!pupil.Child?.IsDeleted ?? false);

            if (pupil.FoulRegionRestriction &&
                (!pupil.FoulRegionRestrictionFrom.HasValue || !pupil.FoulRegionRestrictionTo.HasValue))
            {
                pupil.Filled = false;
            }
        }
    }
}
