using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;

namespace MailingDemon.Tasks
{
    [Task]
    public class UpdateAnalytics : BaseTask
    {
        private const int SecondReportId = 4000;
        private const int DistrictReportId = 5000;

        /// <summary>
        ///     обновление аналитики
        /// </summary>
        protected override void Execute()
        {
            FillFirstReport();
            FillSecondReport();
        }

        /// <summary>
        ///     обновление второго отчета.
        /// </summary>
        private static void FillSecondReport()
        {
            var yearOfCompany = ConfigurationManager.AppSettings["YearMultiCompany"].IntParse() ?? DateTime.Today.Year;

            using (var unit = new UnitOfWork())
            {
                var entity = new ReportSheet
                {
                    Id = SecondReportId,
                    CodeAccess = new Guid(AccessRightEnum.Report.StatisticReport),
                    ReportTables = new List<ReportTable>(),
                    ReportName = "Статистика обработки заявлений",
                    SortOrder = 11
                };

                ApplicationProcessingStatistics(unit, yearOfCompany, entity);
                unit.SaveChanges();
                CountyStatistics(unit, yearOfCompany, entity);
                unit.SaveChanges();
            }
        }

        /// <summary>
        ///     Статистика обработки заявлений
        /// </summary>
        private static void ApplicationProcessingStatistics(IUnitOfWork unit, int yearOfCompany, ReportSheet entity)
        {
            var reportTable = new ReportTable
            {
                Id = SecondReportId,
                ReportSheetId = entity.Id,
                Rows = new List<ReportTableRow>(),
                SortOrder = 1,
                CssClass = "table table-bordered table-statustic"
            };

            entity.ReportTables.Add(reportTable);


            var i = 0;

            var headers = new List<ReportTableHead>
            {
                new ReportTableHead
                {
                    Id = SecondReportId + ++i,
                    SortOrder = i,
                    Name = "&nbsp;",
                    ReportTableId = SecondReportId
                },
                new ReportTableHead
                {
                    Id = SecondReportId + ++i,
                    SortOrder = i,
                    Name = "Итого",
                    ReportTableId = SecondReportId
                },
                new ReportTableHead
                {
                    Id = SecondReportId + ++i,
                    SortOrder = i,
                    Name = "Отказ в регистрации",
                    ReportTableId = SecondReportId
                },
                new ReportTableHead
                {
                    Id = SecondReportId + ++i,
                    SortOrder = i,
                    Name = "На исполнении. Зарегистрировано",
                    ReportTableId = SecondReportId
                },
                new ReportTableHead
                {
                    Id = SecondReportId + ++i,
                    SortOrder = i,
                    Name = "На исполнении. Формирование результата предоставления услуги",
                    ReportTableId = SecondReportId
                },
                new ReportTableHead
                {
                    Id = SecondReportId + ++i,
                    SortOrder = i,
                    Name = "Ожидание прихода Заявителя в ОИВ для подтверждения сведений, указанных в заявлении",
                    ReportTableId = SecondReportId
                },
                new ReportTableHead
                {
                    Id = SecondReportId + ++i,
                    SortOrder = i,
                    Name = "Отказ в предоставлении услуги",
                    ReportTableId = SecondReportId
                },
                new ReportTableHead
                {
                    Id = SecondReportId + ++i,
                    SortOrder = i,
                    Name = "Отозвано по инициативе заявителя",
                    ReportTableId = SecondReportId
                },
                new ReportTableHead
                {
                    Id = SecondReportId + ++i,
                    SortOrder = i,
                    Name = "Услуга оказана",
                    ReportTableId = SecondReportId
                }
            };

            reportTable.ReportTableHeads = headers;

            var queue =
                unit.GetSet<Request>()
                    .Where(r => r.IsLast && !r.IsDeleted && r.StatusId != (long) StatusEnum.Draft &&
                                r.SourceId.HasValue && r.YearOfRest.Year >= yearOfCompany &&
                                !r.TypeOfRest.Commercial);

            var mpguQuery = queue.Where(q => q.SourceId == (long) SourceEnum.Mpgu);
            var mpguIndividual =
                mpguQuery.Where(
                        q => q.TypeOfRestId == (long) TypeOfRestEnum.ChildRest ||
                             q.TypeOfRest.ParentId == (long) TypeOfRestEnum.ChildRest).GroupBy(g => g.StatusId)
                    .ToDictionary(s => s.Key, r => r.Count());
            var mpguParents =
                mpguQuery.Where(
                        q => q.TypeOfRestId == (long) TypeOfRestEnum.RestWithParents ||
                             q.TypeOfRest.ParentId == (long) TypeOfRestEnum.RestWithParents ||
                             q.TypeOfRest.Parent.ParentId == (long) TypeOfRestEnum.RestWithParents)
                    .GroupBy(g => g.StatusId)
                    .ToDictionary(s => s.Key, r => r.Count());
            var operatorQuery = queue.Where(q => q.SourceId == (long) SourceEnum.Operator);
            var operatorIndividual =
                operatorQuery.Where(
                        q => q.TypeOfRestId == (long) TypeOfRestEnum.ChildRest ||
                             q.TypeOfRest.ParentId == (long) TypeOfRestEnum.ChildRest).GroupBy(g => g.StatusId)
                    .ToDictionary(s => s.Key, r => r.Count());
            var operatorParents =
                operatorQuery.Where(
                        q => q.TypeOfRestId == (long) TypeOfRestEnum.RestWithParents ||
                             q.TypeOfRest.ParentId == (long) TypeOfRestEnum.RestWithParents ||
                             q.TypeOfRest.Parent.ParentId == (long) TypeOfRestEnum.RestWithParents)
                    .GroupBy(g => g.StatusId)
                    .ToDictionary(s => s.Key, r => r.Count());


            var mpguAll = GetCollectedDict(null, mpguIndividual);
            mpguAll = GetCollectedDict(mpguAll, mpguParents);

            var operatorAll = GetCollectedDict(null, operatorIndividual);
            operatorAll = GetCollectedDict(operatorAll, operatorParents);

            var total = GetCollectedDict(null, mpguAll);
            total = GetCollectedDict(total, operatorAll);

            var rows = new List<ReportTableRow>
            {
                FillRow("МПГУ - индивидуальный отдых", 1, mpguIndividual),
                FillRow("МПГУ - совместный отдых", 2, mpguParents),
                FillRow("Итого - МПГУ", 3, mpguAll, "table-statustic-subtotal"),
                FillRow("Оператор - индивидуальный отдых", 4, operatorIndividual),
                FillRow("Оператор - совместный отдых", 5, operatorParents),
                FillRow("Итого - оператор", 6, operatorAll, "table-statustic-subtotal"),
                FillRow("Всего", 7, total, "table-statustic-total")
            };

            if (unit.GetSet<ReportSheet>().Any(r => r.Id == SecondReportId))
            {
                unit.Delete(new ReportSheet {Id = SecondReportId});
                unit.SaveChanges();
            }

            unit.AddEntity(entity);

            foreach (var row in rows)
            {
                unit.AddEntity(row);
            }


            unit.SaveChanges();

            var rowsCount = rows.Count;

            reportTable = new ReportTable
            {
                Id = SecondReportId + 1,
                ReportSheetId = entity.Id,
                Rows = new List<ReportTableRow>(),
                SortOrder = 2,
                CssClass = "table table-bordered table-statustic",
                Name = " "
            };

            entity.ReportTables.Add(reportTable);

            var countPlace = unit.GetSet<Tour>()
                .Where(t => t.StateId.HasValue && t.StateId != StateMachineStateEnum.Deleted &&
                            t.YearOfRest.Year >= yearOfCompany && t.IsActive &&
                            !t.TypeOfRest.Commercial &&
                            t.TypeOfRestId != (long) TypeOfRestEnum.SpecializedСamp &&
                            t.TypeOfRest.ParentId != (long) TypeOfRestEnum.SpecializedСamp).Select(
                    t =>
                        t.Volumes.Select(v =>
                            v.TypeOfRoomsId.HasValue
                                ? v.TypeOfRooms.CountBasePlace * v.CountRooms
                                : v.CountPlace).Sum()).Sum() ?? 0;

            var queueChild = unit.GetSet<Child>()
                .Where(
                    c =>
                        c.Request.IsLast && !c.Request.IsDeleted &&
                        c.Request.StatusId == (long) StatusEnum.CertificateIssued &&
                        c.Request.SourceId.HasValue && c.Request.YearOfRest.Year >= yearOfCompany &&
                        !c.Request.TypeOfRest.Commercial);
            var attendants = unit.GetSet<Applicant>()
                .Where(
                    c =>
                        c.Request.IsLast && !c.Request.IsDeleted && c.IsAccomp &&
                        c.Request.StatusId == (long) StatusEnum.CertificateIssued &&
                        c.Request.SourceId.HasValue && c.Request.YearOfRest.Year >= yearOfCompany &&
                        !c.Request.TypeOfRest.Commercial);
            var applicant = unit.GetSet<Request>().Where(
                c =>
                    c.IsLast && !c.IsDeleted && c.Applicant.IsAccomp &&
                    c.StatusId == (long) StatusEnum.CertificateIssued && c.SourceId.HasValue &&
                    c.YearOfRest.Year >= yearOfCompany && !c.TypeOfRest.Commercial);

            var countRequest = queueChild.Count() + attendants.Count() + applicant.Count();

            rows = new List<ReportTableRow>();

            var rowId = SecondReportId + rowsCount + 1;

            rows.Add(new ReportTableRow
            {
                Id = rowId,
                SortOrder = 1,
                TableId = SecondReportId + 1,
                CssClass = "table-statustic-total",
                RowData = new List<ReportRowData>
                {
                    new ReportRowData
                    {
                        Value = "Всего мест / Выдано путевок",
                        RowId = rowId,
                        Id = SecondReportId + 11 * 10 + 1,
                        SortOrder = 1,
                        CssClass = "table-statistic-first",
                        Style = "width:50%; font-weight: normal;"
                    },
                    new ReportRowData
                    {
                        Value = $"{countPlace} / {countRequest}",
                        RowId = rowId,
                        Id = SecondReportId + 11 * 10 + 2,
                        SortOrder = 1,
                        Style = "width:50%;"
                    }
                }
            });

            unit.AddEntity(reportTable);

            foreach (var row in rows)
            {
                unit.AddEntity(row);
            }
        }

