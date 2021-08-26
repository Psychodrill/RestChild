using OfficeOpenXml;
using OfficeOpenXml.Style;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace RestChild.Web.Controllers
{
   public static class ZAGZIntegrationQueue
   {
      private static bool _inProgress = false;

      public static bool InProgress
      {
         get
         {
            return _inProgress && _status != null && _status.LastUpdate > DateTime.Now.AddMinutes(-4);
         }
      }

      private static readonly object _locker = new object();

      private static ProgressStatus _status = null;

      private static byte[] _file = null;

      public static ProgressStatus GetStatus()
      {
         return _status ?? new ProgressStatus() { IsError = true, Message = "Процесс не запущен" };
      }

      public static string Execute(byte[] File)
      {
         lock(_locker)
         {
            if (InProgress)
            {
               if (_status != null && _status.LastUpdate > DateTime.Now.AddMinutes(-3))
               {
                  return "Процесс занят";
               }
            }
               
            _file = File;
            _inProgress = true;
            _status = new ProgressStatus() { Message = "Message", Step = 1, Steps = 100 };


            Thread th = new Thread(Do);
            th.Start();
         }

         return null;
      }

      private static void Do()
      {
         try
         {
            using (var uv = new UnitOfWork())
            {
               SetStatus(new ProgressStatus() { IsError = false, Steps = 100, Step = 1, Message = "Открытие файла данных..." });
               var table = ZAGZIntegration.ImportZagz2018.GetDataTableFromExcel(_file);
               _file = null;

               int steps = table.Count + 3;
               SetStatus(new ProgressStatus() { IsError = false, Steps = steps, Step = 2, Message = $"Файл данных открыт. Извлечено {steps} строк." });

               var index = 3;

               foreach (var row in table)
               {
                  SetStatus(new ProgressStatus() { IsError = false, Steps = steps, Step = index, Message = $"Завершено на {Math.Round((double)(index * 100 / steps))}%; Исполняется {index - 2} строка." });
                  index++;

                  //Thread.Sleep(1500);

                  if (row.Count < 3)
                  {
                     continue;
                  }

                  var requestId = row[0].LongParse();
                  var applicantId = row[1].LongParse();
                  var childId = row[2].LongParse();

                  var exch = new ExchangeBaseRegistry
                  {
                     ApplicantId = applicantId,
                     ChildId = childId,
                     Child = uv.GetById<Child>(childId),
                     IsIncoming = false,
                     SendDate = DateTime.Now,
                     ResponseDate = DateTime.Now,
                     ExchangeBaseRegistryTypeId = (long)ExchangeBaseRegistryTypeEnum.Relationship,
                     ServiceNumber = RelationshipLoadServiceNumber.Name,
                     OperationType = RelationshipLoadServiceNumber.Name,
                     IsProcessed = true,
                     RequestGuid = RelationshipLoadServiceNumber.Name,
                  };
                  if (!exch.ChildId.HasValue || !exch.ApplicantId.HasValue || requestId != exch.Child?.RequestId)
                  {
                     continue;
                  }
                  // сброс старых записей
                  var clear = uv.GetSet<ExchangeBaseRegistry>()
                     .Where(e => !e.NotActual && e.ExchangeBaseRegistryTypeId == (long)ExchangeBaseRegistryTypeEnum.Relationship &&
                                 e.ChildId == exch.ChildId && e.ApplicantId == exch.ApplicantId);
                  foreach (var c in clear)
                  {
                     c.NotActual = true;
                  }

                  var responseGuid = Guid.NewGuid();
                  if (row.Count < 26 || string.Equals(row[22], "NULL", StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(row[22]))
                  {
                     exch.ResponseText = string.Format(ZAGZIntegration.ImportZagz2018.responseErrorTemplate, responseGuid, exch.ServiceNumber,
                        exch.SendDate.DateTimeToXml(), exch.RequestGuid, DateTime.Now.DateTimeToXml(), exch.ServiceNumber);
                     exch.Success = false;
                  }
                  else
                  {
                     exch.ResponseText = string.Format(ZAGZIntegration.ImportZagz2018.responseSuccessTemplate, responseGuid, exch.ServiceNumber,
                        exch.SendDate.DateTimeToXml(), exch.RequestGuid, DateTime.Now.DateTimeToXml(),
                        row[11].TryParseDateDdMmYyyy().DateTimeToXml(),
                        exch.Child?.PlaceOfBirth, row[9], row[8], row[10],
                        row[16].TryParseDateDdMmYyyy().DateTimeToXml(), row[14].NullEmpty(),
                        row[13].NullEmpty(), row[15].NullEmpty(),
                        row[20].TryParseDateDdMmYyyy().DateTimeToXml(), row[18].NullEmpty(),
                        row[17].NullEmpty(), row[19].NullEmpty(), row[23].NullEmpty(), row[24].TryParseDateDdMmYyyy().DateTimeToXml(),
                        row[25].NullEmpty(), row[21].NullEmpty(), row[22].NullEmpty(), string.Empty);

                     //exch.Success = (exch.Child?.DocumentTypeId != (long) DocumentTypeEnum.CertOfBirth && exch.Child?.DocumentTypeId != 23) ||
                     //               (exch.Child.DocumentNumber == row[22] && exch.Child.DocumentSeria == row[21]);
                     exch.Success = true;
                  }
                  uv.AddEntity(exch, false);
                  //uv.SaveChanges();
                  foreach (var entry in uv.Context.ChangeTracker.Entries())
                  {
                     entry.State = EntityState.Detached;
                  }
               }
               SetStatus(new ProgressStatus() { IsError = false, Steps = steps, Step = steps, Message = $"Завершено." });
            }
         }
         catch (Exception ex)
         {
            if (_file != null)
            {
               _file = null;
            }
            SetStatus(new ProgressStatus() { IsError = true, Message = "Во время загрузки произошел сбой: " + ex.Message });
         }
      }

      private static void SetStatus(ProgressStatus status)
      {
         lock (_locker)
         {
            if (status != null && _status != null)
            {
               if(status.IsError)
               {
                  _status.IsError = true;
               }
               else
               {
                  _status.Step = status.Step;
                  _status.Steps = status.Steps;
               }
               _status.Message = status.Message;
               _status.LastUpdate = DateTime.Now;
            }
         }
      }

   }

   public class ZAGZIntegrationController : BaseController
   {
      public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
      {
         base.SetUnitOfWorkInRefClass(unitOfWork);
      }

      public ActionResult Index(string Msg = "")
      {
         SetUnitOfWorkInRefClass(UnitOfWork);

         if (!Security.HasRight(AccessRightEnum.ZAGSIntegration))
         {
            return RedirectToAvalibleAction();
         }

         ViewBag.Yars = UnitOfWork.GetSet<YearOfRest>().Where(ss => !ss.IsClosed).ToDictionary(x => x.Id, y => y.Name);

         return View((object)Msg);
      }

      public ActionResult ExportToExcel(long YearOfRestId = 0)
      {
         SetUnitOfWorkInRefClass(UnitOfWork);

         if (!Security.HasRight(AccessRightEnum.ZAGSIntegration))
         {
            return RedirectToAvalibleAction();
         }


         var dt = new DataTable();
         var conn = UnitOfWork.Database.Connection;
         var connectionState = conn.State;

         try
         {
            if (connectionState != ConnectionState.Open) conn.Open();
            using (var cmd = conn.CreateCommand())
            {
               cmd.CommandText = "Select r.Id as RequestId, a.Id as ApplicantId, c.Id as ChildId, a.LastName, a.FirstName, a.MiddleName, a.DateOfBirth, a.Male, c.LastName, c.FirstName, c.MiddleName, c.DateOfBirth, c.Male, case when c.DocumentTypeId = 22 then c.DocumentSeria else  '' end as DocumentSeria, case when c.DocumentTypeId = 22 then c.DocumentNumber else  '' end as DocumentNumber from dbo.Request r inner join dbo.Child c on c.RequestId = r.Id inner join dbo.Applicant a on a.Id = r.ApplicantId where r.IsFirstCompany = 1 and r.StatusId = 1050 and r.YearOfRestId = @yor union all select r.Id, a.Id, c.Id, a.LastName, a.FirstName, a.MiddleName, a.DateOfBirth, a.Male, c.LastName, c.FirstName, c.MiddleName, c.DateOfBirth, c.Male, case when c.DocumentTypeId = 22 then c.DocumentSeria else  '' end as DocumentSeria, case when c.DocumentTypeId = 22 then c.DocumentNumber else  '' end as DocumentNumber from dbo.Request r inner join dbo.Child c on c.RequestId = r.Id inner join dbo.Applicant a on a.RequestId = r.Id and a.IsAgent = 0 where r.IsFirstCompany = 1 and r.StatusId = 1050 and r.YearOfRestId = @yor";
               cmd.CommandType = CommandType.Text;
               cmd.Parameters.Add(new SqlParameter("yor", YearOfRestId));
               using (var reader = cmd.ExecuteReader())
               {
                  dt.Load(reader);
               }
            }
         }
         catch (Exception ex)
         {
            throw ex;
         }
         finally
         {
            if (connectionState != ConnectionState.Closed) conn.Close();
         }

         using (var ms = new MemoryStream())
         {
            using (var pkg = new ExcelPackage(ms))
            {
               var sheet = pkg.Workbook.Worksheets.Add("Отчет");
               //creates headers
               SetCellHeader(sheet.Cells[1, 1], "RequestId");
               SetCellHeader(sheet.Cells[1, 2], "ApplicantId");
               SetCellHeader(sheet.Cells[1, 3], "ChildId");
               SetCellHeader(sheet.Cells[1, 4], "LastName");
               SetCellHeader(sheet.Cells[1, 5], "FirstName");
               SetCellHeader(sheet.Cells[1, 6], "MiddleName");
               SetCellHeader(sheet.Cells[1, 7], "DateOfBirth");
               SetCellHeader(sheet.Cells[1, 8], "Male");
               SetCellHeader(sheet.Cells[1, 9], "LastName");
               SetCellHeader(sheet.Cells[1, 10], "FirstName");
               SetCellHeader(sheet.Cells[1, 11], "MiddleName");
               SetCellHeader(sheet.Cells[1, 12], "DateOfBirth");
               SetCellHeader(sheet.Cells[1, 13], "Male");
               SetCellHeader(sheet.Cells[1, 14], "Серия");
               SetCellHeader(sheet.Cells[1, 15], "Номер");
               SetCellHeader(sheet.Cells[1, 16], "Фам матери");
               SetCellHeader(sheet.Cells[1, 17], "Имя матери");
               SetCellHeader(sheet.Cells[1, 18], "Отч. матери");
               SetCellHeader(sheet.Cells[1, 19], "ДР матери");
               SetCellHeader(sheet.Cells[1, 20], "Фам. отца");
               SetCellHeader(sheet.Cells[1, 21], "Имя отца");
               SetCellHeader(sheet.Cells[1, 22], "Отч. отца");
               SetCellHeader(sheet.Cells[1, 23], "ДР отца");
               SetCellHeader(sheet.Cells[1, 24], "Серия свид.");
               SetCellHeader(sheet.Cells[1, 25], "Номер свид.");
               SetCellHeader(sheet.Cells[1, 26], "Ном. ЗА");
               SetCellHeader(sheet.Cells[1, 27], "Дата ЗА");
               SetCellHeader(sheet.Cells[1, 28], "ЗАГС");
               SetCellHeader(sheet.Cells[1, 29], "Дата рождения");

               for(int i = 0; i < dt.Rows.Count; i++)
               {
                  var _col = i + 2;
                  SetCellFormat(sheet.Cells[_col, 1]);
                  sheet.Cells[_col, 1].Value = (long)dt.Rows[i][0];

                  SetCellFormat(sheet.Cells[_col, 2]);
                  sheet.Cells[_col, 2].Value = (long)dt.Rows[i][1];

                  SetCellFormat(sheet.Cells[_col, 3]);
                  sheet.Cells[_col, 3].Value = dt.Rows[i][2] != DBNull.Value ? (long)dt.Rows[i][2] : 0;// (long)dt.Rows[i][2];

                  SetCellFormat(sheet.Cells[_col, 4]);
                  sheet.Cells[_col, 4].Value = dt.Rows[i][3] != DBNull.Value ? (string)dt.Rows[i][3] : string.Empty;// (string)dt.Rows[i][3];

                  SetCellFormat(sheet.Cells[_col, 5]);
                  sheet.Cells[_col, 5].Value = dt.Rows[i][4] != DBNull.Value ? (string)dt.Rows[i][4] : string.Empty;// (string)dt.Rows[i][4];

                  SetCellFormat(sheet.Cells[_col, 6]);
                  sheet.Cells[_col, 6].Value = dt.Rows[i][5] != DBNull.Value ? (string)dt.Rows[i][5] : string.Empty;

                  SetCellFormat(sheet.Cells[_col, 7], true);
                  sheet.Cells[_col, 7].Value = ((DateTime)dt.Rows[i][6]).FormatEx();

                  SetCellFormat(sheet.Cells[_col, 8]);
                  sheet.Cells[_col, 8].Value = ((bool)dt.Rows[i][7]) ? 1 : 0;

                  SetCellFormat(sheet.Cells[_col, 9]);
                  sheet.Cells[_col, 9].Value = (string)dt.Rows[i][8];

                  SetCellFormat(sheet.Cells[_col, 10]);
                  sheet.Cells[_col, 10].Value = (string)dt.Rows[i][9];

                  SetCellFormat(sheet.Cells[_col, 11]);
                  sheet.Cells[_col, 11].Value = dt.Rows[i][10] != DBNull.Value ? (string)dt.Rows[i][10] : string.Empty;

                  SetCellFormat(sheet.Cells[_col, 12], true);
                  sheet.Cells[_col, 12].Value = ((DateTime)dt.Rows[i][11]).FormatEx();

                  SetCellFormat(sheet.Cells[_col, 13]);
                  sheet.Cells[_col, 13].Value = ((bool)dt.Rows[i][12]) ? 1 : 0;

                  SetCellFormat(sheet.Cells[_col, 14]);
                  SetCellFormat(sheet.Cells[_col, 15]);
                  SetCellFormat(sheet.Cells[_col, 16]);
                  SetCellFormat(sheet.Cells[_col, 17]);
                  SetCellFormat(sheet.Cells[_col, 18]);
                  SetCellFormat(sheet.Cells[_col, 19], true);
                  SetCellFormat(sheet.Cells[_col, 20]);
                  SetCellFormat(sheet.Cells[_col, 21]);
                  SetCellFormat(sheet.Cells[_col, 22]);
                  SetCellFormat(sheet.Cells[_col, 23], true);
                  SetCellFormat(sheet.Cells[_col, 24]);
                  SetCellFormat(sheet.Cells[_col, 25]);
                  SetCellFormat(sheet.Cells[_col, 26]);
                  SetCellFormat(sheet.Cells[_col, 27], true);
                  SetCellFormat(sheet.Cells[_col, 28]);
                  SetCellFormat(sheet.Cells[_col, 29]);
                  sheet.Cells[_col, 14, _col, 29].Value = string.Empty;
               }

               sheet.Column(1).Width = 15;
               sheet.Column(2).Width = 15;
               sheet.Column(3).Width = 15;
               sheet.Column(4).Width = 30;
               sheet.Column(5).Width = 30;
               sheet.Column(6).Width = 30;
               sheet.Column(7).Width = 15;
               sheet.Column(8).Width = 15;
               sheet.Column(9).Width = 30;
               sheet.Column(10).Width = 30;
               sheet.Column(11).Width = 30;
               sheet.Column(12).Width = 15;
               sheet.Column(13).Width = 15;
               sheet.Column(14).Width = 15;
               sheet.Column(15).Width = 15;
               sheet.Column(16).Width = 30;
               sheet.Column(17).Width = 30;
               sheet.Column(18).Width = 30;
               sheet.Column(19).Width = 15;
               sheet.Column(20).Width = 30;
               sheet.Column(21).Width = 30;
               sheet.Column(22).Width = 30;
               sheet.Column(23).Width = 15;
               sheet.Column(24).Width = 15;
               sheet.Column(25).Width = 15;
               sheet.Column(26).Width = 15;
               sheet.Column(27).Width = 15;
               sheet.Column(28).Width = 45;
               sheet.Column(29).Width = 17;


               pkg.Save();
            }

            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Получение сведений для запроса родства.xlsx");
         }

         //return FileAndDeleteOnClose(null, "application/zip", "Получение сведений для запроса родства.zip");
      }

      private static void SetCellHeader(ExcelRange cell, string Name)
      {
         cell.Style.Font.SetFromFont(new System.Drawing.Font("Times New Roman", 12));
         cell.Style.WrapText = true;
         cell.Style.Font.Bold = false;
         cell.Style.Font.Size = 11;
         cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
         cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
         cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
         cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(166, 166, 166));
         cell.Value = Name;
      }

      private static void SetCellFormat(ExcelRange cell, bool setYellowBackGround = false)
      {
         cell.Style.Font.SetFromFont(new System.Drawing.Font("Times New Roman", 12));
         cell.Style.WrapText = true;
         cell.Style.Font.Bold = false;
         cell.Style.Font.Size = 11;
         cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
         cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
         if(setYellowBackGround)
         {
            cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(255, 255, 0));
         }
      }

      public ActionResult StartProcess()
      {
         if (!Security.HasRight(AccessRightEnum.ZAGSIntegration))
         {
            return RedirectToAvalibleAction();
         }


         if (Request.Files.Count != 1)
         {
            return View("Index", (object)"Ошибка загрузки файла");
         }

         HttpPostedFileBase file = Request.Files[0];

         byte[] data;
         using (MemoryStream target = new MemoryStream())
         {
            file.InputStream.CopyTo(target);
            data = target.ToArray();
         }
            
         var result = ZAGZIntegrationQueue.Execute(data);

         if(string.IsNullOrWhiteSpace(result))
         {
            return RedirectToAction("Index");
         }

         return View("Index", (object)result);
      }

      public ActionResult GetStatus(Guid Index)
      {
         if (!Security.HasRight(AccessRightEnum.ZAGSIntegration))
         {
            return RedirectToAvalibleAction();
         }

         return Json(ZAGZIntegrationQueue.GetStatus(), JsonRequestBehavior.AllowGet);
      }
   }
}
