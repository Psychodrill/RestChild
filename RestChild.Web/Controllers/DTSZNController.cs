using DocumentFormat.OpenXml.Spreadsheet;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Comon.ToExcel;
using RestChild.DAL;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace RestChild.Web.Controllers
{
   public static class DTSZNIntegrationQueue
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
         lock (_locker)
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
               var table = ZAGZIntegration.ImportZagz2018.GetDataTableFromExcel(_file, 3);
               _file = null;

               int steps = table.Count + 3;
               SetStatus(new ProgressStatus() { IsError = false, Steps = steps, Step = 2, Message = $"Файл данных открыт. Извлечено {steps} строк." });

               var index = 3;

               foreach (var row in table)
               {
                  SetStatus(new ProgressStatus() { IsError = false, Steps = steps, Step = index, Message = $"Завершено на {Math.Round((double)(index * 100 / steps))}%; Исполняется {index - 2} строка." });
                  index++;

                  //Thread.Sleep(2500);

                  if (row.Count < 3)
                  {
                     continue;
                  }

                  var requestId = row[20].ToString();
                  var applicantId = row[2].LongParse();
                  var childId = row[1].LongParse();

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
                  if (!exch.ChildId.HasValue || !exch.ApplicantId.HasValue || requestId != exch.Child?.Request.RequestNumber)
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
                  if (row.Count < 21 || string.IsNullOrWhiteSpace(row[21]) || string.Equals(row[21], "NULL", StringComparison.OrdinalIgnoreCase) || string.Equals(row[21], "0", StringComparison.OrdinalIgnoreCase))
                  {
                     exch.ResponseText = string.Format(ZAGZIntegration.ImportZagz2018.responseErrorTemplate, responseGuid, exch.ServiceNumber,
                        exch.SendDate.DateTimeToXml(), exch.RequestGuid, DateTime.Now.DateTimeToXml(), exch.ServiceNumber);
                     exch.Success = false;
                  }
                  else
                  {
                     exch.ResponseText = string.Format(ZAGZIntegration.ImportZagz2018.responseSuccessTemplate,
                        responseGuid, //0
                        exch.ServiceNumber,
                        exch.SendDate.DateTimeToXml(),
                        exch.RequestGuid,
                        DateTime.Now.DateTimeToXml(), //4

                        row[9].TryParseDateDdMmYyyy().DateTimeToXml(), //5
                        exch.Child?.PlaceOfBirth,
                        row[5],
                        row[4],
                        row[6],
                        string.Empty, //10

                        string.Empty, //11
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty, //15

                        string.Empty, //16
                        string.Empty,
                        string.Empty, //row[23].NullEmpty(), запсить акта о рождении
                        string.Empty, //row[24].TryParseDateDdMmYyyy().DateTimeToXml(), дата выдачи свидейтельства о рождении
                        string.Empty, //20 наименование ЗАГС выдовшего свидейтельства

                        row[7].NullEmpty(), //21
                        row[8].NullEmpty(),
                        string.Empty //23
                        );

                     //exch.Success = (exch.Child?.DocumentTypeId != (long) DocumentTypeEnum.CertOfBirth && exch.Child?.DocumentTypeId != 23) ||
                     //               (exch.Child.DocumentNumber == row[22] && exch.Child.DocumentSeria == row[21]);
                     exch.Success = true;
                  }
                  uv.AddEntity(exch, false);

                  uv.SaveChanges();

                  foreach (var entry in uv.Context.ChangeTracker.Entries())
                  {
                     entry.State = EntityState.Detached;
                  }
               }
               SetStatus(new ProgressStatus() { IsError = false, Steps = steps, Step = steps, Message = $"Завершено." });
            }
         }
         catch
         {
            if (_file != null)
            {
               _file = null;
            }
            SetStatus(new ProgressStatus() { IsError = true, Message = "Во время загрузки произошел нераспознанный сбой"});
         }
      }

      private static void SetStatus(ProgressStatus status)
      {
         lock (_locker)
         {
            if (status != null && _status != null)
            {
               if (status.IsError)
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

   public class DTSZNController : BaseController
    {
      #region ExportExcel

      private const string query = @"
select NULL as eid, t.ChildId as cid, t.aid, t.ChildLastName as cln, t.ChildFirstName as cfn, t.ChildMiddleName as cmn, t.csnils, t.cds, t.cdn, t.cdb, t.bn, t.bd, t.br, t.aln, t.afn, t.amn, t.asnils, t.ads, t.adn, isnull(trp2.[Name], isnull(trp1.[Name], trp0.[Name])), t.RequestNumber
From (
Select
r.Id as RequestId, r.TypeOfRestId, r.RequestNumber,
c.Id as ChildId, c.LastName as ChildLastName, c.FirstName as ChildFirstName, c.MiddleName as ChildMiddleName, c.Snils as csnils, c.DocumentSeria as cds, c.DocumentNumber as cdn, c.DateOfBirth as cdb,
a.Id as aid, a.LastName as aln, a.FirstName as afn, a.MiddleName as amn, a.DateOfBirth as adb, a.DocumentSeria as ads, a.DocumentNumber as adn, a.Snils as asnils,
b.[Name] as bn, bd.[Name] as bd, br.[Name] as br
From dbo.Child c
Inner Join dbo.Request r On r.Id = c.RequestId And r.StatusId=1050 and r.IsDeleted=0 and r.TypeOfRestId in (6,12) and r.YearOfRestId=@yor
Inner Join dbo.Applicant a On a.Id = r.ApplicantId And a.RequestId is NULL
left join dbo.BenefitType b on b.Id=c.BenefitTypeId	
left join dbo.[Address] adr on adr.Id = c.AddressId	
left join dbo.BtiAddress ba on ba.Id = adr.BtiAddressId
left join dbo.BtiDistrict bd on bd.Id= isnull(adr.BtiDistrictId,ba.BtiDistrictId)
left join dbo.BtiRegion br on br.Id= isnull(adr.BtiRegionId,ba.BtiRegionId)
Union all 
Select
r.Id as RequestId, r.TypeOfRestId, r.RequestNumber,
c.Id as ChildId, c.LastName as ChildLastName, c.FirstName as ChildFirstName, c.MiddleName as ChildMiddleName, c.Snils as csnils, c.DocumentSeria as cds, c.DocumentNumber as cdn, c.DateOfBirth as cdb,
a.Id as aid, a.LastName as aln, a.FirstName as afn, a.MiddleName as amn, a.DateOfBirth as adb, a.DocumentSeria as ads, a.DocumentNumber as adn, a.Snils as asnils,
b.[Name] as bn, bd.[Name] as bd, br.[Name] as br
From dbo.Child c
Inner Join dbo.Request r On r.Id = c.RequestId And r.StatusId=1050 and r.IsDeleted=0 and r.TypeOfRestId in (6,12) and r.YearOfRestId=@yor
left Join dbo.Applicant a On a.RequestId = r.Id
left join dbo.BenefitType b on b.Id=c.BenefitTypeId	
left join dbo.[Address] adr on adr.Id = c.AddressId	
left join dbo.BtiAddress ba on ba.Id = adr.BtiAddressId
left join dbo.BtiDistrict bd on bd.Id= isnull(adr.BtiDistrictId,ba.BtiDistrictId)
left join dbo.BtiRegion br on br.Id= isnull(adr.BtiRegionId,ba.BtiRegionId)
Where a.Id is not NULL
) t
left join dbo.TypeOfRest trp0 on trp0.Id = t.TypeOfRestId	
left join dbo.TypeOfRest trp1 on trp1.Id = trp0.ParentId	
left join dbo.TypeOfRest trp2 on trp2.Id = trp1.ParentId	
order by t.RequestNumber, cid, aid";

      public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
      {
         base.SetUnitOfWorkInRefClass(unitOfWork);
      }

      public ActionResult ExportToExcel(long YearOfRestId = 0)
      {
         SetUnitOfWorkInRefClass(UnitOfWork);

         //if (!Security.HasRight(AccessRightEnum.ZAGSIntegration))
         //{
         //   return RedirectToAvalibleAction();
         //}


         var dt = new DataTable();
         var conn = UnitOfWork.Database.Connection;
         var connectionState = conn.State;

         try
         {
            if (connectionState != ConnectionState.Open) conn.Open();
            using (var cmd = conn.CreateCommand())
            {
               cmd.CommandText = query;
               cmd.CommandType = CommandType.Text;
               cmd.Parameters.Add(new SqlParameter("yor", YearOfRestId));
               //cmd.Parameters.Add(new SqlParameter("yor", 10020));
               //cmd.Parameters.Add(new SqlParameter("ip", false));
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
               var sheet = pkg.Workbook.Worksheets.Add("Cироты");

               const int startRow = 2;
               const int childColumnStart = 4;
               const int applicantColumnStart = 14;
               const int applicantColumndEnd = 19;

               FormExcelTableHeader(sheet.Cells[1, 1, 2, 1], "EID");
               FormExcelTableHeader(sheet.Cells[1, 2, 2, 2], "ID");
               FormExcelTableHeader(sheet.Cells[1, 3, 2, 3], "IDA");

               FormExcelTableHeader(sheet.Cells[1, childColumnStart, 1, applicantColumnStart - 1], "Ребёнок");
               FormExcelTableHeader(sheet.Cells[1, applicantColumnStart, 1, applicantColumndEnd], "Попечитель");

               FormExcelTableHeader(sheet.Cells[1, 20, 2, 20], "Цель обращения");
               FormExcelTableHeader(sheet.Cells[1, 21, 2, 21], "Номер заявления");
               FormExcelTableHeader(sheet.Cells[1, 22, 2, 22], "Результат проверки");

               //Ребёнок
               SetCellHeader(sheet.Cells[2, 4], "Фамилия");
               SetCellHeader(sheet.Cells[2, 5], "Имя");
               SetCellHeader(sheet.Cells[2, 6], "Отчество");
               SetCellHeader(sheet.Cells[2, 7], "СНИЛС");
               SetCellHeader(sheet.Cells[2, 8], "Серия");
               SetCellHeader(sheet.Cells[2, 9], "Номер");
               SetCellHeader(sheet.Cells[2, 10], "Дата рождения");
               SetCellHeader(sheet.Cells[2, 11], "Льгота");
               SetCellHeader(sheet.Cells[2, 12], "Округ");
               SetCellHeader(sheet.Cells[2, 13], "Район");
               //Попечитель
               SetCellHeader(sheet.Cells[2, 14], "Фамилия");
               SetCellHeader(sheet.Cells[2, 15], "Имя");
               SetCellHeader(sheet.Cells[2, 16], "Отчество");
               SetCellHeader(sheet.Cells[2, 17], "СНИЛС");
               SetCellHeader(sheet.Cells[2, 18], "Серия");
               SetCellHeader(sheet.Cells[2, 19], "Номер");

               for(int i = 0; i < dt.Rows.Count; i++)
               {
                  var _col = i + 3;
                  SetCellFormat(sheet.Cells[_col, 1]);
                  sheet.Cells[_col, 1].Value = string.Empty;

                  SetCellFormat(sheet.Cells[_col, 2]);
                  sheet.Cells[_col, 2].Value = (long)dt.Rows[i][1];

                  SetCellFormat(sheet.Cells[_col, 3]);
                  sheet.Cells[_col, 3].Value = (long)dt.Rows[i][2];

                  //Ребёнок
                  SetCellFormat(sheet.Cells[_col, 4]);
                  sheet.Cells[_col, 4].Value = (string)dt.Rows[i][3];

                  SetCellFormat(sheet.Cells[_col, 5]);
                  sheet.Cells[_col, 5].Value = (string)dt.Rows[i][4];

                  SetCellFormat(sheet.Cells[_col, 6]);
                  sheet.Cells[_col, 6].Value = dt.Rows[i][5] != DBNull.Value ? (string)dt.Rows[i][5] : string.Empty;

                  SetCellFormat(sheet.Cells[_col, 7]);
                  sheet.Cells[_col, 7].Value = (string)dt.Rows[i][6];

                  SetCellFormat(sheet.Cells[_col, 8]);
                  sheet.Cells[_col, 8].Value = (string)dt.Rows[i][7];

                  SetCellFormat(sheet.Cells[_col, 9]);
                  sheet.Cells[_col, 9].Value = (string)dt.Rows[i][8];

                  SetCellFormat(sheet.Cells[_col, 10]);
                  sheet.Cells[_col, 10].Value = (DateTime)dt.Rows[i][9];
                  sheet.Cells[_col, 10].Style.Numberformat.Format = "dd.mm.yyyy";

                  SetCellFormat(sheet.Cells[_col, 11]);
                  sheet.Cells[_col, 11].Value = (string)dt.Rows[i][10];

                  SetCellFormat(sheet.Cells[_col, 12]);
                  sheet.Cells[_col, 12].Value = (string)dt.Rows[i][11];

                  SetCellFormat(sheet.Cells[_col, 13]);
                  sheet.Cells[_col, 13].Value = (string)dt.Rows[i][12];

                  //Попечитель
                  SetCellFormat(sheet.Cells[_col, 14]);
                  sheet.Cells[_col, 14].Value = (string)dt.Rows[i][13];

                  SetCellFormat(sheet.Cells[_col, 15]);
                  sheet.Cells[_col, 15].Value = (string)dt.Rows[i][14];

                  SetCellFormat(sheet.Cells[_col, 16]);
                  sheet.Cells[_col, 16].Value = dt.Rows[i][15] != DBNull.Value ? (string)dt.Rows[i][15] : string.Empty;

                  SetCellFormat(sheet.Cells[_col, 17]);
                  sheet.Cells[_col, 17].Value = (string)dt.Rows[i][16];

                  SetCellFormat(sheet.Cells[_col, 18]);
                  sheet.Cells[_col, 18].Value = (string)dt.Rows[i][17];

                  SetCellFormat(sheet.Cells[_col, 19]);
                  sheet.Cells[_col, 19].Value = (string)dt.Rows[i][18];


                  SetCellFormat(sheet.Cells[_col, 20]);
                  sheet.Cells[_col, 20].Value = (string)dt.Rows[i][19];

                  SetCellFormat(sheet.Cells[_col, 21]);
                  sheet.Cells[_col, 21].Value = (string)dt.Rows[i][20];

                  SetCellFormat(sheet.Cells[_col, 22]);
                  sheet.Cells[_col, 22].Value = string.Empty;
               }


               sheet.Column(1).Width = ModifyWidth(8.43);
               sheet.Column(2).Width = ModifyWidth(6.29);
               sheet.Column(3).Width = ModifyWidth(6.29);
               sheet.Column(4).Width = ModifyWidth(20.29);
               sheet.Column(5).Width = ModifyWidth(18.29);
               sheet.Column(6).Width = ModifyWidth(18);
               sheet.Column(7).Width = ModifyWidth(18);
               sheet.Column(8).Width = ModifyWidth(9.14);
               sheet.Column(9).Width = ModifyWidth(11.29);
               sheet.Column(10).Width = ModifyWidth(14.71);
               sheet.Column(11).Width = ModifyWidth(28.71);
               sheet.Column(12).Width = ModifyWidth(42.14);
               sheet.Column(13).Width = ModifyWidth(49.29);
               sheet.Column(14).Width = ModifyWidth(30);
               sheet.Column(15).Width = ModifyWidth(20.29);
               sheet.Column(16).Width = ModifyWidth(18.29);
               sheet.Column(17).Width = ModifyWidth(18.29);
               sheet.Column(18).Width = ModifyWidth(18.29);
               sheet.Column(19).Width = ModifyWidth(18.29);
               sheet.Column(20).Width = ModifyWidth(18.29);
               sheet.Column(21).Width = ModifyWidth(30);
               sheet.Column(22).Width = ModifyWidth(27.86);

               sheet.Cells[startRow, 1, dt.Rows.Count + startRow, 22].AutoFilter = true;

               pkg.Save();
            }

            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ДТСЗН сироты.xlsx");
         }
      }

      private static double ModifyWidth(double Width)
      {
         return Width * 1.12930;
      }

      private static void FormExcelTableHeader(ExcelRange range, string value)
      {
         range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
         range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
         range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
         range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
         range.Style.WrapText = false;
         range.Value = value;
         range.Merge = true;
         range.Style.Font.Bold = true;
         range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
      }

      private static void SetCellHeader(ExcelRange cell, string Name)
      {
         cell.Style.Font.SetFromFont(new System.Drawing.Font("Calibri", 11));
         cell.Style.WrapText = true;
         cell.Style.Font.Bold = true;
         cell.Style.Font.Size = 11;
         cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
         cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
         cell.Value = Name;
         cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
         cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
         cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
         cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;

      }

      private static void SetCellFormat(ExcelRange cell, bool setYellowBackGround = false)
      {
         cell.Style.Font.SetFromFont(new System.Drawing.Font("Calibri", 11));
         cell.Style.WrapText = false;
         cell.Style.Font.Bold = false;
         cell.Style.Font.Size = 11;
         cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
         cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
         if (setYellowBackGround)
         {
            cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(255, 255, 0));
         }
      }

      #endregion

      public ActionResult Index(string Msg = "")
      {
         SetUnitOfWorkInRefClass(UnitOfWork);

         if (!Security.HasRight(AccessRightEnum.DTSZNIntegration))
         {
            return RedirectToAvalibleAction();
         }

         ViewBag.Yars = UnitOfWork.GetSet<YearOfRest>().Where(ss => !ss.IsClosed).ToDictionary(x => x.Id, y => y.Name);

         return View((object)Msg);
      }

      public ActionResult StartProcess()
      {
         if (!Security.HasRight(AccessRightEnum.DTSZNIntegration))
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

         var result = DTSZNIntegrationQueue.Execute(data);

         if (string.IsNullOrWhiteSpace(result))
         {
            return RedirectToAction("Index");
         }

         return View("Index", (object)result);
      }

      public ActionResult GetStatus(Guid Index)
      {
         if (!Security.HasRight(AccessRightEnum.DTSZNIntegration))
         {
            return RedirectToAvalibleAction();
         }

         return Json(DTSZNIntegrationQueue.GetStatus(), JsonRequestBehavior.AllowGet);
      }
   }
}