        /// <summary>
        ///     Статистика по округам
        /// </summary>
        private static void CountyStatistics(IUnitOfWork unit, int yearOfCompany, ReportSheet entity)
        {
            var reportTable = new ReportTable
            {
                Id = DistrictReportId,
                ReportSheetId = entity.Id,
                Rows = new List<ReportTableRow>(),
                SortOrder = 3,
                CssClass = "table table-bordered table-statustic",
                Name = "Статистика по округам"
            };

            entity.ReportTables.Add(reportTable);

            var i = 0;

            var headers = new List<ReportTableHead>
            {
                new ReportTableHead
                {
                    Id = DistrictReportId + ++i,
                    SortOrder = i,
                    Name = "Административный округ города Москвы",
                    ReportTableId = DistrictReportId,
                    RowIndex = 1,
                    RowSpan = 2
                },
                new ReportTableHead
                {
                    Id = DistrictReportId + ++i,
                    SortOrder = i,
                    Name = "Отдых детей в возрасте от 7 до 15 лет",
                    RowIndex = 1,
                    ColSpan = 2,
                    ReportTableId = DistrictReportId
                },
                new ReportTableHead
                {
                    Id = DistrictReportId + ++i,
                    SortOrder = i,
                    Name = "Отдых детей в сопровождении родителей (законных представителей)",
                    RowIndex = 1,
                    ColSpan = 2,
                    ReportTableId = DistrictReportId
                },
                new ReportTableHead
                {
                    Id = DistrictReportId + ++i,
                    SortOrder = i,
                    Name = "Сертификат на отдых и оздоровление",
                    RowIndex = 1,
                    ColSpan = 2,
                    ReportTableId = DistrictReportId
                },
                new ReportTableHead
                {
                    Id = DistrictReportId + ++i,
                    SortOrder = i,
                    Name = "Молодежный отдых",
                    RowIndex = 1,
                    ColSpan = 2,
                    ReportTableId = DistrictReportId
                },
                new ReportTableHead
                {
                    Id = DistrictReportId + ++i,
                    SortOrder = i,
                    Name = "Принято заявлений",
                    RowIndex = 2,
                    ReportTableId = DistrictReportId
                },
                new ReportTableHead
                {
                    Id = DistrictReportId + ++i,
                    SortOrder = i,
                    Name = "Выдано путевок",
                    RowIndex = 2,
                    ReportTableId = DistrictReportId
                },
                new ReportTableHead
                {
                    Id = DistrictReportId + ++i,
                    SortOrder = i,
                    Name = "Принято заявлений",
                    RowIndex = 2,
                    ReportTableId = DistrictReportId
                },
                new ReportTableHead
                {
                    Id = DistrictReportId + ++i,
                    SortOrder = i,
                    Name = "Выдано путевок",
                    RowIndex = 2,
                    ReportTableId = DistrictReportId
                },
                new ReportTableHead
                {
                    Id = DistrictReportId + ++i,
                    SortOrder = i,
                    Name = "Принято заявлений",
                    RowIndex = 2,
                    ReportTableId = DistrictReportId
                },
                new ReportTableHead
                {
                    Id = DistrictReportId + ++i,
                    SortOrder = i,
                    Name = "Выдано сертификатов",
                    RowIndex = 2,
                    ReportTableId = DistrictReportId
                },
                new ReportTableHead
                {
                    Id = DistrictReportId + ++i,
                    SortOrder = i,
                    Name = "Принято заявлений",
                    RowIndex = 2,
                    ReportTableId = DistrictReportId
                },
                new ReportTableHead
                {
                    Id = DistrictReportId + ++i,
                    SortOrder = i,
                    Name = "Выдано путевок",
                    RowIndex = 2,
                    ReportTableId = DistrictReportId
                }
            };

            reportTable.ReportTableHeads = headers;

            var queue =
                unit.GetSet<Request>()
                    .Where(
                        r =>
                            r.IsLast && !r.IsDeleted && r.StatusId != (long) StatusEnum.Draft &&
                            r.SourceId.HasValue &&
                            r.YearOfRest.Year >= yearOfCompany && !r.TypeOfRest.Commercial);

            //Отдых детей в возрасте от 7 до 15 лет - Принято заявлений
            var childRest =
                queue.Where(
                        q => q.TypeOfRestId == (long) TypeOfRestEnum.ChildRest ||
                             q.TypeOfRest.ParentId == (long) TypeOfRestEnum.ChildRest)
                    .GroupBy(
                        q =>
                            q.Child.FirstOrDefault().Address.BtiDistrictId ??
                            q.Child.FirstOrDefault().Address.BtiAddress.BtiDistrictId ?? 15)
                    .ToDictionary(q => q.Key, r => r.Count());

            //Отдых детей в возрасте от 7 до 15 лет - Выдано путевок
            var childRestCertificate =
                queue.Where(
                        q => (q.TypeOfRestId == (long) TypeOfRestEnum.ChildRest ||
                              q.TypeOfRest.ParentId == (long) TypeOfRestEnum.ChildRest)
                             && q.StatusId == (long) StatusEnum.CertificateIssued)
                    .GroupBy(
                        q =>
                            q.Child.FirstOrDefault().Address.BtiDistrictId ??
                            q.Child.FirstOrDefault().Address.BtiAddress.BtiDistrictId ?? 15)
                    .ToDictionary(q => q.Key, r => r.Count());

            //Отдых детей в сопровождении родителей (законных представителей) - Принято заявлений
            var individualRest =
                queue.Where(
                        q => q.TypeOfRestId == (long) TypeOfRestEnum.RestWithParents ||
                             q.TypeOfRest.ParentId == (long) TypeOfRestEnum.RestWithParents ||
                             q.TypeOfRestId == (long) TypeOfRestEnum.RestWithParentsInvalidOrphanComplex ||
                             q.TypeOfRest.ParentId == (long) TypeOfRestEnum.RestWithParentsInvalidOrphanComplex)
                    .GroupBy(
                        q =>
                            q.Child.FirstOrDefault().Address.BtiDistrictId ??
                            q.Child.FirstOrDefault().Address.BtiAddress.BtiDistrictId ?? 15)
                    .ToDictionary(q => q.Key, r => r.Count());

            //Отдых детей в сопровождении родителей (законных представителей) - Выдано путевок
            var individualRestCertificate =
                queue.Where(
                        q => (q.TypeOfRestId == (long) TypeOfRestEnum.RestWithParents ||
                              q.TypeOfRest.ParentId == (long) TypeOfRestEnum.RestWithParents ||
                              q.TypeOfRestId == (long) TypeOfRestEnum.RestWithParentsInvalidOrphanComplex ||
                              q.TypeOfRest.ParentId == (long) TypeOfRestEnum.RestWithParentsInvalidOrphanComplex)
                             && q.StatusId == (long) StatusEnum.CertificateIssued)
                    .GroupBy(
                        q =>
                            q.Child.FirstOrDefault().Address.BtiDistrictId ??
                            q.Child.FirstOrDefault().Address.BtiAddress.BtiDistrictId ?? 15)
                    .ToDictionary(q => q.Key, r => r.Count());

            //Сертификат на отдых и оздоровление - Принято заявлений
            var certificate =
                queue.Where(
                        q => q.TypeOfRestId == (long) TypeOfRestEnum.Money ||
                             q.TypeOfRest.ParentId == (long) TypeOfRestEnum.Money)
                    .GroupBy(
                        q =>
                            q.Child.FirstOrDefault().Address.BtiDistrictId ??
                            q.Child.FirstOrDefault().Address.BtiAddress.BtiDistrictId ?? 15)
                    .ToDictionary(q => q.Key, r => r.Count());

            //Сертификат на отдых и оздоровление - Выдано сертификатов
            var certificateCertificate =
                queue.Where(
                        q => (q.TypeOfRestId == (long) TypeOfRestEnum.Money ||
                              q.TypeOfRest.ParentId == (long) TypeOfRestEnum.Money)
                             && q.StatusId == (long) StatusEnum.CertificateIssued)
                    .GroupBy(
                        q =>
                            q.Child.FirstOrDefault().Address.BtiDistrictId ??
                            q.Child.FirstOrDefault().Address.BtiAddress.BtiDistrictId ?? 15)
                    .ToDictionary(q => q.Key, r => r.Count());


            //Молодежный отдых - Принято заявлений
            var youthRest =
                queue.Where(
                        q => q.TypeOfRestId == (long) TypeOfRestEnum.YouthRestCamps ||
                             q.TypeOfRest.ParentId == (long) TypeOfRestEnum.YouthRestCamps)
                    .GroupBy(
                        q =>
                            q.Applicant.Address.BtiDistrictId ??
                            q.Applicant.Address.BtiAddress.BtiDistrictId ?? 15)
                    .ToDictionary(q => q.Key, r => r.Count());

            //Молодежный отдых - Выдано путевок
            var youthRestCertificate =
                queue.Where(
                        q => (q.TypeOfRestId == (long) TypeOfRestEnum.YouthRestCamps ||
                              q.TypeOfRest.ParentId == (long) TypeOfRestEnum.YouthRestCamps)
                             && q.StatusId == (long) StatusEnum.CertificateIssued)
                    .GroupBy(
                        q =>
                            q.Applicant.Address.BtiDistrictId ??
                            q.Applicant.Address.BtiAddress.BtiDistrictId ?? 15)
                    .ToDictionary(q => q.Key, r => r.Count());


            var districts = unit.GetSet<BtiDistrict>().ToList();

            var rows = new List<ReportTableRow>();

            var rowStartIndex = DistrictReportId + 10;

            foreach (var district in districts)
            {
                rows.Add(FillRow(district.Name, district.Id, ref rowStartIndex, childRest, childRestCertificate,
                    individualRest,
                    individualRestCertificate, certificate, certificateCertificate, youthRest,
                    youthRestCertificate));
            }

            rows.Add(FillRow("Нет округа", 15, ref rowStartIndex, childRest, childRestCertificate, individualRest,
                individualRestCertificate, certificate, certificateCertificate, youthRest, youthRestCertificate));

            var result = new ReportTableRow
            {
                RowData = new List<ReportRowData>(),
                Id = DistrictReportId + 16,
                TableId = DistrictReportId,
                SortOrder = Convert.ToInt32(16)
            };
            result.RowData.Add(new ReportRowData
            {
                Value = "ИТОГО",
                ReportTableHeadId = DistrictReportId + 1,
                RowId = DistrictReportId + 16,
                Id = rowStartIndex++,
                SortOrder = 1,
                CssClass = "table-statistic-first"
            });


            var headId = 4;

            //Отдых детей в возрасте от 7 до 15 лет - Принято заявлений
            result.RowData.Add(new ReportRowData
            {
                Value = childRest.Values.Sum().FormatEx(),
                ReportTableHeadId = DistrictReportId + headId,
                RowId = DistrictReportId + 16,
                Id = rowStartIndex++,
                SortOrder = headId
            });
            headId++;

            //Отдых детей в возрасте от 7 до 15 лет - Выдано путевок
            result.RowData.Add(new ReportRowData
            {
                Value = childRestCertificate.Values.Sum().FormatEx(),
                ReportTableHeadId = DistrictReportId + headId,
                RowId = DistrictReportId + 16,
                Id = rowStartIndex++,
                SortOrder = headId
            });
            headId++;

            //Отдых детей в сопровождении родителей (законных представителей) - Принято заявлений
            result.RowData.Add(new ReportRowData
            {
                Value = individualRest.Values.Sum().FormatEx(),
                ReportTableHeadId = DistrictReportId + headId,
                RowId = DistrictReportId + 16,
                Id = rowStartIndex++,
                SortOrder = headId
            });
            headId++;

            //Отдых детей в сопровождении родителей (законных представителей) - Выдано путевок
            result.RowData.Add(new ReportRowData
            {
                Value = individualRestCertificate.Values.Sum().FormatEx(),
                ReportTableHeadId = DistrictReportId + headId,
                RowId = DistrictReportId + 16,
                Id = rowStartIndex++,
                SortOrder = headId
            });
            headId++;

            //Сертификат на отдых и оздоровление - Принято заявлений
            result.RowData.Add(new ReportRowData
            {
                Value = certificate.Values.Sum().FormatEx(),
                ReportTableHeadId = DistrictReportId + headId,
                RowId = DistrictReportId + 16,
                Id = rowStartIndex++,
                SortOrder = headId
            });
            headId++;

            //Сертификат на отдых и оздоровление - Выдано сертификатов
            result.RowData.Add(new ReportRowData
            {
                Value = certificateCertificate.Values.Sum().FormatEx(),
                ReportTableHeadId = DistrictReportId + headId,
                RowId = DistrictReportId + 16,
                Id = rowStartIndex++,
                SortOrder = headId
            });
            headId++;

            //Молодежный отдых - Принято заявлений
            result.RowData.Add(new ReportRowData
            {
                Value = youthRest.Values.Sum().FormatEx(),
                ReportTableHeadId = DistrictReportId + headId,
                RowId = DistrictReportId + 16,
                Id = rowStartIndex++,
                SortOrder = headId
            });
            headId++;

            //Молодежный отдых - Выдано путевок
            result.RowData.Add(new ReportRowData
            {
                Value = youthRestCertificate.Values.Sum().FormatEx(),
                ReportTableHeadId = DistrictReportId + headId,
                RowId = DistrictReportId + 16,
                Id = rowStartIndex,
                SortOrder = headId
            });
            rows.Add(result);

            unit.AddEntity(reportTable);

            foreach (var row in rows)
            {
                unit.AddEntity(row);
            }
        }

