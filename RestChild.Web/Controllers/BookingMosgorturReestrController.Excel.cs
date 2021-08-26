using OfficeOpenXml;
using OfficeOpenXml.Style;
using RestChild.Comon.Enumeration;
using RestChild.Extensions.Filter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestChild.Web.Controllers
{
    /// <summary>
    /// Контроллер генерации Excel документов для реестра записи в МГТ
    /// </summary>
    public partial class BookingMosgorturReestrController
    {
        /// <summary>
        /// генерация Excel для списка бронирований/визитов на приём в МГТ
        /// </summary>
        public ActionResult ExportToExcel(BookingMosgorturReestrFilterModel filter)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.MosgorturScheduleBookingView))
            {
                return RedirectToAvalibleAction();
            }
            filter = filter ?? new BookingMosgorturReestrFilterModel();
            var result = ApiController.Get(filter, true);

            using (var ms = new MemoryStream())
            {
                using (var pkg = new ExcelPackage(ms))
                {
                    var sheet = pkg.Workbook.Worksheets.Add("Список");
                    var col = 1;
                    var row = 1;
                    var colsToAutoFilter = new List<int>();

                    SetCellHeader(sheet.Cells[row, col], "Дата посещения");
                    sheet.Column(col).Width = 19;
                    colsToAutoFilter.Add(col);
                    col++;
                    SetCellHeader(sheet.Cells[row, col], "Время посещения");
                    sheet.Column(col).Width = 19;
                    colsToAutoFilter.Add(col);
                    col++;
                    SetCellHeader(sheet.Cells[row, col], "Цель посещения");
                    sheet.Column(col).Width = 49;
                    sheet.Column(col).Style.WrapText = true;
                    colsToAutoFilter.Add(col);
                    col++;
                    SetCellHeader(sheet.Cells[row, col], "Заявление");
                    sheet.Column(col).Width = 19;
                    colsToAutoFilter.Add(col);
                    col++;
                    SetCellHeader(sheet.Cells[row, col], "Статус");
                    sheet.Column(col).Style.WrapText = true;
                    sheet.Column(col).Width = 49;
                    colsToAutoFilter.Add(col);
                    col++;
                    SetCellHeader(sheet.Cells[row, col], "Кол-во слотов");
                    sheet.Column(col).Width = 19;
                    colsToAutoFilter.Add(col);
                    col++;
                    SetCellHeader(sheet.Cells[row, col], "Источник");
                    sheet.Column(col).Width = 19;
                    colsToAutoFilter.Add(col);
                    col++;
                    SetCellHeader(sheet.Cells[row, col], "Дата регистрации");
                    sheet.Column(col).Width = 19;
                    colsToAutoFilter.Add(col);
                    col++;
                    SetCellHeader(sheet.Cells[row, col], "ПИН код");
                    sheet.Column(col).Width = 10;
                    col++;


                    SetCellHeader(sheet.Cells[row, col, row, (col + 5)], "Заявитель");
                    //FIO
                    sheet.Column(col).Width = 25;
                    sheet.Column(col).Style.WrapText = true;
                    col++;
                    //DataBirth
                    sheet.Column(col).Width = 19;
                    col++;
                    //Sex
                    sheet.Column(col).Width = 10;
                    col++;
                    //SNILS
                    sheet.Column(col).Width = 19;
                    col++;
                    //Tel
                    sheet.Column(col).Width = 25;
                    col++;
                    //email
                    sheet.Column(col).Width = 25;
                    col++;




                    for (int i = 0; i < result.Count(); i++)
                    {
                        row++;
                        col = 1;
                        SetCellFormat(sheet.Cells[row, col]);
                        sheet.Cells[row, col].Value = result[i].DateShedule;
                        sheet.Cells[row, col].Style.Numberformat.Format = "dd.mm.yyyy";
                        col++;

                        SetCellFormat(sheet.Cells[row, col]);
                        sheet.Cells[row, col].Value = result[i].DateShedule;
                        sheet.Cells[row, col].Style.Numberformat.Format = "hh:MM";
                        col++;

                        SetCellFormat(sheet.Cells[row, col]);
                        sheet.Cells[row, col].Value = result[i].Target;
                        col++;

                        SetCellFormat(sheet.Cells[row, col]);
                        sheet.Cells[row, col].Value = result[i].BookingNumber;
                        col++;

                        SetCellFormat(sheet.Cells[row, col]);
                        sheet.Cells[row, col].Value = result[i].Status;
                        col++;

                        SetCellFormat(sheet.Cells[row, col]);
                        sheet.Cells[row, col].Value = result[i].SlotsCount;
                        col++;

                        SetCellFormat(sheet.Cells[row, col]);
                        sheet.Cells[row, col].Value = result[i].Source;
                        col++;

                        SetCellFormat(sheet.Cells[row, col]);
                        sheet.Cells[row, col].Value = result[i].RegDate;
                        sheet.Cells[row, col].Style.Numberformat.Format = "dd.mm.yyyy";
                        col++;

                        SetCellFormat(sheet.Cells[row, col]);
                        sheet.Cells[row, col].Value = result[i].PINCode;
                        col++;

                        //Aplicant
                        SetCellFormat(sheet.Cells[row, col]);
                        sheet.Cells[row, col].Value = result[i].AplicantFIO;
                        col++;

                        SetCellFormat(sheet.Cells[row, col]);
                        sheet.Cells[row, col].Value = result[i].AplicantDateBirth;
                        sheet.Cells[row, col].Style.Numberformat.Format = "dd.mm.yyyy";
                        col++;

                        SetCellFormat(sheet.Cells[row, col]);
                        sheet.Cells[row, col].Value = result[i].AplicantMale ? "М" : "Ж";
                        col++;

                        SetCellFormat(sheet.Cells[row, col]);
                        sheet.Cells[row, col].Value = result[i].AplicantSNILS;
                        col++;

                        SetCellFormat(sheet.Cells[row, col]);
                        sheet.Cells[row, col].Value = result[i].AplicantTel;
                        col++;

                        SetCellFormat(sheet.Cells[row, col]);
                        sheet.Cells[row, col].Value = result[i].AplicantEmail;
                        col++;

                    }

                    var firstcol = 0;
                    foreach (var colToAutoFilter in colsToAutoFilter.Distinct().OrderBy(ss => ss))
                    {
                        if (firstcol == 0)
                        {
                            firstcol = colToAutoFilter;
                        }

                        if (colsToAutoFilter.Contains(colToAutoFilter + 1))
                        {
                            continue;
                        }


                        sheet.Cells[1, firstcol, result.Count + 1, colToAutoFilter].AutoFilter = true;
                        firstcol = 0;
                    }

                    pkg.Save();
                }

                return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Реестр записи на приём.xlsx");
            }
        }


        /// <summary>
        /// Создание заголовка колонке(ам) в генерируемом Excel
        /// </summary>
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

            if (cell.Columns > 1)
            {
                cell.Merge = true;
            }
        }

        /// <summary>
        /// Установка формата ячейки
        /// </summary>
        /// <param name="cell"></param>
        private static void SetCellFormat(ExcelRange cell)
        {
            cell.Style.Font.SetFromFont(new System.Drawing.Font("Calibri", 11));
            cell.Style.WrapText = false;
            cell.Style.Font.Bold = false;
            cell.Style.Font.Size = 11;
            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        }
    }
}
