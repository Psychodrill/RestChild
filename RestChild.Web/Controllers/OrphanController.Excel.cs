using OfficeOpenXml.Style;
using RestChild.Comon.Enumeration;
using RestChild.Comon.ToExcel;
using RestChild.Web.Models.Orphans;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestChild.Web.Controllers
{
    public partial class OrphanController
    {
        //идентификатор инвалидности (Передвигается с помощью инвалидного кресла-коляски), соответствующего emun нет
        private const int typeOfSubRestriction = 1; 

        /// <summary>
        ///     выгрузка реестра учреждений социальной защиты
        /// </summary>
        public ActionResult ExcelOrphanageList(OrphanageFilterModel filter)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRightForSomeOrganization(AccessRightEnum.Orphans.Main))
            {
                return RedirectToAvalibleAction();
            }

            filter.Result = ApiController.GetOrphanage(filter, 0, true);

            var columns = new List<ExcelColumn<OrphanageResultListModel>>
                {
                    new ExcelColumn<OrphanageResultListModel> {Title = "Наименование учреждения социальной защиты", Func = t => t.Name, Width = 70 },
                    new ExcelColumn<OrphanageResultListModel> {Title = "Краткое наименование", Func = t => t.ShortName, Width = 40 },
                    new ExcelColumn<OrphanageResultListModel> {Title = "Адрес", Func = t => string.Join("\n\r", t.Address), Width = 70 },
                    new ExcelColumn<OrphanageResultListModel> {Title = "Телефон", Func = t => t.Phone, Width = 40 },
                    new ExcelColumn<OrphanageResultListModel> {Title = "E-mail", Func = t => t.EMail, Width = 40 },
                    new ExcelColumn<OrphanageResultListModel> {Title = "Директор", Func = t => t.DirectorName, Width = 40 },
                    new ExcelColumn<OrphanageResultListModel> {Title = "ФИО (ответственного за отдых)", Func = t => t.FioRfr, Width = 40 },
                    new ExcelColumn<OrphanageResultListModel> {Title = "Телефон (ответственного за отдых)", Func = t => t.PhoneRfr, Width = 40 },
                    new ExcelColumn<OrphanageResultListModel> {Title = "E-mail (ответственного за отдых)", Func = t => t.EMailRfr, Width = 40 }
                };

            columns = columns.Select(c =>
            {
                c.WordWrap = true;
                c.VerticalAlignment = ExcelVerticalAlignment.Center;
                return c;
            }).ToList();


            using (var excel = new ExcelTable<OrphanageResultListModel>(columns))
            {
                const int startRow = 0;
                var excelWorksheet = excel.CreateExcelWorksheet("Список учреждений социальной защиты");

                excel.TableName = "Список учреждений социальной защиты";

                excel.DataBind(excelWorksheet, filter.Result, ExcelBorderStyle.Thin, startRow);

                return File(excel.CreateExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Реестр учреждений социальной защиты.xlsx");
            }
        }

        /// <summary>
        ///     выгрузка реестра потребностей учреждений социальной защиты
        /// </summary>
        public ActionResult ExcelOrphanageGroupsList(OrphanageGroupsFilterModel filter)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRightForSomeOrganization(AccessRightEnum.Orphans.Main))
            {
                return RedirectToAvalibleAction();
            }

            var query = ApiController.PrepareQueryPupilGroup(filter);

            var columns = new List<ExcelColumn<OrphanageGroupsExcelResultListModel>>
                {
                    new ExcelColumn<OrphanageGroupsExcelResultListModel> {Title = "Год потребности", Func = t => t.Year , Width = 40 },
                    new ExcelColumn<OrphanageGroupsExcelResultListModel> {Title = "Каникулярный период", Func = t => t.VacationPeriod , Width = 70 },
                    new ExcelColumn<OrphanageGroupsExcelResultListModel> {Title = "Форма отдыха и оздоровления", Func = t => t.FormOfRest, Width = 40 },
                    new ExcelColumn<OrphanageGroupsExcelResultListModel> {Title = "Регион отдыха", Func = t => string.Join("\n\r", t.RegionsOfRest), Width = 40 },
                    new ExcelColumn<OrphanageGroupsExcelResultListModel> {Title = "Период отдыха", Func = t => string.Join("\n\r", t.PeriodsOfRest), Width = 40 },
                    new ExcelColumn<OrphanageGroupsExcelResultListModel> {Title = "Каникулы с", Func = t => string.Join("\n\r", t.VacationsFrom), Width = 40 },
                    new ExcelColumn<OrphanageGroupsExcelResultListModel> {Title = "Каникулы по", Func = t => string.Join("\n\r", t.VacationsTo), Width = 40 },
                    new ExcelColumn<OrphanageGroupsExcelResultListModel> {Title = "Оздоровительная организация", Func = t => string.Join("\n\r", t.Camps), Width = 40 },
                    new ExcelColumn<OrphanageGroupsExcelResultListModel> {Title = "Учреждение социальной защиты", Func = t => t.OrphanageName, Width = 70 },
                    new ExcelColumn<OrphanageGroupsExcelResultListModel> {Title = "Порядковый номер группы", Func = t => t.Name, Width = 70 },
                    new ExcelColumn<OrphanageGroupsExcelResultListModel> {Title = "Количество воспитанников", Func = t => t.PipilCount, Width = 30 },
                    new ExcelColumn<OrphanageGroupsExcelResultListModel> {Title = "Количество воспитанников, передвигающихся с помощью кресла-коляски", Func = t => t.PupilРandicappedCount, Width = 90, WordWrap = true},
                    new ExcelColumn<OrphanageGroupsExcelResultListModel> {Title = "Количество сопровождающих", Func = t => t.OverseerCount, Width = 30 },
                    new ExcelColumn<OrphanageGroupsExcelResultListModel> {Title = "Количество сопровождающих от ГАУК \"МОСГОРТУР\"", Func = t => t.MGTOverseerCount, Width = 80, WordWrap = true },
                };

            columns = columns.Select(c =>
            {
                c.WordWrap = true;
                c.VerticalAlignment = ExcelVerticalAlignment.Center;
                return c;
            }).ToList();

            var result = query.ToList().Select(g => new OrphanageGroupsExcelResultListModel
            {
                Id = g.Id,
                Name = g.Name,
                Year = g.YearOfRest?.Year.ToString(),
                VacationPeriod = g.VacationPeriod?.Name,
                FormOfRest = g.FormOfRest?.Name,
                OrphanageName = g.Organization?.Name,
                RegionsOfRest =
                    g.Requests?.Select(sx => sx.PlaceOfRest?.Name).ToList() ??
                    new List<string>(),
                PeriodsOfRest =
                    g.Requests?.Select(sx => sx.TimeOfRest?.Name).ToList() ??
                    new List<string>(),
                Camps =
                    g.Requests?.Select(sx => sx.Tour?.Hotels?.Name).ToList() ??
                    new List<string>(),
                PipilCount = g.PupilsCount ?? 0,
                PupilРandicappedCount = g.PupilsHealthStatuses.Where(ss => ss.TypeOfSubRestrictionId == typeOfSubRestriction).Sum(ss => ss.PupilsCount) ?? 0,
                OverseerCount = g.CollaboratorsCount ?? 0,
                MGTOverseerCount = g.MGTCollaboratorsCount ?? 0,
                VacationsFrom = g.Requests?.Select(sx => $"{sx.VacationFrom:dd.MM.yyyy}").ToList() ??
                            new List<string>(),
                VacationsTo = g.Requests?.Select(sx => $"{sx.VacationTo:dd.MM.yyyy}").ToList() ??
                                new List<string>(),
            }).ToList();


            using (var excel = new ExcelTable<OrphanageGroupsExcelResultListModel>(columns))
            {
                const int startRow = 0;
                var excelWorksheet = excel.CreateExcelWorksheet("Список потребностей учреждений социальной защиты");

                excel.TableName = "Список потребностей учреждений социальной защиты";

                excel.DataBind(excelWorksheet, result, ExcelBorderStyle.Thin, startRow);

                return File(excel.CreateExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Реестр потребностей учреждений социальной защиты.xlsx");
            }
        }

        /// <summary>
        ///     выгрузка реестра групп отправок учреждений социальной защиты
        /// </summary>
        public ActionResult ExcelOrphanagePupilGroupListList(OrphanagePupilGroupListFilterModel filter)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRightForSomeOrganization(AccessRightEnum.Orphans.Main))
            {
                return RedirectToAvalibleAction();
            }

            var query = ApiController.PrepareQueryPupilGroupList(filter);


            var columns = new List<ExcelColumn<OrphanagePupilGroupListExcelResultListModel>>
                {
                    new ExcelColumn<OrphanagePupilGroupListExcelResultListModel> {Title = "Год потребности", Func = t => t.YearOfRest, Width = 40},
                    new ExcelColumn<OrphanagePupilGroupListExcelResultListModel> {Title = "Форма отдыха и оздоровления", Func = t => t.FormOfRest, Width = 40 },
                    new ExcelColumn<OrphanagePupilGroupListExcelResultListModel> {Title = "Регион отдыха", Func = t => t.RegionOfRest, Width = 40 },
                    new ExcelColumn<OrphanagePupilGroupListExcelResultListModel> {Title = "Период отдыха", Func = t => t.TimeOfRest, Width = 40 },
                    new ExcelColumn<OrphanagePupilGroupListExcelResultListModel> {Title = "Оздоровительная организация", Func = t => t.HotelName, Width = 50 },
                    new ExcelColumn<OrphanagePupilGroupListExcelResultListModel> {Title = "Наименование учреждения", Func = t => t.OrphanageName, Width = 70 },
                    new ExcelColumn<OrphanagePupilGroupListExcelResultListModel> {Title = "Порядковый номер группы", Func = t => t.GroupName , Width = 40 },
                    new ExcelColumn<OrphanagePupilGroupListExcelResultListModel> {Title = "Количество воспитанников", Func = t => t.Pupils.Length , Width = 40 },
                    new ExcelColumn<OrphanagePupilGroupListExcelResultListModel> {Title = "ФИО воспитанников", MultiValueColumn = true, FuncMulti = t =>
                    {
                        return t.Pupils.Select(sx => sx.Name).ToArray();
                    }, Width = 70, WordWrap = true },
                    new ExcelColumn<OrphanagePupilGroupListExcelResultListModel> {Title = "Количество воспитанников, передвигающихся с помощью кресла-коляски", MultiValueColumn = true, FuncMulti = t =>
                    {
                        return t.Pupils.Where(ss => ss.Pandicapped).Select(sx => sx.Name).ToArray();
                    }, Width = 90, WordWrap = true },
                    new ExcelColumn<OrphanagePupilGroupListExcelResultListModel> {Title = "Количество сопровождающих", Func = t => t.Collaborators.Length , Width = 40 },
                    new ExcelColumn<OrphanagePupilGroupListExcelResultListModel> {Title = "ФИО сопровождающих", MultiValueColumn = true, FuncMulti = t =>
                    {
                        return t.Collaborators.Select(sx => sx.Name).ToArray();
                    }, Width = 70, WordWrap = true },
                    new ExcelColumn<OrphanagePupilGroupListExcelResultListModel> {Title = "Билеты туда", MultiValueColumn = true, FuncMulti = t =>
                    {
                        return t.Pupils.Where(ss => ss.TicketsTo).Concat(t.Collaborators.Where(sx => sx.TicketsTo))
                                .Select(sx => sx.Name).ToArray();
                    }, Width = 70, WordWrap = true },
                    new ExcelColumn<OrphanagePupilGroupListExcelResultListModel> {Title = "Билеты обратно", MultiValueColumn = true, FuncMulti = t =>
                    {
                        return t.Pupils.Where(ss => ss.TicketsFrom)
                                .Concat(t.Collaborators.Where(sx => sx.TicketsFrom)).Select(sx => sx.Name).ToArray();
                    }, Width = 70, WordWrap = true },
                    new ExcelColumn<OrphanagePupilGroupListExcelResultListModel> {Title = "Особенности питания", MultiValueColumn = true, FuncMulti = t =>
                    {
                        return t.Pupils.Where(ss => ss.FoodFeatures.Length > 0)
                                .Select(ss => ss.Name + ":\n" + string.Join("\n", ss.FoodFeatures)).ToArray();
                    }, Width = 70, WordWrap = true },
                    new ExcelColumn<OrphanagePupilGroupListExcelResultListModel> {Title = "Лекарственные препараты", MultiValueColumn = true, FuncMulti = t =>
                    {
                        return t.Pupils.Where(ss => ss.Drugs.Length > 0)
                                .Select(ss => ss.Name + ":\n" + string.Join("\n", ss.Drugs)).ToArray();
                    }, Width = 70, WordWrap = true },
                };

            columns = columns.Select(c =>
            {
                c.WordWrap = true;
                c.VerticalAlignment = ExcelVerticalAlignment.Center;
                return c;
            }).ToList();

            var result = query.ToList().Select(ss => new OrphanagePupilGroupListExcelResultListModel
            {
                Id = ss.Id,
                OrphanageName = ss.PupilGroupRequest?.FirstOrDefault()?.PupilGroup?.Organization?.Name,
                GroupName = ss.PupilGroupRequest?.FirstOrDefault()?.PupilGroup?.Name,
                YearOfRest = ss.PupilGroupRequest?.FirstOrDefault()?.PupilGroup?.YearOfRest?.Name,
                FormOfRest = ss.PupilGroupRequest?.FirstOrDefault()?.PupilGroup?.FormOfRest?.Name,
                RegionOfRest = ss.PupilGroupRequest?.FirstOrDefault()?.PlaceOfRest?.Name,
                TimeOfRest = ss.PupilGroupRequest?.FirstOrDefault()?.Tour?.Name,
                HotelName = ss.PupilGroupRequest?.FirstOrDefault()?.Tour?.Hotels?.Name,
                Pupils = ss.GroupPupils?.Select(sx => new OrphanagePupilGroupListExcelResultPeopleModel
                {
                    Name = sx.Pupil?.Child?.GetFio(),
                    TicketsTo = sx.TicketTo,
                    TicketsFrom = sx.TicketFrom,
                    Drugs = sx.GroupPupilDoses?.Where(sa => !sa.Dose.IsDeleted).Select(sa =>
                        $"Наименование: {sa.Dose.Drug.Name} ({sa.Dose.Drug.DrugType.Name}), схема приема: {sa.DrugQuantity}, условия хранения: {sa.Dose.Drug.Storage};").ToArray(),
                    FoodFeatures = new List<string> { sx.Pupil?.FoodAditionals, sx.Pupil?.GlutenFreeFood ?? false ? "Безглютеновое" : string.Empty, sx.Pupil?.PureedFood ?? false ? "Протертое" : string.Empty }.Where(sa => !string.IsNullOrWhiteSpace(sa)).ToArray(),
                    Pandicapped = sx.Pupil?.Child?.TypeOfSubRestrictionId == typeOfSubRestriction
                }).OrderBy(sx => sx.Name).ToArray() ?? new OrphanagePupilGroupListExcelResultPeopleModel[0],
                Collaborators = ss.GroupCollaborators?.Select(sx => new OrphanagePupilGroupListExcelResultPeopleModel
                {
                    Name = sx.OrganisatonCollaborator?.Applicant.GetFio(),
                    TicketsTo = sx.TicketTo,
                    TicketsFrom = sx.TicketFrom
                }).OrderBy(sx => sx.Name).ToArray() ?? new OrphanagePupilGroupListExcelResultPeopleModel[0]
            }).ToList();


            using (var excel = new ExcelTable<OrphanagePupilGroupListExcelResultListModel>(columns))
            {
                const int startRow = 0;
                var excelWorksheet = excel.CreateExcelWorksheet("Список групп отправок учреждений социальной защиты");

                excel.TableName = "Список групп отправок учреждений социальной защиты";

                excel.DataBind(excelWorksheet, result, ExcelBorderStyle.Thin, startRow);

                return File(excel.CreateExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Реестр групп отправок учреждений социальной защиты.xlsx");
            }
        }
    }
}