        private static ReportTableRow FillRow(string name, long rowId, ref int rowIdStart,
            params Dictionary<long, int>[] data)
        {
            var result = new ReportTableRow
            {
                RowData = new List<ReportRowData>(),
                Id = DistrictReportId + rowId,
                TableId = DistrictReportId,
                SortOrder = Convert.ToInt32(rowId)
            };
            result.RowData.Add(new ReportRowData
            {
                Value = name,
                ReportTableHeadId = DistrictReportId + 1,
                RowId = DistrictReportId + rowId,
                Id = rowIdStart++,
                SortOrder = 1,
                CssClass = "table-statistic-first"
            });

            var headId = 4;

            foreach (var dict in data)
            {
                result.RowData.Add(new ReportRowData
                {
                    Value = (dict.ContainsKey(rowId) ? dict[rowId] : 0).FormatEx(),
                    ReportTableHeadId = DistrictReportId + headId,
                    RowId = DistrictReportId + rowId,
                    Id = rowIdStart++,
                    SortOrder = headId
                });
                headId++;
            }

            return result;
        }

        private static Dictionary<long?, int> GetCollectedDict(Dictionary<long?, int> mpguAll,
            Dictionary<long?, int> mpguIndividual)
        {
            mpguAll = mpguAll ?? new Dictionary<long?, int>();
            foreach (var key in mpguIndividual.Keys.Where(k => k.HasValue))
            {
                if (mpguAll.ContainsKey(key))
                {
                    mpguAll[key] = mpguAll[key] + mpguIndividual[key];
                }
                else
                {
                    mpguAll.Add(key, mpguIndividual[key]);
                }
            }

            return mpguAll;
        }

