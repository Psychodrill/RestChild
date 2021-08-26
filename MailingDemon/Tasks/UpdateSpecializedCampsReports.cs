using System;
using System.Collections.Generic;
using System.Linq;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL;
using RestChild.Domain;
using RestChild.Extensions.Models;

namespace MailingDemon.Tasks
{
   /// <summary>
   ///    Генерация отчета по профильникам
   /// </summary>
   [Task]
   public class UpdateSpecializedCampsReports : BaseTask
   {
      private const int ReportId = 20000;

      protected override void Execute()
      {
         using (var unitOfWork = new UnitOfWork())
         {
            var timesOfRest =
               unitOfWork.GetSet<TimeOfRest>()
                  .Where(t => t.TypeOfRestId == (long) TypeOfRestEnum.SpecializedСamp &&
                              t.YearOfRest.Year >= DateTime.Today.Year)
                  .OrderBy(t => t.Id)
                  .ToList();
            var organisations = GetOrganizations(unitOfWork);
            var oivs = organisations.Where(o => o.IsVedomstvo).ToList();
            var reportSheet = CreateReportSheet(timesOfRest, oivs);
            var organizationNum = 1;
            var rowItemInum = 1;
            var tableNum = 1;

            CreateReport(timesOfRest, unitOfWork,
               organisations.OrderBy(o => (o.ParentId ?? o.Id) * 10000 + (o.IsVedomstvo ? 0 : 1)).ToList(),
               reportSheet.ReportTables.First(), ref organizationNum, ref rowItemInum, true);

            foreach (var organization in oivs)
            {
               CreateReport(timesOfRest, unitOfWork,
                  organisations.Where(o => o.ParentId == organization.Id || o.Id == organization.Id)
                     .OrderBy(o => (o.ParentId ?? o.Id) * 10000 + (o.IsVedomstvo ? 0 : 1)).ToList(),
                  reportSheet.ReportTables.ElementAt(tableNum), ref organizationNum, ref rowItemInum, false);
               tableNum++;
            }

            SaveReport(unitOfWork, reportSheet);
         }
      }

      private void CreateReport(
         List<TimeOfRest> timesOfRest,
         IUnitOfWork unitOfWork,
         List<Organization> oivs,
         ReportTable reportTable,
         ref int organizationNum,
         ref int rowItemInum,
         bool forOivReport)
      {
         var reports = new List<SpecializedCampsCampersReport>();
         foreach (var timeOfRest in timesOfRest)
         {
            var result = LoadData(unitOfWork, timeOfRest, oivs.Select(o => o.Id).ToList());
            reports.AddRange(result);
         }

         var oivCopy = oivs.Select(o => new Organization(o)).ToDictionary(o => o.Id);
         var sum = reports.Where(r => r.Organisation.IsVedomstvo).Select(r => new SpecializedCampsCampersReport
         {
            VolumeAdded = r.VolumeAdded,
            RestPlacesBrought = r.RestPlacesBrought,
            TimeOfRest = r.TimeOfRest,
            Organisation = oivCopy[r.Organisation.Id],
            Approved = reports
               .Where(re => re.TimeOfRest.Id == r.TimeOfRest.Id && r.Organisation.Id == re.Organisation.ParentId)
               .Select(re => re.Approved).Sum(),
            ChildsInserted = reports
               .Where(re => re.TimeOfRest.Id == r.TimeOfRest.Id && r.Organisation.Id == re.Organisation.ParentId)
               .Select(re => re.ChildsInserted).Sum(),
            ForAprove = reports
               .Where(re => re.TimeOfRest.Id == r.TimeOfRest.Id && r.Organisation.Id == re.Organisation.ParentId)
               .Select(re => re.ForAprove).Sum(),
            Paid = reports
               .Where(re => re.TimeOfRest.Id == r.TimeOfRest.Id && r.Organisation.Id == re.Organisation.ParentId)
               .Select(re => re.Paid).Sum(),
            VolumeBrought = reports
               .Where(re => re.TimeOfRest.Id == r.TimeOfRest.Id && r.Organisation.Id == re.Organisation.ParentId)
               .Select(re => re.VolumeBrought).Sum(),
            IsSum = true
         }).ToList();


         if (forOivReport)
         {
            reports = sum;
         }
         else
         {
            reports.AddRange(sum);
         }

         var reportsByOrganisations = reports.OrderByDescending(r => r.IsSum)
            .ThenByDescending(r => r.Organisation.IsVedomstvo).ThenBy(r => r.Organisation.Id)
            .GroupBy(r => r.Organisation);

         foreach (var report in reportsByOrganisations)
         {
            var organisation = report.Key;
            if (organisation == null)
            {
               continue;
            }

            var vedomstvo = organisation.Parent ?? organisation;
            var vedomstvoLimit = unitOfWork.GetSet<LimitOnVedomstvo>()
               .FirstOrDefault(l => l.OrganizationId == vedomstvo.Id);

            RenderReportItem(
               report.OrderBy(r => r.TimeOfRest.Id).ToList(),
               organisation,
               vedomstvoLimit?.Volume ?? 0,
               reportTable,
               ref organizationNum,
               ref rowItemInum,
               forOivReport);
         }
      }

