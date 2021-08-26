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
   /// <summary>
   ///    формирование отчетов
   /// </summary>
   public class BaseUpdateTour
   {
      private const string Green = "#C5FCC2";
      private const string Grey = "#f1f1f1";
      private const string Red = "#FCC2C2";
      private const string Yellow = "#FCFBC2";
      private readonly Guid _accessRight;
      private readonly string _cssClass;
      private readonly long? _organisationId;
      private readonly long _reportDataId;
      private readonly long _reportHeadersId;
      private readonly long _reportRowId;
      private readonly long _reportSheetId;
      private readonly long _reportTableId;
      private readonly string _tabName;
      private readonly TypeOfRestEnum _typeOfRest;
      private Dictionary<string, ReportTableHead> _dictHeaders;
      private ReportSheet _entity;
      private int _organizatonBusyTotal;
      private int _organizatonTotal;
      private ReportTable _reportTable;
      private ReportTableHead _reportTableHead;
      private readonly string _tableName;
      private ReportTableHead _totalHeader;
      private ReportTableHead _totalHighHeader;
      private bool _useExistedSheet;

      public BaseUpdateTour(Guid accessRight, TypeOfRestEnum typeOfRest, long startId, string tabName,
         long? sheetId = null, bool useExistedSheet = false, long? organisationId = null,
         string tableName = "Забронировано", string cssClass = null)
      {
         _typeOfRest = typeOfRest;
         _reportSheetId = sheetId ?? startId;
         _useExistedSheet = useExistedSheet;
         _reportRowId = startId;
         _reportTableId = startId;
         _reportDataId = startId;
         _reportHeadersId = startId;
         _accessRight = accessRight;
         _organisationId = organisationId;
         _tableName = tableName;
         _cssClass = cssClass ?? "tour-report-table";
         _tabName = tabName;
      }

      public void UpdateTourReport()
      {
         var yearOfCompany = ConfigurationManager.AppSettings["YearMultiCompany"].IntParse() ?? DateTime.Today.Year;
         using (var unit = new UnitOfWork())
         {
            var listsOfChildrenDbSets = unit.GetSet<ListOfChilds>();
            var typeOfRest = unit.GetById<TypeOfRest>((long) _typeOfRest);

            if (typeOfRest == null)
            {
               return;
            }

            if (_useExistedSheet)
            {
               _entity = unit.GetById<ReportSheet>(_reportSheetId);
            }

            if (_entity == null)
            {
               _entity = new ReportSheet
               {
                  Id = _reportSheetId,
                  CodeAccess = _accessRight,
                  ReportTables = new List<ReportTable>(),
                  ReportName = _tabName ?? typeOfRest.Name,
                  SortOrder = 10
               };
               _useExistedSheet = false;
            }


            _reportTable = new ReportTable
            {
               Id = _reportTableId,
               ReportSheetId = _entity.Id,
               ReportTableHeads = new List<ReportTableHead>(),
               Rows = new List<ReportTableRow>(),
               SortOrder = 1,
               CssClass = _cssClass
            };

            _dictHeaders = new Dictionary<string, ReportTableHead>();

            _entity.ReportTables.Add(_reportTable);

            _reportTableHead = new ReportTableHead
            {
               ReportTableId = _reportTable.Id,
               SortOrder = 0,
               Name = "&nbsp;",
               Key = "00001",
               ReportTable = _reportTable,
               RowIndex = 1,
               RowSpan = 2
            };
            _reportTable.ReportTableHeads.Add(_reportTableHead);

            _totalHeader = new ReportTableHead
            {
               ReportTableId = _reportTable.Id,
               SortOrder = 2,
               Name = "Всего",
               Key = "00002",
               ReportTable = _reportTable,
               RowIndex = 2
            };
            _reportTable.ReportTableHeads.Add(_totalHeader);

            _totalHighHeader = new ReportTableHead
            {
               ReportTableId = _reportTable.Id,
               SortOrder = 1,
               Name = _tableName,
               Key = "000000",
               RowIndex = 1,
               ColSpan = 1,
               RowSpan = 1,
               ReportTable = _reportTable
            };
            _reportTable.ReportTableHeads.Add(_totalHighHeader);

            var contracts =
               unit.GetSet<Contract>()
                  .Where(
                     c =>
                        c.YearOfRest.Year >= yearOfCompany && c.StateId == StateMachineStateEnum.Contract.Active &&
                        c.Supplier != null &&
                        c.Tour.Any(
                           t =>
                              t.StateId.HasValue &&
                              (t.StateId == StateMachineStateEnum.Tour.Formed ||
                               t.StateId == StateMachineStateEnum.Tour.ToFormationFromFormed)
                              && t.YearOfRest.Year >= yearOfCompany && t.IsActive
                              && (t.TypeOfRestId == (long) _typeOfRest || t.TypeOfRest.ParentId == (long) _typeOfRest)))
                  .ToList();

            var rowIndex = 1;
            foreach (var cntGroup in contracts.GroupBy(c => c.SupplierId))
            {
               _organizatonTotal = 0;
               _organizatonBusyTotal = 0;
               var row = new ReportTableRow
               {
                  TableId = _reportTable.Id,
                  Table = _reportTable,
                  SortOrder = rowIndex++,
                  RowData = new List<ReportRowData>()
               };
               var organizationId = cntGroup.FirstOrDefault().NullSafe(c => c.Supplier.Id);
               row.RowData.Add(new ReportRowData
               {
                  Value = $"<b>{cntGroup.FirstOrDefault().NullSafe(c => c.Supplier.Name)}</b>",
                  ReportTableHeadId = _reportTableHead.Id,
                  ReportTableHead = _reportTableHead,
                  Row = row,
                  CssClass = "main-report-header"
               });

               var organizationTotalData = new ReportRowData
               {
                  Value = cntGroup.Select(c => c.PlanCount).Sum().FormatEx(),
                  ReportTableHeadId = _totalHeader.Id,
                  ReportTableHead = _totalHeader,
                  Row = row
               };

               row.RowData.Add(organizationTotalData);
               _reportTable.Rows.Add(row);

               var toursDb = unit.GetSet<Tour>().AsQueryable();

               if (_organisationId.HasValue)
               {
                  toursDb = toursDb.Where(t => t.LimitOnVedomstvo.OrganizationId == _organisationId);
               }

               var hotelsData = toursDb
                  .Where(
                     t =>
                        t.StateId.HasValue && (t.StateId == StateMachineStateEnum.Tour.Formed ||
                                               t.StateId == StateMachineStateEnum.Tour.ToFormationFromFormed)
                                           && t.YearOfRest.Year >= yearOfCompany && t.IsActive && t.Contract != null &&
                                           t.Contract.SupplierId == organizationId
                                           &&
                                           (t.TypeOfRestId == (long) _typeOfRest ||
                                            t.TypeOfRest.ParentId == (long) _typeOfRest))
                  .Select(t => new {Tour = t, ChildLists = listsOfChildrenDbSets.Where(l => l.TourId == t.Id).ToList()})
                  .ToList()
                  .Select(
                     t =>
                     {
                        t.Tour.ChildLists = t.ChildLists;
                        return t.Tour;
                     }).ToList();

               var hotels = hotelsData.GroupBy(g => g.HotelsId).Select(s => s.ToList()).ToList();

               rowIndex = RowIndex(hotels, rowIndex);

               organizationTotalData.Value = string.Format("{2} из {1} (запланировано {0})",
                  cntGroup.Select(c => c.PlanCount).Sum().FormatEx(),
                  _organizatonTotal, _organizatonBusyTotal);

               organizationTotalData.Style =
                  $"background-color: {(_organizatonBusyTotal == _organizatonTotal ? Grey : Convert.ToDouble(_organizatonBusyTotal) / Convert.ToDouble(_organizatonTotal) * 100d > 90d ? Green : Convert.ToDouble(_organizatonBusyTotal) / Convert.ToDouble(_organizatonTotal) * 100d < 50d ? Red : Yellow)}";

               var times = hotelsData.GroupBy(GetKeyTour).OrderBy(g => g.Key).ToList();
               foreach (var time in times)
               {
                  if (!_dictHeaders.ContainsKey(time.Key))
                  {
                     var t = time.FirstOrDefault();
                     var name = t.NullSafe(tm => tm.TimeOfRest.GroupedTimeOfRest.Name) ??
                                $"{t.NullSafe(tm => tm.DateIncome).FormatEx()}-{t.NullSafe(tm => tm.DateOutcome).FormatEx()}";

                     var header = new ReportTableHead
                     {
                        Name = name,
                        Key = time.Key,
                        ReportTableId = _reportTable.Id,
                        ReportTable = _reportTable,
                        RowIndex = 2
                     };
                     _dictHeaders.Add(time.Key, header);
                  }


                  var countRow = time.Select(t => t.Volumes.Select(v =>
                                    v.TypeOfRoomsId.HasValue
                                       ? v.TypeOfRooms.CountBasePlace * v.CountRooms
                                       : v.CountPlace).Sum()).Sum() ?? 0;
                  int countBusyRow;
                  if (_typeOfRest == TypeOfRestEnum.SpecializedСamp)
                  {
                     countBusyRow = time.Where(t => t.ChildLists != null).Select(t =>
                        t.ChildLists
                           .Where(l => !l.IsDeleted && l.StateId != StateMachineStateEnum.Deleted && l.Childs != null)
                           .Select(l => l.Childs.Count).Sum()).Sum();
                  }
                  else
                  {
                     countBusyRow = time.Select(t => t.Volumes.Select(v =>
                                       v.TypeOfRoomsId.HasValue
                                          ? v.TypeOfRooms.CountBasePlace * v.CountBusyRooms
                                          : v.CountBusyPlace).Sum()).Sum() ?? 0;
                  }

                  row.RowData.Add(new ReportRowData
                  {
                     Value = string.Format("{1} / {0}", countRow.FormatEx(), countBusyRow.FormatEx()),
                     ReportTableHead = _dictHeaders[time.Key],
                     Style = _typeOfRest != TypeOfRestEnum.SpecializedСamp
                        ? $"background-color: {(countBusyRow == countRow ? Grey : Convert.ToDouble(countBusyRow) / Convert.ToDouble(countRow) * 100d > 90d ? Green : Convert.ToDouble(countBusyRow) / Convert.ToDouble(countRow) * 100d < 50d ? Red : Yellow)}"
                        : string.Empty,
                     Row = row
                  });
               }
            }

            _organizatonTotal = 0;

            _organizatonBusyTotal = 0;


            var tours = unit.GetSet<Tour>().AsQueryable();

            if (_organisationId.HasValue)
            {
               tours = tours.Where(t => t.LimitOnVedomstvo.OrganizationId == _organisationId);
            }

            var dataItems = tours
               .Where(
                  t =>
                     t.StateId.HasValue && (t.StateId == StateMachineStateEnum.Tour.Formed ||
                                            t.StateId == StateMachineStateEnum.Tour.ToFormationFromFormed)
                                        && t.YearOfRest.Year >= yearOfCompany && t.IsActive &&
                                        (!t.ContractId.HasValue || t.Contract.Supplier == null ||
                                         t.Contract.StateId != StateMachineStateEnum.Contract.Active)
                                        && (t.TypeOfRestId == (long) _typeOfRest ||
                                            t.TypeOfRest.ParentId == (long) _typeOfRest))
               .Select(t => new {Tour = t, ChildLists = listsOfChildrenDbSets.Where(l => l.TourId == t.Id).ToList()})
               .ToList()
               .Select(
                  t =>
                  {
                     t.Tour.ChildLists = t.ChildLists;
                     return t.Tour;
                  }).ToList();

            RowIndex(dataItems.GroupBy(g => g.HotelsId).Select(s => s.ToList()).ToList(), rowIndex);

            var headers = _dictHeaders.Values.OrderBy(h => h.Key).ToList();
            var orderIndex = 3;

            foreach (var header in headers)
            {
               header.SortOrder = orderIndex++;
            }

            _reportTable.ReportTableHeads.AddRange(headers);

            var headersId = _reportHeadersId;
            var rowId = _reportRowId;
            var dataId = _reportDataId;

            foreach (var header in _reportTable.ReportTableHeads)
            {
               header.Id = headersId++;
               header.ReportTableId = header.ReportTable.Id;
            }

            foreach (var row in _reportTable.Rows)
            {
               var existsKeys = row.RowData.Select(r => r.ReportTableHead.Key).ToList();
               var addonHeaders = _reportTable.ReportTableHeads
                  .Where(h => !existsKeys.Contains(h.Key) && h.Key != "000000").ToList();
               foreach (var ah in addonHeaders)
               {
                  row.RowData.Add(new ReportRowData
                  {
                     ReportTableHead = ah,
                     Value = "-",
                     Row = row
                  });
               }

               row.Id = rowId++;
               row.RowData = row.RowData.OrderBy(d => d.ReportTableHead.Key).ToList();
               row.TableId = row.Table.Id;
               _totalHighHeader.ColSpan = existsKeys.Count - 1;

               foreach (var data in row.RowData)
               {
                  data.SortOrder = Convert.ToInt32(dataId);
                  data.Id = dataId++;
                  data.ReportTableHeadId = data.ReportTableHead.Id;
                  data.RowId = data.Row.Id;
               }
            }


            if (!_useExistedSheet)
            {
               if (unit.GetSet<ReportSheet>().Any(r => r.Id == _reportSheetId))
               {
                  unit.Delete(new ReportSheet {Id = _reportSheetId});
                  unit.SaveChanges();
               }

               unit.AddEntity(_entity);
            }

            unit.SaveChanges();
         }
      }

      private int RowIndex(List<List<Tour>> hotels, int rowIndex)
      {
         foreach (var group in hotels)
         {
            var hotel = group.Select(g => g.Hotels).FirstOrDefault();
            var filteredGroups = _organisationId.HasValue
               ? group.Where(g => g.LimitOnVedomstvo.OrganizationId == _organisationId).ToList()
               : group;

            if (!filteredGroups.Any())
            {
               continue;
            }

            var hotelRow = new ReportTableRow
            {
               SortOrder = rowIndex++,
               Table = _reportTable,
               RowData = new List<ReportRowData>()
            };

            hotelRow.RowData.Add(new ReportRowData
            {
               Value = hotel.NullSafe(h => h.Name),
               ReportTableHeadId = _reportTableHead.Id,
               ReportTableHead = _reportTableHead,
               Row = hotelRow,
               CssClass = "main-report-header"
            });

            var totalRowData = new ReportRowData
            {
               Value = "-",
               ReportTableHeadId = _totalHeader.Id,
               ReportTableHead = _totalHeader,
               Row = hotelRow
            };

            hotelRow.RowData.Add(totalRowData);
            var totalRow = 0;
            var totalBusyRow = 0;
            var times = filteredGroups.GroupBy(GetKeyTour).OrderBy(g => g.Key).ToList();
            foreach (var time in times)
            {
               if (!_dictHeaders.ContainsKey(time.Key))
               {
                  var t = time.FirstOrDefault();
                  var name = t.NullSafe(tm => tm.TimeOfRest.GroupedTimeOfRest.Name) ??
                             $"{t.NullSafe(tm => tm.DateIncome).FormatEx()}-{t.NullSafe(tm => tm.DateOutcome).FormatEx()}";

                  var header = new ReportTableHead
                  {
                     Name = name,
                     Key = time.Key,
                     ReportTableId = _reportTable.Id,
                     ReportTable = _reportTable,
                     RowIndex = 2
                  };
                  _dictHeaders.Add(time.Key, header);
               }


               var countRow = time.Select(t => t.Volumes.Select(v =>
                                 v.TypeOfRoomsId.HasValue
                                    ? v.TypeOfRooms.CountBasePlace * v.CountRooms
                                    : v.CountPlace).Sum()).Sum() ?? 0;
               int countBusyRow;
               if (_typeOfRest == TypeOfRestEnum.SpecializedСamp)
               {
                  countBusyRow = time.Where(t => t.ChildLists != null).Select(t =>
                     t.ChildLists
                        .Where(l => !l.IsDeleted && l.StateId != StateMachineStateEnum.Deleted && l.Childs != null)
                        .Select(l => l.Childs.Count).Sum()).Sum();
               }
               else
               {
                  countBusyRow = time.Select(t => t.Volumes.Select(v =>
                                    v.TypeOfRoomsId.HasValue
                                       ? v.TypeOfRooms.CountBasePlace * v.CountBusyRooms
                                       : v.CountBusyPlace).Sum()).Sum() ?? 0;
               }

               totalRow += countRow;
               totalBusyRow += countBusyRow;

               hotelRow.RowData.Add(new ReportRowData
               {
                  Value = string.Format("{1} / {0}", countRow.FormatEx(), countBusyRow.FormatEx()),
                  ReportTableHead = _dictHeaders[time.Key],
                  Style = _typeOfRest != TypeOfRestEnum.SpecializedСamp
                     ? $"background-color: {(countBusyRow == countRow ? Grey : Convert.ToDouble(countBusyRow) / Convert.ToDouble(countRow) * 100d > 90d ? Green : Convert.ToDouble(countBusyRow) / Convert.ToDouble(countRow) * 100d < 50d ? Red : Yellow)}"
                     : string.Empty,
                  Row = hotelRow
               });
            }

            totalRowData.Value = string.Format("{1} / {0}", totalRow.FormatEx(), totalBusyRow.FormatEx());
            totalRowData.Style =
               $"background-color: {(totalBusyRow == totalRow ? Grey : Convert.ToDouble(totalBusyRow) / Convert.ToDouble(totalRow) * 100d > 90d ? Green : Convert.ToDouble(totalBusyRow) / Convert.ToDouble(totalRow) * 100d < 50d ? Red : Yellow)}";

            _organizatonTotal += totalRow;
            _organizatonBusyTotal += totalBusyRow;
            _reportTable.Rows.Add(hotelRow);
         }

         return rowIndex;
      }

      private static string GetKeyTour(Tour time)
      {
         string key;
         if (time.NullSafe(t => t.TimeOfRest.GroupedTimeOfRestId) > 0)
         {
            key = "000-" + time.NullSafe(t => t.TimeOfRest.GroupedTimeOfRestId);
         }
         else
         {
            key = $"002-{time.NullSafe(t => t.DateIncome):yyyyMMdd}-{time.NullSafe(t => t.DateOutcome):yyyyMMdd}";
         }

         return key;
      }
   }
}