        private static ReportTableRow FillRow(string name, long rowId, Dictionary<long?, int> data,
            string classRow = null)
        {
            var headerId = 1;

            var statuses = new long?[]
            {
                1030,
                1050,
                1052,
                1055,
                1080,
                1090,
                1075
            };

            var rtrId = SecondReportId + rowId;

            var result = new ReportTableRow
            {
                RowData = new List<ReportRowData>(),
                Id = rtrId,
                TableId = SecondReportId,
                SortOrder = Convert.ToInt32(rowId),
                CssClass = classRow
            };

            result.RowData.Add(new ReportRowData
            {
                Value = name,
                ReportTableHeadId = SecondReportId + headerId,
                RowId = SecondReportId + rowId,
                Id = SecondReportId + rowId * 10 + headerId,
                SortOrder = headerId,
                CssClass = "table-statistic-first"
            });
            headerId++;

            result.RowData.Add(new ReportRowData
            {
                Value = data.Where(ss => statuses.Contains(ss.Key)).Sum(ss => ss.Value).FormatEx(),
                ReportTableHeadId = SecondReportId + headerId,
                RowId = SecondReportId + rowId,
                Id = SecondReportId + rowId * 10 + headerId,
                SortOrder = headerId
            });
            headerId++;

            result.RowData.Add(new ReportRowData
            {
                Value = (data.ContainsKey(1030) ? data[1030] : 0).FormatEx(),
                ReportTableHeadId = SecondReportId + headerId,
                RowId = SecondReportId + rowId,
                Id = SecondReportId + rowId * 10 + headerId,
                SortOrder = headerId
            });
            headerId++;

            result.RowData.Add(new ReportRowData
            {
                Value = (data.ContainsKey(1050) ? data[1050] : 0).FormatEx(),
                ReportTableHeadId = SecondReportId + headerId,
                RowId = SecondReportId + rowId,
                Id = SecondReportId + rowId * 10 + headerId,
                SortOrder = headerId
            });
            headerId++;

            result.RowData.Add(new ReportRowData
            {
                Value = (data.ContainsKey(1052) ? data[1052] : 0).FormatEx(),
                ReportTableHeadId = SecondReportId + headerId,
                RowId = SecondReportId + rowId,
                Id = SecondReportId + rowId * 10 + headerId,
                SortOrder = headerId
            });
            headerId++;

            result.RowData.Add(new ReportRowData
            {
                Value = (data.ContainsKey(1055) ? data[1055] : 0).FormatEx(),
                ReportTableHeadId = SecondReportId + headerId,
                RowId = SecondReportId + rowId,
                Id = SecondReportId + rowId * 10 + headerId,
                SortOrder = headerId
            });
            headerId++;

            result.RowData.Add(new ReportRowData
            {
                Value = (data.ContainsKey(1080) ? data[1080] : 0).FormatEx(),
                ReportTableHeadId = SecondReportId + headerId,
                RowId = SecondReportId + rowId,
                Id = SecondReportId + rowId * 10 + headerId,
                SortOrder = headerId
            });
            headerId++;

            result.RowData.Add(new ReportRowData
            {
                Value = (data.ContainsKey(1090) ? data[1090] : 0).FormatEx(),
                ReportTableHeadId = SecondReportId + headerId,
                RowId = SecondReportId + rowId,
                Id = SecondReportId + rowId * 10 + headerId,
                SortOrder = headerId
            });
            headerId++;

            result.RowData.Add(new ReportRowData
            {
                Value = (data.ContainsKey(1075) ? data[1075] : 0).FormatEx(),
                ReportTableHeadId = SecondReportId + headerId,
                RowId = SecondReportId + rowId,
                Id = SecondReportId + rowId * 10 + headerId,
                SortOrder = headerId
            });

            return result;
        }