      private List<Organization> GetOrganizations(IUnitOfWork unitOfWork)
      {
         var tours = unitOfWork.GetSet<Tour>()
            .Where(t => t.LimitOnVedomstvo.Organization.IsVedomstvo && t.Volumes.Any());
         var organisations =
            unitOfWork.GetSet<Organization>()
               .Where(
                  o =>
                     o.LimitOrganization.Any(
                        lo =>
                           lo.StateId != StateMachineStateEnum.Deleted &&
                           lo.StateId != StateMachineStateEnum.Limit.Organization.Formation)
                     || tours.Any(t => t.LimitOnVedomstvo.OrganizationId == o.Id)).ToList();

         return organisations;
      }

      private List<SpecializedCampsCampersReport> LoadData(IUnitOfWork unitOfWork, TimeOfRest timeOfRest,
         ICollection<long> organisationIds)
      {
         var toursAll = unitOfWork.GetSet<Tour>().Where(t => t.YearOfRest.Year >= DateTime.Today.Year);
         var listsAll = unitOfWork.GetSet<ListOfChilds>().Where(l => l.Tour.YearOfRest.Year >= DateTime.Today.Year);
         var limitsOnOrganisations = unitOfWork.GetSet<LimitOnOrganization>()
            .Where(l => l.Tour.YearOfRest.Year >= DateTime.Today.Year);
         var result =
            unitOfWork.GetSet<Organization>()
               .Where(o => organisationIds.Contains(o.Id))
               .Select(
                  o =>
                     new
                     {
                        Organisation = o,
                        Tours = toursAll.Where(t =>
                           t.TimeOfRestId == timeOfRest.Id && t.LimitOnVedomstvo.OrganizationId == o.Id),
                        Lists = listsAll.Where(l =>
                           l.LimitOnOrganization.OrganizationId == o.Id && l.TimeOfRestId == timeOfRest.Id)
                     })
               .Select(
                  o =>
                     new SpecializedCampsCampersReport
                     {
                        Organisation = o.Organisation,
                        VolumeAdded =
                           o.Tours.Where(
                                 t =>
                                    t.StateId == StateMachineStateEnum.Tour.Formed
                                    //|| t.StateId == StateMachineStateEnum.Tour.Paid
                                    || t.StateId == StateMachineStateEnum.Tour.ToFormed
                                    || t.StateId == StateMachineStateEnum.Tour.Formation)
                              .SelectMany(t => t.Volumes)
                              .Where(v => v.CountPlace.HasValue)
                              .Sum(v => (int?) v.CountPlace.Value) ?? 0,
                        RestPlacesBrought =
                           o.Tours.Where(
                                 t =>
                                    t.StateId == StateMachineStateEnum.Tour.Formed
                                    //|| t.StateId == StateMachineStateEnum.Tour.Paid
                                    || t.StateId == StateMachineStateEnum.Tour.ToFormed)
                              .SelectMany(t => t.Volumes)
                              .Where(v => v.CountPlace.HasValue)
                              .Sum(v => (int?) v.CountPlace.Value) ?? 0,
                        VolumeBrought =
                           limitsOnOrganisations.Where(
                              t =>
                                 t.StateId != StateMachineStateEnum.Deleted
                                 && t.StateId != StateMachineStateEnum.Limit.Organization.Formation
                                 && t.StateId.HasValue && t.OrganizationId == o.Organisation.Id
                                 && t.TimeOfRestId == timeOfRest.Id).Sum(v => (int?) v.Volume) ?? 0,
                        ChildsInserted = o.Lists.Where(l => l.StateId != StateMachineStateEnum.Deleted && !l.IsDeleted)
                                            .Sum(l => (int?) l.CountChild) ?? 0,
                        ForAprove =
                           o.Lists.Where(
                                 l =>
                                    l.LimitOnOrganization.StateId
                                    == StateMachineStateEnum.Limit.Organization.ToApprove
                                    && l.StateId != StateMachineStateEnum.Deleted && !l.IsDeleted)
                              .Sum(l => (int?) l.CountChild)
                           ?? 0,
                        Approved =
                           o.Lists.Where(
                                 l =>
                                    (l.LimitOnOrganization.StateId
                                     == StateMachineStateEnum.Limit.Organization.Approved
                                     || l.LimitOnOrganization.StateId ==
                                     StateMachineStateEnum.Limit.Organization.Confirmed)
                                    && l.StateId != StateMachineStateEnum.Deleted && !l.IsDeleted)
                              .Sum(l => (int?) l.CountChild)
                           ?? 0,
                        Paid =
                           o.Lists.Where(l => l.StateId != StateMachineStateEnum.Deleted && !l.IsDeleted)
                              .SelectMany(l => l.Childs).Count(c => !c.IsDeleted && c.Payed)
                     }).OrderBy(o =>
                  (o.Organisation.ParentId ?? o.Organisation.Id) * 10000 +
                  (o.Organisation.IsVedomstvo ? 0 : o.Organisation.Id)).ToList();

         foreach (var reportItem in result)
         {
            reportItem.TimeOfRest = timeOfRest;
         }

         return result;
      }

