using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL;
using RestChild.Domain;
using RestChild.Extensions.Filter;
using RestChild.Security.Logger;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers
{
    public partial class FirstRequestCompanyController
    {
        private void CompensationRegistryExcelHeader(ExcelWorksheet sheet, CompensationRegistryFillResult result)
        {
            CompensationRegistryColumnWidth(sheet);
            using (var cell = sheet.Cells[1, 1, 1, 12])
            {
                cell.Merge = true;
                cell.Style.WrapText = true;
                cell.Style.Font.SetFromFont(new Font("Times New Roman", 12));
                cell.Style.Font.Bold = true;
                cell.Style.Font.Size = 9;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                cell.Value = "Реестр № от \"___\"_______________ 20___ г.";
            }

            using (var cell = sheet.Cells[2, 10])
            {
                cell.Merge = true;
                cell.Style.WrapText = true;
                cell.Style.Font.SetFromFont(new Font("Times New Roman", 12));
                cell.Style.Font.Bold = true;
                cell.Style.Font.Size = 9;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                cell.Value = "К ОПЛАТЕ:";
            }

            using (var cell = sheet.Cells[2, 11])
            {
                cell.Merge = true;
                cell.Style.WrapText = true;
                cell.Style.Font.Bold = true;
                cell.Style.Font.Size = 9;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                cell.Value = result.Summa;
            }

            using (var cell = sheet.Cells[4, 1, 4, 12])
            {
                cell.Merge = true;
                cell.Style.WrapText = true;
                cell.Style.Font.SetFromFont(new Font("Times New Roman", 12));
                cell.Style.Font.Bold = true;
                cell.Style.Font.Size = 10;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                cell.Value =
                    "РЕЕСТР ПОЛУЧАТЕЛЕЙ КОМПЕНСАЦИИ ЗА САМОСТОЯТЕЛЬНО ПРИОБРЕТЕННУЮ ПУТЁВКУ\n НА ОСНОВАНИИ ЗАЯВЛЕНИЙ ПРИНЯТЫХ В ГАУК «МОСГОРТУР»";
            }

            sheet.Row(4).Height = 46;
        }

        private void CompensationRegistryExcelFooter(ExcelWorksheet sheet, int row)
        {
            row = row + 1;
            using (var cell = sheet.Cells[row, 1, row, 7])
            {
                cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                cell.Merge = true;
                cell.Style.WrapText = true;
                cell.Style.Font.SetFromFont(new Font("Times New Roman", 12));
                cell.Style.Font.Bold = true;
                cell.Style.Font.Size = 12;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                cell.Value = "ИТОГО:";
            }

            using (var cell = sheet.Cells[row, 8])
            {
                cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                cell.Merge = true;
                cell.Style.WrapText = true;
                cell.Style.Font.SetFromFont(new Font("Times New Roman", 12));
                cell.Style.Font.Bold = true;
                cell.Style.Font.Size = 12;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            }

            using (var cell = sheet.Cells[row, 9, row, 12])
            {
                cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                cell.Merge = true;
                cell.Style.WrapText = true;
                cell.Style.Font.SetFromFont(new Font("Times New Roman", 12));
                cell.Style.Font.Bold = true;
                cell.Style.Font.Size = 10;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                cell.Value = "";
            }

            row += 2;

            sheet.Row(row + 1).Height = 45;
            sheet.Row(row + 2).Height = 32;
            FooterFill(sheet, row, 1, 2, "Исполнитель:", true);
            FooterFill(sheet, row + 1, 1, 2, "Менеджер отдела обработки документов по предоставлению услуг населению");
            FooterFill(sheet, row + 2, 1, 2, Security.GetCurrentAccount()?.Name, true);
            FooterFill(sheet, row + 3, 1, 2, "«___» ________ 20___г. ____________");

            FooterFill(sheet, row, 4, 1, "Согласовано:", true);
            FooterFill(sheet, row + 1, 4, 1, "Начальник отдела обработки документов по предоставлению услуг населению");
            FooterFill(sheet, row + 2, 4, 1, "Иванов Игорь Игоревич", true);
            FooterFill(sheet, row + 3, 4, 1, "«___» ________ 20___г. ____________");

            FooterFill(sheet, row, 7, 1, "Согласовано:", true);
            FooterFill(sheet, row + 1, 7, 1, "Начальник управления по предоставлению услуг населению");
            FooterFill(sheet, row + 2, 7, 1, "Сердешнова Наталья Олеговна", true);
            FooterFill(sheet, row + 3, 7, 1, "«___» ________ 20___г. ____________");

            FooterFill(sheet, row, 9, 2, "Согласовано:", true);
            FooterFill(sheet, row + 1, 9, 2, "Генеральный директор");
            FooterFill(sheet, row + 2, 9, 2, "Голубева Инна Викторовна", true);
            FooterFill(sheet, row + 3, 9, 2, "«___» ________ 20___г. ____________");
        }

        private string CompensationRegistryExcel(IQueryable<Request> query, CompensationRegistryFillResult result,
            bool haveHeader, bool haveFooter)
        {
            var row = haveHeader ? 5 : 1;

            using (var pkg = new ExcelPackage())
            {
                var sheet = pkg.Workbook.Worksheets.Add("Данные");
                if (haveHeader)
                {
                    CompensationRegistryExcelHeader(sheet, result);
                }

                row = CompensationRegistryExcelFillTable(sheet, query, row, result);

                if (haveFooter)
                {
                    CompensationRegistryExcelFooter(sheet, row);
                }

                var tempFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                pkg.SaveAs(new FileInfo(tempFile));
                return tempFile;
            }
        }

        /// <summary>
        ///     генерация таблицы с детьми
        /// </summary>
        private int CompensationRegistryExcelFillTable(ExcelWorksheet sheet, IQueryable<Request> query, int startRow,
            CompensationRegistryFillResult result)
        {
            result = result ?? new CompensationRegistryFillResult();

            var row = startRow;
            CompensationRegistryColumnWidth(sheet);

            FormatTableHeader(sheet, row, 1, "№ п/п");
            FormatTableHeader(sheet, row, 2, "Номер заявления");
            FormatTableHeader(sheet, row, 3, "Дата заявления");
            FormatTableHeader(sheet, row, 4, "Вид льготы");
            FormatTableHeader(sheet, row, 5, "Ф.И.О.");
            FormatTableHeader(sheet, row, 6, "Дата рождения");
            FormatTableHeader(sheet, row, 7, "СНИЛС");
            FormatTableHeader(sheet, row, 8, "Сумма компенсации");
            FormatTableHeader(sheet, row, 9, "Ф.И.О. Получателя компенсации");
            FormatTableHeader(sheet, row, 10,
                "Реквизиты и счет в кредитной организации (наименование банка, номер лицевого счета получателя)");
            FormatTableHeader(sheet, row, 11, "Номер телефона");
            FormatTableHeader(sheet, row, 12, "Период отдыха");

            sheet.View.FreezePanes(row + 1, 1);
            sheet.Cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells.Style.Fill.BackgroundColor.SetColor(Color.White);

            foreach (var request in query.ToList())
            {
                foreach (var informationVouchers in request.InformationVouchers)
                {
                    foreach (var price in informationVouchers.AttendantsPrice.Where(a => a.Applicant != null)
                        .OrderBy(a => a.Applicant.LastName).ThenBy(a => a.Applicant.FirstName).ToList())
                    {
                        row++;
                        result.Number = result.Number + 1;
                        FormatTableRow(sheet, row, 1, result.Number.ToString());
                        FormatTableRow(sheet, row, 2, request.RequestNumber);
                        FormatTableRow(sheet, row, 3, request.DateRequest.FormatEx());
                        FormatTableRow(sheet, row, 4, "Сопровождающий");
                        FormatTableRow(sheet, row, 5,
                            $"{price.Applicant.LastName} {price.Applicant.FirstName} {price.Applicant.MiddleName}");
                        FormatTableRow(sheet, row, 6, price.Applicant.DateOfBirth.FormatEx(),
                            ExcelHorizontalAlignment.Center);
                        FormatTableRow(sheet, row, 7, price.Applicant.Snils.FormatEx(),
                            ExcelHorizontalAlignment.Center);
                        FormatTableRow(sheet, row, 8, price.AmountOfCompensation, ExcelHorizontalAlignment.Right);
                        FormatTableRow(sheet, row, 9,
                            $"{request.Applicant?.LastName} {request.Applicant?.FirstName} {request.Applicant?.MiddleName}");
                        FormatTableRow(sheet, row, 10, $"{request.BankName}, л/с {request.BankAccount}");
                        FormatTableRow(sheet, row, 11, request.Applicant?.Phone);
                        FormatTableRow(sheet, row, 12,
                            $"{informationVouchers?.DateFrom.FormatEx()}-{informationVouchers?.DateTo.FormatEx()}");
                    }
                }

                foreach (var child in request.Child.ToList())
                {
                    row++;
                    result.Number = result.Number + 1;
                    FormatTableRow(sheet, row, 1, result.Number.ToString());
                    FormatTableRow(sheet, row, 2, request.RequestNumber);
                    FormatTableRow(sheet, row, 3, request.DateRequest.FormatEx());
                    FormatTableRow(sheet, row, 4, child.BenefitType?.Name);
                    FormatTableRow(sheet, row, 5, $"{child.LastName} {child.FirstName} {child.MiddleName}");
                    FormatTableRow(sheet, row, 6, child.DateOfBirth.FormatEx(), ExcelHorizontalAlignment.Center);
                    FormatTableRow(sheet, row, 7, child.Snils.FormatEx(), ExcelHorizontalAlignment.Center);
                    FormatTableRow(sheet, row, 8, child.AmountOfCompensation, ExcelHorizontalAlignment.Right);
                    FormatTableRow(sheet, row, 9,
                        $"{request.Applicant?.LastName} {request.Applicant?.FirstName} {request.Applicant?.MiddleName}");
                    FormatTableRow(sheet, row, 10, $"{request.BankName}, л/с {request.BankAccount}");
                    FormatTableRow(sheet, row, 11, request.Applicant?.Phone);
                    FormatTableRow(sheet, row, 12,
                        $"{child.RequestInformationVoucher?.DateFrom.FormatEx()}-{child.RequestInformationVoucher?.DateTo.FormatEx()}");
                }
            }

            return row;
        }

        private static void CompensationRegistryColumnWidth(ExcelWorksheet sheet)
        {
            sheet.Column(1).Width = 3.57;
            sheet.Column(2).Width = 15.57;
            sheet.Column(3).Width = 16.14;
            sheet.Column(4).Width = 16.14;
            sheet.Column(5).Width = 32.14;
            sheet.Column(6).Width = 9.57;
            sheet.Column(7).Width = 11.43;
            sheet.Column(8).Width = 11.43;
            sheet.Column(9).Width = 32.71;
            sheet.Column(10).Width = 31.43;
            sheet.Column(11).Width = 12.57;
            sheet.Column(12).Width = 18.57;
        }

        private static void FormatTableHeader(ExcelWorksheet sheet, int r, int c, string value)
        {
            using (var cell = sheet.Cells[r, c])
            {
                cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                cell.Style.WrapText = true;
                cell.Style.Font.SetFromFont(new Font("Times New Roman", 12));
                cell.Style.Font.Bold = true;
                cell.Style.Font.Size = 10;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                cell.Value = value;
            }
        }

        private static void FooterFill(ExcelWorksheet sheet, int r, int c, int dc, object value, bool bold = false)
        {
            using (var cell = sheet.Cells[r, c, r, c + dc])
            {
                if (dc > 0)
                {
                    cell.Merge = true;
                }

                cell.Style.WrapText = true;
                cell.Style.Font.SetFromFont(new Font("Times New Roman", 12));
                cell.Style.Font.Bold = bold;
                cell.Style.Font.Size = 12;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                cell.Value = value;
            }
        }

        private static void FormatTableRow(ExcelWorksheet sheet, int r, int c, object value,
            ExcelHorizontalAlignment alignment = ExcelHorizontalAlignment.Left)
        {
            using (var cell = sheet.Cells[r, c])
            {
                cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                cell.Style.Font.SetFromFont(new Font("Times New Roman", 12));
                cell.Style.WrapText = true;
                cell.Style.Font.Size = 10;
                cell.Style.HorizontalAlignment = alignment;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                cell.Value = value;
                if (value is decimal || value is float)
                {
                    cell.Style.Numberformat.Format = "#,##0.00";
                }
            }
        }


        /// <summary>
        ///     выгрузка по компенсации
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public ActionResult CompensationRegistryToExcel(RequestFilterModel filter)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            filter = filter ?? new RequestFilterModel();
            filter.TypeOfRestId = (long) TypeOfRestEnum.CompensationGroup;

            SecurityLogger.AddToLogProcess(UnitOfWork, "Реестр заявлений -> Получатели компенсации",
                Security.GetCurrentAccountId().Value, HttpContext.Request.UserAgent);

            PrepareVocabulary(null, filter);
            var query = ApiController.RequestListQuery(filter);
            var files = new List<string>();
            var count = query.Count();

            var data = new CompensationRegistryFillResult
            {
                Summa = (query.SelectMany(c => c.Child).Select(c => c.AmountOfCompensation).Sum() ?? 0) +
                        (query.SelectMany(c => c.InformationVouchers).SelectMany(v => v.AttendantsPrice)
                            .Where(a => a.Applicant != null).Select(c => c.AmountOfCompensation).Sum() ?? 0)
            };

            if (count <= 1000)
            {
                var file = CompensationRegistryExcel(query, data, true, true);
                if (!string.IsNullOrEmpty(file))
                {
                    return FileAndDeleteOnClose(file,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Реестр получателей.xlsx");
                }

                return null;
            }

            var index = 0;
            while (index < count)
            {
                using (var uw = new UnitOfWork())
                {
                    SetUnitOfWorkInRefClass(uw);
                    var squery = ApiController.RequestListQuery(filter);
                    var fn = CompensationRegistryExcel(squery.OrderBy(r => r.Id).Skip(index).Take(5000), data,
                        index == 0,
                        index + 5000 >= count);
                    if (!string.IsNullOrWhiteSpace(fn))
                    {
                        files.Add(fn);
                    }
                }

                index = index + 5000;
            }

            var tempFile = UnionFilesToZip(files, "Реестр получателей");

            return FileAndDeleteOnClose(tempFile, "application/zip", "Реестр получателей.zip");
        }

        private void PaymentRegistryExcelHeader(ExcelWorksheet sheet, CompensationRegistryFillResult result)
        {
            CompensationRegistryColumnWidth(sheet);
            using (var cell = sheet.Cells[1, 1, 1, 11])
            {
                cell.Merge = true;
                cell.Style.WrapText = true;
                cell.Style.Font.SetFromFont(new Font("Times New Roman", 12));
                cell.Style.Font.Bold = true;
                cell.Style.Font.Size = 9;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                cell.Value = "Реестр № от \"___\"_______________ 20___ г.";
            }

            using (var cell = sheet.Cells[2, 6])
            {
                cell.Merge = true;
                cell.Style.WrapText = true;
                cell.Style.Font.SetFromFont(new Font("Times New Roman", 12));
                cell.Style.Font.Bold = true;
                cell.Style.Font.Size = 9;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                cell.Value = "К ОПЛАТЕ:";
            }

            using (var cell = sheet.Cells[2, 7])
            {
                cell.Merge = true;
                cell.Style.WrapText = true;
                cell.Style.Font.Bold = true;
                cell.Style.Font.Size = 9;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                cell.Value = result.Summa;
                cell.Style.Numberformat.Format = "#,##0.00";
            }

            using (var cell = sheet.Cells[4, 1, 4, 7])
            {
                cell.Merge = true;
                cell.Style.WrapText = true;
                cell.Style.Font.SetFromFont(new Font("Times New Roman", 12));
                cell.Style.Font.Bold = true;
                cell.Style.Font.Size = 10;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                cell.Value =
                    "РЕЕСТР ПОЛУЧАТЕЛЕЙ, КОТОРЫМ ПРЕДОСТАВЛЕНЫ СЕРТИФИКАТЫ НА САМОСТОЯТЕЛЬНУЮ ОРГАНИЗАЦИЮ ОТДЫХА И ОЗДОРОВЛЕНИЯ ДЕТЕЙ, ПРАВО НА КОТОРУЮ УДОСТОВЕРЯЕТСЯ СЕРТИФИКАТОМ";
            }

            sheet.Row(4).Height = 46;
        }

        private static void PaymentRegistryColumnWidth(ExcelWorksheet sheet)
        {
            sheet.Column(1).Width = 5;
            sheet.Column(2).Width = 30.71;
            sheet.Column(3).Width = 16.43;
            sheet.Column(4).Width = 19.14;
            sheet.Column(5).Width = 38.71;
            sheet.Column(6).Width = 38.86;
            sheet.Column(7).Width = 20.71;
        }

        /// <summary>
        ///     генерация таблицы с детьми
        /// </summary>
        private int PaymentRegistryExcelFillTable(ExcelWorksheet sheet, IQueryable<Request> query, int startRow,
            CompensationRegistryFillResult result)
        {
            result = result ?? new CompensationRegistryFillResult();

            var row = startRow;
            PaymentRegistryColumnWidth(sheet);

            FormatTableHeader(sheet, row, 1, "№ п/п");
            FormatTableHeader(sheet, row, 2, "Ф И О ребёнка");
            FormatTableHeader(sheet, row, 3, "Дата рождения");
            FormatTableHeader(sheet, row, 4, "СНИЛС");
            FormatTableHeader(sheet, row, 5, "Сумма выплаты");
            FormatTableHeader(sheet, row, 6, "Ф И О Получателя");
            FormatTableHeader(sheet, row, 7, "Реквизиты и счет в кредитной организации");
            FormatTableHeader(sheet, row, 8, "Номер телефона");

            sheet.View.FreezePanes(row + 1, 1);
            sheet.Cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells.Style.Fill.BackgroundColor.SetColor(Color.White);

            var benefitCertificateOnAttendant = Settings.Default.BenefitCertificateOnAttendant.Cast<string>()
                .Select(s => s.LongParse()).Where(s => s.HasValue).ToArray();

            foreach (var request in query.ToList())
            {
                var attendants = 0;
                if (request.IsFirstCompany && (
                    request.TypeOfRestId == (long) TypeOfRestEnum.RestWithParents ||
                    request.TypeOfRestId == (long) TypeOfRestEnum.RestWithParentsPoor ||
                    request.TypeOfRestId == (long) TypeOfRestEnum.RestWithParentsComplex ||
                    request.TypeOfRestId == (long) TypeOfRestEnum.RestWithParentsInvalid ||
                    request.TypeOfRestId == (long) TypeOfRestEnum.RestWithParentsInvalidOrphanComplex))
                {
                    attendants = request.Attendant.Count;
                    if (request.Applicant.IsAccomp)
                    {
                        attendants++;
                    }
                }

                foreach (var child in request.Child.OrderBy(c => c.LastName).ThenBy(c => c.FirstName)
                    .ThenBy(c => c.MiddleName).ThenBy(c => c.Id).ToList())
                {
                    row++;
                    result.Number = result.Number + 1;
                    FormatTableRow(sheet, row, 1, result.Number);
                    FormatTableRow(sheet, row, 2, $"{child.LastName} {child.FirstName} {child.MiddleName}");
                    FormatTableRow(sheet, row, 3, child.DateOfBirth.FormatEx(), ExcelHorizontalAlignment.Center);
                    FormatTableRow(sheet, row, 4, child.Snils.FormatEx(), ExcelHorizontalAlignment.Center);

                    var cntMoney = 1;

                    if (benefitCertificateOnAttendant.Contains(child.BenefitTypeId) && attendants > 0)
                    {
                        attendants--;
                        cntMoney += 1;
                    }

                    FormatTableRow(sheet, row, 5, Settings.Default.CertificateOnMoneyPricePerChild * cntMoney);
                    FormatTableRow(sheet, row, 6,
                        !string.IsNullOrWhiteSpace(request.BankLastName)
                            ? $"{request.BankLastName} {request.BankFirstName} {request.BankMiddleName}"
                            : $"{request.Applicant?.LastName} {request.Applicant?.FirstName} {request.Applicant?.MiddleName}");
                    var bank = request.BankName;

                    if (!string.IsNullOrWhiteSpace(request.BankBik) || !string.IsNullOrWhiteSpace(request.BankInn) ||
                        !string.IsNullOrWhiteSpace(request.BankKpp))
                    {
                        bank += "(";
                        if (!string.IsNullOrWhiteSpace(request.BankBik))
                        {
                            bank += "БИК:" + request.BankBik + "; ";
                        }

                        if (!string.IsNullOrWhiteSpace(request.BankInn))
                        {
                            bank += "ИНН:" + request.BankInn + "; ";
                        }

                        if (!string.IsNullOrWhiteSpace(request.BankInn))
                        {
                            bank += "КПП:" + request.BankKpp + "; ";
                        }

                        bank = bank.Trim() + ")";
                    }

                    if (!string.IsNullOrWhiteSpace(request.BankAccount))
                    {
                        bank += "; р/с:" + request.BankAccount;
                    }

                    if (!string.IsNullOrWhiteSpace(request.BankCorr))
                    {
                        bank += "; к/с:" + request.BankCorr;
                    }

                    if (!string.IsNullOrWhiteSpace(request.BankCardNumber))
                    {
                        bank += "; номер карты:" + request.BankCardNumber;
                    }

                    FormatTableRow(sheet, row, 7, bank);
                    FormatTableRow(sheet, row, 8, request.Applicant?.Phone);
                }
            }

            return row;
        }

        private void PaymentRegistryExcelFooter(ExcelWorksheet sheet, int row, decimal summa)
        {
            row = row + 1;
            using (var cell = sheet.Cells[row, 1, row, 4])
            {
                cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                cell.Merge = true;
                cell.Style.WrapText = true;
                cell.Style.Font.SetFromFont(new Font("Times New Roman", 12));
                cell.Style.Font.Bold = true;
                cell.Style.Font.Size = 12;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                cell.Value = "ИТОГО:";
            }

            using (var cell = sheet.Cells[row, 5])
            {
                cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                cell.Merge = true;
                cell.Style.WrapText = true;
                cell.Style.Font.SetFromFont(new Font("Times New Roman", 12));
                cell.Style.Font.Bold = true;
                cell.Style.Font.Size = 12;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                cell.Value = summa;
                cell.Style.Numberformat.Format = "#,##0.00";
            }

            using (var cell = sheet.Cells[row, 6, row, 8])
            {
                cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                cell.Merge = true;
                cell.Style.WrapText = true;
                cell.Style.Font.SetFromFont(new Font("Times New Roman", 12));
                cell.Style.Font.Bold = true;
                cell.Style.Font.Size = 10;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                cell.Value = $"({summa:### ### ### ##0} рублей 00 коп.)";
            }

            row += 2;

            sheet.Row(row + 1).Height = 45;
            sheet.Row(row + 2).Height = 32;
            FooterFill(sheet, row, 1, 1, "Исполнитель:", true);
            FooterFill(sheet, row + 1, 1, 1, "Менеджер отдела обработки документов по предоставлению услуг населению");
            FooterFill(sheet, row + 2, 1, 1, Security.GetCurrentAccount()?.Name, true);
            FooterFill(sheet, row + 3, 1, 1, "«___» ________ 20___г. ____________");

            FooterFill(sheet, row, 3, 1, "Согласовано:", true);
            FooterFill(sheet, row + 1, 3, 1, "Начальник отдела обработки документов по предоставлению услуг населению");
            FooterFill(sheet, row + 2, 3, 1, "Иванов Игорь Игоревич", true);
            FooterFill(sheet, row + 3, 3, 1, "«___» ________ 20___г. ____________");

            FooterFill(sheet, row, 5, 0, "Согласовано:", true);
            FooterFill(sheet, row + 1, 5, 0, "Начальник управления по предоставлению услуг населению");
            FooterFill(sheet, row + 2, 5, 0, "Сердешнова Наталья Олеговна", true);
            FooterFill(sheet, row + 3, 5, 0, "«___» ________ 20___г. ____________");

            FooterFill(sheet, row, 6, 1, "Согласовано:", true);
            FooterFill(sheet, row + 1, 6, 1, "Генеральный директор");
            FooterFill(sheet, row + 2, 6, 1, "Голубева Инна Викторовна", true);
            FooterFill(sheet, row + 3, 6, 1, "«___» ________ 20___г. ____________");
        }

        private string PaymentRegistryExcel(IQueryable<Request> query, CompensationRegistryFillResult result,
            bool haveHeader, bool haveFooter)
        {
            var row = haveHeader ? 5 : 1;

            using (var pkg = new ExcelPackage())
            {
                var sheet = pkg.Workbook.Worksheets.Add("Данные");
                if (haveHeader)
                {
                    PaymentRegistryExcelHeader(sheet, result);
                }

                row = PaymentRegistryExcelFillTable(sheet, query, row, result);

                if (haveFooter)
                {
                    PaymentRegistryExcelFooter(sheet, row, result.Summa);
                }

                var tempFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                pkg.SaveAs(new FileInfo(tempFile));
                return tempFile;
            }
        }

        /// <summary>
        ///     выгрузка по получивших выплату
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public ActionResult PaymentsRegistryToExcel(RequestFilterModel filter)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            filter = filter ?? new RequestFilterModel();
            filter.TypeOfDecision = 2;

            SecurityLogger.AddToLogProcess(UnitOfWork, "Реестр заявлений -> Сертификаты на самостоятельный отдых",
                Security.GetCurrentAccountId().Value, HttpContext.Request.UserAgent);

            PrepareVocabulary(null, filter);
            var query = ApiController.RequestListQuery(filter);
            var files = new List<string>();
            var count = query.Count();

            var data = new CompensationRegistryFillResult
            {
                Summa = query.SelectMany(c => c.Child).Count() * Settings.Default.CertificateOnMoneyPricePerChild
            };

            if (count <= 1000)
            {
                var file = PaymentRegistryExcel(query, data, true, true);
                if (!string.IsNullOrEmpty(file))
                {
                    return FileAndDeleteOnClose(file,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Реестр получателей.xlsx");
                }

                return null;
            }

            var index = 0;
            while (index < count)
            {
                using (var uw = new UnitOfWork())
                {
                    SetUnitOfWorkInRefClass(uw);
                    var squery = ApiController.RequestListQuery(filter);
                    var fn = PaymentRegistryExcel(squery.OrderBy(r => r.Id).Skip(index).Take(5000), data, index == 0,
                        index + 5000 >= count);
                    if (!string.IsNullOrWhiteSpace(fn))
                    {
                        files.Add(fn);
                    }
                }

                index = index + 5000;
            }

            var tempFile = UnionFilesToZip(files, "Реестр получателей");

            return FileAndDeleteOnClose(tempFile, "application/zip", "Реестр получателей.zip");
        }

        /// <summary>
        ///     класс для подсчета реестра
        /// </summary>
        protected class CompensationRegistryFillResult
        {
            public int Number { get; set; }

            public decimal Summa { get; set; }
        }
    }
}