        private static void FillFirstReport()
        {
            const string green = "C5FCC2";
            const string red = "FCC2C2";
            // ReSharper disable once StringLiteralTypo
            const string yellow = "FCFBC2";

            var yearOfCompany = ConfigurationManager.AppSettings["YearMultiCompany"].IntParse() ?? DateTime.Today.Year;

            using (var unit = new UnitOfWork())
            {
                var queue =
                    unit.GetSet<Request>()
                        .Where(r => r.IsLast && !r.IsDeleted && r.StatusId != (long) StatusEnum.Draft &&
                                    r.SourceId.HasValue && r.YearOfRest.Year >= yearOfCompany &&
                                    !r.TypeOfRest.Commercial);
                var queueChild =
                    unit.GetSet<Child>()
                        .Where(
                            c =>
                                c.Request.IsLast && !c.Request.IsDeleted &&
                                c.Request.StatusId != (long) StatusEnum.Draft &&
                                c.Request.SourceId.HasValue && c.Request.YearOfRest.Year >= yearOfCompany &&
                                !c.Request.TypeOfRest.Commercial && !c.IsDeleted);
                var attendants =
                    unit.GetSet<Applicant>()
                        .Where(
                            c =>
                                c.Request.IsLast && !c.Request.IsDeleted && c.IsAccomp &&
                                c.Request.StatusId != (long) StatusEnum.Draft &&
                                c.Request.SourceId.HasValue && c.Request.YearOfRest.Year >= yearOfCompany &&
                                !c.Request.TypeOfRest.Commercial);
                var applicant =
                    unit.GetSet<Request>()
                        .Where(
                            c =>
                                c.IsLast && !c.IsDeleted && c.Applicant.IsAccomp &&
                                c.StatusId != (long) StatusEnum.Draft &&
                                c.SourceId.HasValue && c.YearOfRest.Year >= yearOfCompany && !c.TypeOfRest.Commercial);

                var checkDate = DateTime.Now.AddDays(-1);
                var checkHour = DateTime.Now.AddHours(-1);
                var checkWeek = DateTime.Now.AddDays(-7);
                var res = new List<AnalyticsViewRow>();

                var rowAnalytics = new AnalyticsViewRow
                {
                    Id = 1,
                    AnalyticsViewRowTypeId = (long) AnalyticsViewRowTypeEnum.EightColumns,
                    Name = "Поступило заявлений/отдыхающих",
                    ByDay = queue.Count(q => q.DateRequest >= checkDate),
                    ByHour = queue.Count(q => q.DateRequest >= checkHour),
                    ByWeek = queue.Count(q => q.DateRequest >= checkWeek),
                    Total = queue.Count(),
                    ByDay2 =
                        queueChild.Count(q => q.Request.DateRequest >= checkDate) +
                        attendants.Count(q => q.Request.DateRequest >= checkDate) +
                        applicant.Count(q => q.DateRequest >= checkDate),
                    ByHour2 =
                        queueChild.Count(q => q.Request.DateRequest >= checkHour) +
                        attendants.Count(q => q.Request.DateRequest >= checkHour) +
                        applicant.Count(q => q.DateRequest >= checkHour),
                    ByWeek2 =
                        queueChild.Count(q => q.Request.DateRequest >= checkWeek) +
                        attendants.Count(q => q.Request.DateRequest >= checkWeek) +
                        applicant.Count(q => q.DateRequest >= checkWeek),
                    Total2 = queueChild.Count() + attendants.Count() + applicant.Count()
                };

                rowAnalytics.ByDayColor = rowAnalytics.ByDay > 0 ? green : red;
                rowAnalytics.ByHourColor = rowAnalytics.ByHour > 0 ? green : red;
                rowAnalytics.ByWeekColor = rowAnalytics.ByWeek > 0 ? green : red;
                rowAnalytics.TotalColor = rowAnalytics.Total > 0 ? green : red;
                res.Add(rowAnalytics);

                var requestsByHour =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.MainStatistics.Requests.ByHour);
                var requestsByDay =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.MainStatistics.Requests.ByDay);
                var requestsByWeek =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.MainStatistics.Requests.ByWeek);
                var requestsAll =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.MainStatistics.Requests.All);

                if (requestsByHour != null)
                {
                    requestsByHour.Value = $"{rowAnalytics.ByHour.FormatEx("0")}/{rowAnalytics.ByHour2.FormatEx("0")}";
                    requestsByHour.Style = $"background-color:#{rowAnalytics.ByHourColor}";
                }

                if (requestsByDay != null)
                {
                    requestsByDay.Value = $"{rowAnalytics.ByDay.FormatEx("0")}/{rowAnalytics.ByDay2.FormatEx("0")}";
                    requestsByDay.Style = $"background-color:#{rowAnalytics.ByDayColor}";
                }

                if (requestsByWeek != null)
                {
                    requestsByWeek.Value = $"{rowAnalytics.ByWeek.FormatEx("0")}/{rowAnalytics.ByWeek2.FormatEx("0")}";
                    requestsByWeek.Style = $"background-color:#{rowAnalytics.ByWeekColor}";
                }

                if (requestsAll != null)
                {
                    requestsAll.Value = $"{rowAnalytics.Total.FormatEx("0")}/{rowAnalytics.Total2.FormatEx("0")}";
                    requestsAll.Style = $"background-color:#{rowAnalytics.TotalColor}";
                }

                var queueInc = queue.Where(r => r.StatusId == (long) StatusEnum.CertificateIssued);
                var queueChildInc = queueChild.Where(c => c.Request.StatusId == (long) StatusEnum.CertificateIssued);
                var attendantsInc = attendants.Where(c => c.Request.StatusId == (long) StatusEnum.CertificateIssued);
                var applicantInc = applicant.Where(r => r.StatusId == (long) StatusEnum.CertificateIssued);

                rowAnalytics = new AnalyticsViewRow
                {
                    Id = 2,
                    AnalyticsViewRowTypeId = (long) AnalyticsViewRowTypeEnum.EightColumns,
                    Name = "Выдано путевок",
                    ByDay = queueInc.Count(q => q.DateChangeStatus >= checkDate),
                    ByHour = queueInc.Count(q => q.DateChangeStatus >= checkHour),
                    ByWeek = queueInc.Count(q => q.DateChangeStatus >= checkWeek),
                    Total = queueInc.Count(),
                    ByDay2 =
                        queueChildInc.Count(q => q.Request.DateChangeStatus >= checkDate) +
                        attendantsInc.Count(q => q.Request.DateChangeStatus >= checkDate) +
                        applicantInc.Count(q => q.DateChangeStatus >= checkDate),
                    ByHour2 =
                        queueChildInc.Count(q => q.Request.DateChangeStatus >= checkHour) +
                        attendantsInc.Count(q => q.Request.DateChangeStatus >= checkHour) +
                        applicantInc.Count(q => q.DateChangeStatus >= checkHour),
                    ByWeek2 =
                        queueChildInc.Count(q => q.Request.DateChangeStatus >= checkWeek) +
                        attendantsInc.Count(q => q.Request.DateChangeStatus >= checkWeek) +
                        applicantInc.Count(q => q.DateChangeStatus >= checkWeek),
                    Total2 = queueChildInc.Count() + attendantsInc.Count() + applicantInc.Count()
                };

                rowAnalytics.ByDayColor = rowAnalytics.ByDay > 0 ? green : yellow;
                rowAnalytics.ByHourColor = rowAnalytics.ByHour > 0 ? green : yellow;
                rowAnalytics.ByWeekColor = rowAnalytics.ByWeek > 0 ? green : yellow;
                rowAnalytics.TotalColor = rowAnalytics.Total > 0 ? green : yellow;
                res.Add(rowAnalytics);

                var certificatesByHour =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.MainStatistics.Sertificates.ByHour);
                var certificatesByDay =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.MainStatistics.Sertificates.ByDay);
                var certificatesByWeek =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.MainStatistics.Sertificates.ByWeek);
                var certificatesAll =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.MainStatistics.Sertificates.All);

                if (certificatesByHour != null)
                {
                    certificatesByHour.Value =
                        $"{rowAnalytics.ByHour.FormatEx("0")}/{rowAnalytics.ByHour2.FormatEx("0")}";
                    certificatesByHour.Style = $"background-color:#{rowAnalytics.ByHourColor}";
                }

                if (certificatesByDay != null)
                {
                    certificatesByDay.Value = $"{rowAnalytics.ByDay.FormatEx("0")}/{rowAnalytics.ByDay2.FormatEx("0")}";
                    certificatesByDay.Style = $"background-color:#{rowAnalytics.ByDayColor}";
                }

                if (certificatesByWeek != null)
                {
                    certificatesByWeek.Value =
                        $"{rowAnalytics.ByWeek.FormatEx("0")}/{rowAnalytics.ByWeek2.FormatEx("0")}";
                    certificatesByWeek.Style = $"background-color:#{rowAnalytics.ByWeekColor}";
                }

                if (certificatesAll != null)
                {
                    certificatesAll.Value = $"{rowAnalytics.Total.FormatEx("0")}/{rowAnalytics.Total2.FormatEx("0")}";
                    certificatesAll.Style = $"background-color:#{rowAnalytics.TotalColor}";
                }

                var qWait = queue.Where(q => q.StatusId == (long) StatusEnum.WaitApplicant);

                rowAnalytics = new AnalyticsViewRow
                {
                    Id = 3,
                    AnalyticsViewRowTypeId = (long) AnalyticsViewRowTypeEnum.FourColumns,
                    Name = "Ожидание прихода заявителя",
                    ByDay = qWait.Count(q => q.DateChangeStatus >= checkDate),
                    ByHour = qWait.Count(q => q.DateChangeStatus >= checkHour),
                    ByWeek = qWait.Count(q => q.DateChangeStatus >= checkWeek),
                    Total = qWait.Count()
                };

                rowAnalytics.ByDayColor = rowAnalytics.ByDay == 0 ? green : yellow;
                rowAnalytics.ByHourColor = rowAnalytics.ByHour == 0 ? green : yellow;
                rowAnalytics.ByWeekColor = rowAnalytics.ByWeek == 0 ? green : yellow;
                rowAnalytics.TotalColor = rowAnalytics.Total == 0 ? green : yellow;
                res.Add(rowAnalytics);

                var applicantAwaitByHour =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.MainStatistics.ApplicantAwait.ByHour);
                var applicantAwaitByDay =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.MainStatistics.ApplicantAwait.ByDay);
                var applicantAwaitByWeek =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.MainStatistics.ApplicantAwait.ByWeek);
                var applicantAwaitAll =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.MainStatistics.ApplicantAwait.All);

                if (applicantAwaitByHour != null)
                {
                    applicantAwaitByHour.Value = rowAnalytics.ByHour.FormatEx("0");
                    applicantAwaitByHour.Style = $"background-color:#{rowAnalytics.ByHourColor}";
                }

                if (applicantAwaitByDay != null)
                {
                    applicantAwaitByDay.Value = rowAnalytics.ByDay.FormatEx("0");
                    applicantAwaitByDay.Style = $"background-color:#{rowAnalytics.ByDayColor}";
                }

                if (applicantAwaitByWeek != null)
                {
                    applicantAwaitByWeek.Value = rowAnalytics.ByWeek.FormatEx("0");
                    applicantAwaitByWeek.Style = $"background-color:#{rowAnalytics.ByWeekColor}";
                }

                if (applicantAwaitAll != null)
                {
                    applicantAwaitAll.Value = rowAnalytics.Total.FormatEx("0");
                    applicantAwaitAll.Style = $"background-color:#{rowAnalytics.TotalColor}";
                }

                var qReject = queue.Where(q => q.StatusId == (long) StatusEnum.Reject);

                rowAnalytics = new AnalyticsViewRow
                {
                    Id = 4,
                    AnalyticsViewRowTypeId = (long) AnalyticsViewRowTypeEnum.FourColumns,
                    Name = "Отказ в предоставлении услуги",
                    ByDay = qReject.Count(q => q.DateChangeStatus >= checkDate),
                    ByHour = qReject.Count(q => q.DateChangeStatus >= checkHour),
                    ByWeek = qReject.Count(q => q.DateChangeStatus >= checkWeek),
                    Total = qReject.Count()
                };

                res.Add(rowAnalytics);

                var serviceDeniedByHour =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceDenied.ByHour);
                var serviceDeniedByDay =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceDenied.ByDay);
                var serviceDeniedByWeek =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceDenied.ByWeek);
                var serviceDeniedAll =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceDenied.All);

                if (serviceDeniedByHour != null)
                {
                    serviceDeniedByHour.Value = rowAnalytics.ByHour.FormatEx("0");
                }

                if (serviceDeniedByDay != null)
                {
                    serviceDeniedByDay.Value = rowAnalytics.ByDay.FormatEx("0");
                }

                if (serviceDeniedByWeek != null)
                {
                    serviceDeniedByWeek.Value = rowAnalytics.ByWeek.FormatEx("0");
                }

                if (serviceDeniedAll != null)
                {
                    serviceDeniedAll.Value = rowAnalytics.Total.FormatEx("0");
                }

                var qRegistrationDecline =
                    queue.Where(q => q.StatusId == (long) StatusEnum.RegistrationDecline);

                rowAnalytics = new AnalyticsViewRow
                {
                    Id = 5,
                    AnalyticsViewRowTypeId = (long) AnalyticsViewRowTypeEnum.FourColumns,
                    Name = "Отказ в регистрации",
                    ByDay = qRegistrationDecline.Count(q => q.DateChangeStatus >= checkDate),
                    ByHour = qRegistrationDecline.Count(q => q.DateChangeStatus >= checkHour),
                    ByWeek = qRegistrationDecline.Count(q => q.DateChangeStatus >= checkWeek),
                    Total = qRegistrationDecline.Count()
                };

                res.Add(rowAnalytics);

                var serviceRegistrationDeclineByHour =
                    unit.GetById<ReportRowData>(
                        ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceRegisterDenied.ByHour);
                var serviceRegistrationDeclineByDay =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceRegisterDenied
                        .ByDay);
                var serviceRegistrationDeclineByWeek =
                    unit.GetById<ReportRowData>(
                        ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceRegisterDenied.ByWeek);
                var serviceRegistrationDeclineAll =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceRegisterDenied
                        .All);

                if (serviceRegistrationDeclineByHour != null)
                {
                    serviceRegistrationDeclineByHour.Value = rowAnalytics.ByHour.FormatEx("0");
                }

                if (serviceRegistrationDeclineByDay != null)
                {
                    serviceRegistrationDeclineByDay.Value = rowAnalytics.ByDay.FormatEx("0");
                }

                if (serviceRegistrationDeclineByWeek != null)
                {
                    serviceRegistrationDeclineByWeek.Value = rowAnalytics.ByWeek.FormatEx("0");
                }

                if (serviceRegistrationDeclineAll != null)
                {
                    serviceRegistrationDeclineAll.Value = rowAnalytics.Total.FormatEx("0");
                }

                var br = unit.GetSet<RestChild.Domain.ExchangeBaseRegistry>().Where(r => !r.ResponseDate.HasValue);

                var curYearDate = new DateTime(DateTime.Now.Year, 1, 1);

                rowAnalytics = new AnalyticsViewRow
                {
                    Id = 6,
                    AnalyticsViewRowTypeId = (long) AnalyticsViewRowTypeEnum.FourColumns,
                    Name = "Ожидает ответа из Базового регистра",
                    ByDay = br.Count(q => q.SendDate >= checkDate),
                    ByHour = br.Count(q => q.SendDate >= checkHour),
                    ByWeek = br.Count(q => q.SendDate >= checkWeek),
                    Total = br.Count(q => q.SendDate >= curYearDate)
                };

                rowAnalytics.ByDayColor = rowAnalytics.ByDay == 0 ? green : string.Empty;
                rowAnalytics.ByHourColor = rowAnalytics.ByHour == 0 ? green : string.Empty;
                rowAnalytics.ByWeekColor = rowAnalytics.ByWeek == 0 ? green : string.Empty;
                rowAnalytics.TotalColor = rowAnalytics.Total == 0 ? green : string.Empty;
                res.Add(rowAnalytics);

                var baseRegisterAwaitByHour =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterAwait
                        .ByHour);
                var baseRegisterAwaitByDay =
                    unit.GetById<ReportRowData>(
                        ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterAwait.ByDay);
                var baseRegisterAwaitByWeek =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterAwait
                        .ByWeek);
                var baseRegisterAwaitAll =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterAwait.All);

                if (baseRegisterAwaitByHour != null)
                {
                    baseRegisterAwaitByHour.Value = rowAnalytics.ByHour.FormatEx("0");
                    baseRegisterAwaitByHour.Style = $"background-color:#{rowAnalytics.ByHourColor}";
                }

                if (baseRegisterAwaitByDay != null)
                {
                    baseRegisterAwaitByDay.Value = rowAnalytics.ByDay.FormatEx("0");
                    baseRegisterAwaitByDay.Style = $"background-color:#{rowAnalytics.ByDayColor}";
                }

                if (baseRegisterAwaitByWeek != null)
                {
                    baseRegisterAwaitByWeek.Value = rowAnalytics.ByWeek.FormatEx("0");
                    baseRegisterAwaitByWeek.Style = $"background-color:#{rowAnalytics.ByWeekColor}";
                }

                if (baseRegisterAwaitAll != null)
                {
                    baseRegisterAwaitAll.Value = rowAnalytics.Total.FormatEx("0");
                    baseRegisterAwaitAll.Style = $"background-color:#{rowAnalytics.TotalColor}";
                }

                br = unit.GetSet<RestChild.Domain.ExchangeBaseRegistry>().Where(r => r.ResponseDate.HasValue);

                var rowAnalyticsSecond = new AnalyticsViewRow
                {
                    Id = 7,
                    AnalyticsViewRowTypeId = (long) AnalyticsViewRowTypeEnum.FourColumns,
                    Name = "Получены ответы в Базовом регистре",
                    ByDay = br.Count(q => q.ResponseDate >= checkDate),
                    ByHour = br.Count(q => q.ResponseDate >= checkHour),
                    ByWeek = br.Count(q => q.ResponseDate >= checkWeek),
                    Total = br.Count(q => q.ResponseDate >= curYearDate)
                };

                rowAnalyticsSecond.ByDayColor =
                    rowAnalyticsSecond.ByDay == 0 && rowAnalytics.ByDay > 0 ? red : string.Empty;
                rowAnalyticsSecond.ByHourColor =
                    rowAnalyticsSecond.ByHour == 0 && rowAnalytics.ByHour > 0 ? red : string.Empty;
                rowAnalyticsSecond.ByWeekColor =
                    rowAnalyticsSecond.ByWeek == 0 && rowAnalytics.ByWeek > 0 ? red : string.Empty;
                rowAnalyticsSecond.TotalColor =
                    rowAnalyticsSecond.Total == 0 && rowAnalytics.Total > 0 ? red : string.Empty;
                res.Add(rowAnalyticsSecond);

                var baseRegisterResponseByHour =
                    unit.GetById<ReportRowData>(
                        ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterResponsed.ByHour);
                var baseRegisterResponseByDay =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterResponsed
                        .ByDay);
                var baseRegisterResponseByWeek =
                    unit.GetById<ReportRowData>(
                        ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterResponsed.ByWeek);
                var baseRegisterResponseAll =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterResponsed
                        .All);

                if (baseRegisterResponseByHour != null)
                {
                    baseRegisterResponseByHour.Value = rowAnalyticsSecond.ByHour.FormatEx("0");
                    baseRegisterResponseByHour.Style =
                        $"background-color:#{rowAnalyticsSecond.ByHourColor}";
                }

                if (baseRegisterResponseByDay != null)
                {
                    baseRegisterResponseByDay.Value = rowAnalyticsSecond.ByDay.FormatEx("0");
                    baseRegisterResponseByDay.Style =
                        $"background-color:#{rowAnalyticsSecond.ByDayColor}";
                }

                if (baseRegisterResponseByWeek != null)
                {
                    baseRegisterResponseByWeek.Value = rowAnalyticsSecond.ByWeek.FormatEx("0");
                    baseRegisterResponseByWeek.Style =
                        $"background-color:#{rowAnalyticsSecond.ByWeekColor}";
                }

                if (baseRegisterResponseAll != null)
                {
                    baseRegisterResponseAll.Value = rowAnalyticsSecond.Total.FormatEx("0");
                    baseRegisterResponseAll.Style =
                        $"background-color:#{rowAnalyticsSecond.TotalColor}";
                }

                var etp = unit.GetSet<ExchangeUTS>().Where(e => e.IsError && e.Incoming);

                rowAnalyticsSecond = new AnalyticsViewRow
                {
                    Id = 8,
                    AnalyticsViewRowTypeId = (long) AnalyticsViewRowTypeEnum.FourColumns,
                    Name = "Количество некорректных сообщений от МПГУ",
                    ByDay = etp.Count(q => q.DateCreate >= checkDate),
                    ByHour = etp.Count(q => q.DateCreate >= checkHour),
                    ByWeek = etp.Count(q => q.DateCreate >= checkWeek),
                    Total = etp.Count(q => q.DateCreate >= curYearDate)
                };

                rowAnalyticsSecond.ByDayColor =
                    rowAnalyticsSecond.ByDay == 0 && rowAnalytics.ByDay > 0 ? red : string.Empty;
                rowAnalyticsSecond.ByHourColor =
                    rowAnalyticsSecond.ByHour == 0 && rowAnalytics.ByHour > 0 ? red : string.Empty;
                rowAnalyticsSecond.ByWeekColor =
                    rowAnalyticsSecond.ByWeek == 0 && rowAnalytics.ByWeek > 0 ? red : string.Empty;
                rowAnalyticsSecond.TotalColor =
                    rowAnalyticsSecond.Total == 0 && rowAnalytics.Total > 0 ? red : string.Empty;
                res.Add(rowAnalyticsSecond);

                var mpguErrorMessagesByHour =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.MainStatistics.MpguErrorMessages
                        .ByHour);
                var mpguErrorMessagesByDay =
                    unit.GetById<ReportRowData>(
                        ReportEnum.Rows.ServiceStatistics.MainStatistics.MpguErrorMessages.ByDay);
                var mpguErrorMessagesByWeek =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.MainStatistics.MpguErrorMessages
                        .ByWeek);
                var mpguErrorMessagesAll =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.MainStatistics.MpguErrorMessages.All);

                if (mpguErrorMessagesByHour != null)
                {
                    mpguErrorMessagesByHour.Value = rowAnalyticsSecond.ByHour.FormatEx("0");
                    mpguErrorMessagesByHour.Style =
                        $"background-color:#{rowAnalyticsSecond.ByHourColor}";
                }

                if (mpguErrorMessagesByDay != null)
                {
                    mpguErrorMessagesByDay.Value = rowAnalyticsSecond.ByDay.FormatEx("0");
                    mpguErrorMessagesByDay.Style = $"background-color:#{rowAnalyticsSecond.ByDayColor}";
                }

                if (mpguErrorMessagesByWeek != null)
                {
                    mpguErrorMessagesByWeek.Value = rowAnalyticsSecond.ByWeek.FormatEx("0");
                    mpguErrorMessagesByWeek.Style =
                        $"background-color:#{rowAnalyticsSecond.ByWeekColor}";
                }

                if (mpguErrorMessagesAll != null)
                {
                    mpguErrorMessagesAll.Value = rowAnalyticsSecond.Total.FormatEx("0");
                    mpguErrorMessagesAll.Style = $"background-color:#{rowAnalyticsSecond.TotalColor}";
                }

                var requestInterdepartmental = unit.GetSet<Request>()
                    .Where(
                        r =>
                            r.IsLast && !r.IsDeleted && r.SourceId.HasValue &&
                            r.StatusId == (long) StatusEnum.ApplicantCome &&
                            !r.TypeOfRest.Commercial)
                    .Where(r => r.Child.Any(c => c.IsIncludeInInteragency || c.IsIncludeInInteragencySecondary));

                var children =
                    requestInterdepartmental.SelectMany(c => c.Child)
                        .Where(c => c.IsIncludeInInteragency || c.IsIncludeInInteragencySecondary);

                var rowOneColumn = new AnalyticsViewRow
                {
                    Id = 9,
                    AnalyticsViewRowTypeId = (long) AnalyticsViewRowTypeEnum.TwoColumn,
                    Name = "Ожидание ответов по межведомственным запросам - заявлений/детей",
                    Total = requestInterdepartmental.Count(),
                    Total2 = children.Count()
                };

                var reportColumn =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.InteragencyAwait.Row1.Column1);
                if (reportColumn != null)
                {
                    reportColumn.Value = $"{rowOneColumn.Name}: {rowOneColumn.Total}/{rowOneColumn.Total2}";
                }

                res.Add(rowOneColumn);

                queue =
                    unit.GetSet<Request>()
                        .Where(r => r.IsLast && !r.IsDeleted && r.StatusId == (long) StatusEnum.WaitApplicant &&
                                    r.SourceId.HasValue && !r.TypeOfRest.Commercial);

                var daysWork1 = unit.GetNextWorkDay(DateTime.Now.Date);
                var daysWork2 = unit.AddWorkDays(daysWork1, 1);
                var daysWork3 = unit.AddWorkDays(daysWork2, 1);
                var daysWork4 = unit.AddWorkDays(daysWork3, 1);
                var daysWork5 = unit.AddWorkDays(daysWork4, 1);
                var daysWork6 = unit.AddWorkDays(daysWork5, 1);
                var daysWork1Check = unit.AddWorkDays(daysWork1,
                    -(ConfigurationManager.AppSettings["CountDaysForReject"].IntParse() ?? 0));
                var daysWork2Check = unit.AddWorkDays(daysWork2,
                    -(ConfigurationManager.AppSettings["CountDaysForReject"].IntParse() ?? 0));
                var daysWork3Check = unit.AddWorkDays(daysWork3,
                    -(ConfigurationManager.AppSettings["CountDaysForReject"].IntParse() ?? 0));
                var daysWork4Check = unit.AddWorkDays(daysWork4,
                    -(ConfigurationManager.AppSettings["CountDaysForReject"].IntParse() ?? 0));
                var daysWork5Check = unit.AddWorkDays(daysWork5,
                    -(ConfigurationManager.AppSettings["CountDaysForReject"].IntParse() ?? 0));
                var daysWork6Check = unit.AddWorkDays(daysWork6,
                    -(ConfigurationManager.AppSettings["CountDaysForReject"].IntParse() ?? 0));

                rowOneColumn = new AnalyticsViewRow
                {
                    Id = 10,
                    AnalyticsViewRowTypeId = (long) AnalyticsViewRowTypeEnum.ByDays,
                    Name = "Ожидание прихода заявителя",
                    Total = queue.Count(),
                    DataDay1 = daysWork1,
                    DataDay2 = daysWork2,
                    DataDay3 = daysWork3,
                    DataDay4 = daysWork4,
                    DataDay5 = daysWork5,
                    Day1 = queue.Count(r =>
                        r.DateChangeStatus >= daysWork1Check && r.DateChangeStatus < daysWork2Check),
                    Day2 = queue.Count(r =>
                        r.DateChangeStatus >= daysWork2Check && r.DateChangeStatus < daysWork3Check),
                    Day3 = queue.Count(r =>
                        r.DateChangeStatus >= daysWork3Check && r.DateChangeStatus < daysWork4Check),
                    Day4 = queue.Count(r =>
                        r.DateChangeStatus >= daysWork4Check && r.DateChangeStatus < daysWork5Check),
                    Day5 = queue.Count(r => r.DateChangeStatus >= daysWork5Check && r.DateChangeStatus < daysWork6Check)
                };

                res.Add(rowOneColumn);


                var day1Header =
                    unit.GetById<ReportTableHead>(ReportEnum.Heads.ServiceStatistics.ApplicantAwaitReport.Column1);
                var day2Header =
                    unit.GetById<ReportTableHead>(ReportEnum.Heads.ServiceStatistics.ApplicantAwaitReport.Column2);
                var day3Header =
                    unit.GetById<ReportTableHead>(ReportEnum.Heads.ServiceStatistics.ApplicantAwaitReport.Column3);
                var day4Header =
                    unit.GetById<ReportTableHead>(ReportEnum.Heads.ServiceStatistics.ApplicantAwaitReport.Column4);
                var day5Header =
                    unit.GetById<ReportTableHead>(ReportEnum.Heads.ServiceStatistics.ApplicantAwaitReport.Column5);

                var day1 = unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.ApplicantAwaitReport.Row1
                    .Column1);
                var day2 = unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.ApplicantAwaitReport.Row1
                    .Column2);
                var day3 = unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.ApplicantAwaitReport.Row1
                    .Column3);
                var day4 = unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.ApplicantAwaitReport.Row1
                    .Column4);
                var day5 = unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.ApplicantAwaitReport.Row1
                    .Column5);

                if (day1Header != null)
                {
                    day1Header.Name = rowOneColumn.DataDay1.FormatEx();
                }

                if (day2Header != null)
                {
                    day2Header.Name = rowOneColumn.DataDay2.FormatEx();
                }

                if (day3Header != null)
                {
                    day3Header.Name = rowOneColumn.DataDay3.FormatEx();
                }

                if (day4Header != null)
                {
                    day4Header.Name = rowOneColumn.DataDay4.FormatEx();
                }

                if (day5Header != null)
                {
                    day5Header.Name = rowOneColumn.DataDay5.FormatEx();
                }

                if (day1 != null)
                {
                    day1.Value = rowOneColumn.Day1.FormatEx();
                }

                if (day2 != null)
                {
                    day2.Value = rowOneColumn.Day2.FormatEx();
                }

                if (day3 != null)
                {
                    day3.Value = rowOneColumn.Day3.FormatEx();
                }

                if (day4 != null)
                {
                    day4.Value = rowOneColumn.Day4.FormatEx();
                }

                if (day5 != null)
                {
                    day5.Value = rowOneColumn.Day5.FormatEx();
                }

                var bookingWithoutRequests = unit.GetSet<Booking>().Count(b => !b.Canceled && !b.RequestId.HasValue);

                rowOneColumn = new AnalyticsViewRow
                {
                    Id = 11,
                    AnalyticsViewRowTypeId = (long) AnalyticsViewRowTypeEnum.OneColumn,
                    Name = "Количество бронирований по которым нет заявлений",
                    Total = bookingWithoutRequests
                };

                var bookingsWithoutRequestsReportColumn =
                    unit.GetById<ReportRowData>(ReportEnum.Rows.ServiceStatistics.BookingsWithoutRequests.Row1.Column1);
                if (bookingsWithoutRequestsReportColumn != null)
                {
                    bookingsWithoutRequestsReportColumn.Value =
                        $"{rowOneColumn.Name}: {rowOneColumn.Total}";
                }

                res.Add(rowOneColumn);

                foreach (var row in unit.GetSet<AnalyticsViewRow>().ToList())
                {
                    unit.Delete(row);
                }

                foreach (var item in res)
                {
                    unit.GetSet<AnalyticsViewRow>().Add(item);
                }

                unit.SaveChanges();
            }
        }
    }
}