      private ReportSheet CreateReportSheet(ICollection<TimeOfRest> timesOfRest,
         ICollection<Organization> organizations)
      {
         var sheet = new ReportSheet
         {
            Id = ReportId,
            CodeAccess = new Guid(AccessRightEnum.Report.SpecializedCampsReport),
            ReportTables = new List<ReportTable>(),
            ReportName = "Отдыхающие в профильных лагерях",
            SortOrder = 20
         };

         var tableId = ReportId + 1;
         var headerId = ReportId;


         CreateTable(timesOfRest, sheet, null, ref headerId, ref tableId);
         foreach (var organization in organizations)
         {
            CreateTable(timesOfRest, sheet, organization, ref headerId, ref tableId);
         }

         return sheet;
      }

      private static void CreateTable(
         ICollection<TimeOfRest> timesOfRest,
         ReportSheet sheet,
         Organization organization,
         ref int headerId,
         ref int tableId)
      {
         var reportTable = new ReportTable
         {
            Id = tableId,
            ReportSheetId = sheet.Id,
            Rows = new List<ReportTableRow>(),
            SortOrder = organization != null ? -1 : 1,
            CssClass = "table table-bordered table-statistic specialized-camps-report",
            ReportTableHeads = new List<ReportTableHead>(),
            Name =
               organization != null
                  ? "specialized-camps-report-organisation-" + organization.Id
                  : null
         };

         sheet.ReportTables.Add(reportTable);

         reportTable.ReportTableHeads.AddRange(
            new List<ReportTableHead>
            {
               new ReportTableHead
               {
                  Id = headerId + 1,
                  SortOrder = 1,
                  Name = "Организация",
                  ReportTableId = tableId,
                  RowIndex = 1,
                  RowSpan = 2
               },
               new ReportTableHead
               {
                  Id = headerId + 2,
                  SortOrder = 2,
                  Name = "Размер квоты",
                  ReportTableId = tableId,
                  RowIndex = 1,
                  RowSpan = 2
               }
            });

         var reportTableHeadId = 3;
         headerId += 3;
         foreach (var timeOfRest in timesOfRest)
         {
            reportTable.ReportTableHeads.AddRange(
               new List<ReportTableHead>
               {
                  new ReportTableHead
                  {
                     Id = headerId,
                     SortOrder = reportTableHeadId,
                     Name = "Внесены квоты",
                     ReportTableId = tableId,
                     RowIndex = 2
                  },
                  new ReportTableHead
                  {
                     Id = headerId + 1,
                     SortOrder = reportTableHeadId,
                     Name = "Доведены МО",
                     ReportTableId = tableId,
                     RowIndex = 2
                  },
                  new ReportTableHead
                  {
                     Id = headerId + 2,
                     SortOrder = reportTableHeadId,
                     Name = "Доведены квоты",
                     ReportTableId = tableId,
                     RowIndex = 2
                  },
                  new ReportTableHead
                  {
                     Id = headerId + 3,
                     SortOrder = reportTableHeadId,
                     Name = "Введено детей",
                     ReportTableId = tableId,
                     RowIndex = 2
                  },
                  new ReportTableHead
                  {
                     Id = headerId + 4,
                     SortOrder = reportTableHeadId,
                     Name = "На согласовании",
                     ReportTableId = tableId,
                     RowIndex = 2
                  },
                  new ReportTableHead
                  {
                     Id = headerId + 5,
                     SortOrder = reportTableHeadId,
                     Name = "Согласовано",
                     ReportTableId = tableId,
                     RowIndex = 2
                  },
                  new ReportTableHead
                  {
                     Id = headerId + 6,
                     SortOrder = reportTableHeadId,
                     Name = "Оплачено",
                     ReportTableId = tableId,
                     RowIndex = 2
                  },
                  new ReportTableHead
                  {
                     Id = headerId + 7,
                     SortOrder = reportTableHeadId,
                     Name = timeOfRest.Name,
                     ReportTableId = tableId,
                     RowIndex = 1,
                     ColSpan = 7,
                     CssClass = "text-center"
                  }
               });
            reportTableHeadId += 8;
            headerId += 8;
         }

         tableId += 1;
      }

      private void RenderReportItem(
         ICollection<SpecializedCampsCampersReport> reportItem,
         Organization organization,
         int organisationVolume,
         ReportTable reportTable,
         ref int rowNum,
         ref int rowItemNum,
         bool forOivTable)
      {
         var row = new ReportTableRow
         {
            Id = ReportId + rowNum,
            Name = organization.Name,
            SortOrder = rowNum,
            RowData = new List<ReportRowData>(),
            TableId = reportTable.Id,
            CssClass = !forOivTable && reportItem.Any(i => i.IsSum) ? "specialized-camps-report-oiv" : string.Empty
         };

         row.RowData.AddRange(
            new List<ReportRowData>
            {
               new ReportRowData
               {
                  Id = ReportId + rowItemNum,
                  Value = forOivTable && reportItem.Any(i => i.IsSum)
                     ? string.Format(
                        "<a href=\"/Report/GetSpecializedCampsReportByOiv?oivId={0}\" target=\"_blank\">{1}</a>",
                        organization.Id, organization.Name)
                     : organization.Name,
                  SortOrder = 1,
                  RowId = ReportId + rowNum
               },
               new ReportRowData
               {
                  Id = ReportId + rowItemNum + 1,
                  Value = organization.IsVedomstvo ? organisationVolume.ToString() : string.Empty,
                  SortOrder = rowItemNum + 1,
                  RowId = ReportId + rowNum
               }
            });

         rowItemNum += 2;

         foreach (var item in reportItem)
         {
            row.RowData.AddRange(
               new List<ReportRowData>
               {
                  new ReportRowData
                  {
                     Id = ReportId + rowItemNum,
                     Value = item.VolumeAdded.ToString(),
                     SortOrder = rowItemNum,
                     RowId = ReportId + rowNum
                  },
                  new ReportRowData
                  {
                     Id = ReportId + rowItemNum + 1,
                     Value = item.RestPlacesBrought.ToString(),
                     SortOrder = rowItemNum + 1,
                     RowId = ReportId + rowNum
                  },
                  new ReportRowData
                  {
                     Id = ReportId + rowItemNum + 2,
                     Value = item.VolumeBrought.ToString(),
                     SortOrder = rowItemNum + 2,
                     RowId = ReportId + rowNum
                  },
                  new ReportRowData
                  {
                     Id = ReportId + rowItemNum + 3,
                     Value = item.ChildsInserted.ToString(),
                     SortOrder = rowItemNum + 3,
                     RowId = ReportId + rowNum
                  },
                  new ReportRowData
                  {
                     Id = ReportId + rowItemNum + 4,
                     Value = item.ForAprove.ToString(),
                     SortOrder = rowItemNum + 4,
                     RowId = ReportId + rowNum
                  },
                  new ReportRowData
                  {
                     Id = ReportId + rowItemNum + 5,
                     Value = item.Approved.ToString(),
                     SortOrder = rowItemNum + 5,
                     RowId = ReportId + rowNum
                  },
                  new ReportRowData
                  {
                     Id = ReportId + rowItemNum + 6,
                     Value = item.Paid.ToString(),
                     SortOrder = rowItemNum + 6,
                     RowId = ReportId + rowNum
                  }
               });

            rowItemNum += 7;
            rowNum++;
         }

         reportTable.Rows.Add(row);
      }

      private void SaveReport(IUnitOfWork unitOfWork, ReportSheet reportSheet)
      {
         if (unitOfWork.GetSet<ReportSheet>().Any(r => r.Id == ReportId))
         {
            unitOfWork.Delete(new ReportSheet {Id = ReportId});
            unitOfWork.SaveChanges();
         }

         unitOfWork.Context.Set<ReportSheet>().Add(reportSheet);
         unitOfWork.SaveChanges();
      }
   }
}
