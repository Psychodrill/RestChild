using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RestChild.Comon;
using RestChild.Comon.ToExcel;

namespace RestChild.Web.Models.Business.Export
{
	public class PreRequestExcelExport
	{
		public static string GenerateFile(IEnumerable<PreRequestViewModel> data)
		{
			var columns = new List<ExcelColumn<PreRequestViewModel>>
			{
				//запросы
				new ExcelColumn<PreRequestViewModel> {Title = "Номер заявления", Func = r => r.ReqNum.FormatEx(false), Width = 31},
				new ExcelColumn<PreRequestViewModel> {Title = "Номер заявления с МПГУ", Func = r => r.MpguNumber.FormatEx(false), Width = 24},
				new ExcelColumn<PreRequestViewModel> {Title = "Номер путевки", Func = r => r.CertificateNumber.FormatEx(false), Width = 15},
				new ExcelColumn<PreRequestViewModel> {Title = "Дата заявления", Func = r => r.RequestDate.FormatEx(), Width = 15},
				new ExcelColumn<PreRequestViewModel> {Title = "Статус заявления", Func = r => r.RequestState.FormatEx(false), Width = 30},
				new ExcelColumn<PreRequestViewModel> {Title = "Причина отказа", Func = r => r.DeclineReason.FormatEx(false), Width = 40},

				//Списки
				new ExcelColumn<PreRequestViewModel> {Title = "ОИВ",Func = r => r.VedomstvoShortName.FormatEx(false), Width = 40},
				new ExcelColumn<PreRequestViewModel> {Title = "Учреждение",Func = r => r.OrganizationName.FormatEx(false), Width = 40},
				new ExcelColumn<PreRequestViewModel> {Title = "Список", Func = r => r.ListOfChildrenName.FormatEx(false), Width = 20},
				new ExcelColumn<PreRequestViewModel> {Title = "Статус оплаты", Func = r => r.PaymentStatus.FormatEx(false), Width = 15},

				//Разное
				new ExcelColumn<PreRequestViewModel> {Title = "Цель обращения", Func = r => r.TypeOfRest.FormatEx(false), Width = 41},
				new ExcelColumn<PreRequestViewModel> {Title = "Место отдыха", Func = r => r.HotelName.FormatEx(false)},
				new ExcelColumn<PreRequestViewModel> {Title = "Адрес оздоровительной организации", Func = r => r.HotelAddress.FormatEx(false)},
				new ExcelColumn<PreRequestViewModel> {Title = "Время отдыха", Func = r => r.TimeOfRest.FormatEx(false)},
				new ExcelColumn<PreRequestViewModel> {Title = "Тематика смены", Func = r => r.RestSubject.FormatEx(false)},
				new ExcelColumn<PreRequestViewModel> {Title = "Категория отдыхающего", Func = r => r.RestCaregory.FormatEx(false)},

				//отдыхающий
				new ExcelColumn<PreRequestViewModel> {Title = "Фамилия", Func = r => r.LastName.FormatEx(false), Width = 20},
				new ExcelColumn<PreRequestViewModel> {Title = "Имя", Func = r => r.FirstName.FormatEx(false), Width = 20},
				new ExcelColumn<PreRequestViewModel> {Title = "Отчество", Func = r => r.MiddleName.FormatEx(false), Width = 20},
				new ExcelColumn<PreRequestViewModel> {Title = "Пол", Func = r => r.Sex.FormatEx(false), Width = 10},
				new ExcelColumn<PreRequestViewModel> {Title = "Дата рождения", Func = r => r.DateOfBirth.FormatEx(false), Width = 15},
				new ExcelColumn<PreRequestViewModel> {Title = "Возраст", Func = r => r.Age.FormatEx(false), Width = 8},
				new ExcelColumn<PreRequestViewModel> {Title = "Место рождения", Func = r => r.PlaceOfBirth.FormatEx(false), Width = 30},
			    new ExcelColumn<PreRequestViewModel> {Title = "СНИЛС", Func = r => r.Snils.FormatEx(false), Width = 15},
				new ExcelColumn<PreRequestViewModel> {Title = "Документ", Func = r => r.DocumentType.FormatEx(false), Width = 30},
				new ExcelColumn<PreRequestViewModel> {Title = "Серия", Func = r => r.DocumentSeria.FormatEx(false), Width = 10},
				new ExcelColumn<PreRequestViewModel> {Title = "Номер", Func = r => r.DocumentNumber.FormatEx(false), Width = 15},
				new ExcelColumn<PreRequestViewModel> {Title = "Дата выдачи", Func = r => r.DocumentDate.FormatEx(false), Width = 10},
				new ExcelColumn<PreRequestViewModel> {Title = "Кем выдан", Func = r => r.DocumentIssure.FormatEx(false), Width = 30},
				new ExcelColumn<PreRequestViewModel> {Title = "Вид ограничения", Func = r => r.RestrictionType.FormatEx(false), Width = 50},
			    new ExcelColumn<PreRequestViewModel> {Title = "Подвид ограничения", Func = r => r.RestrictionSubType.FormatEx(false), Width = 50},
				new ExcelColumn<PreRequestViewModel> {Title = "Вид льготы", Func = r => r.BenefitType.FormatEx(false), Width = 50},
				new ExcelColumn<PreRequestViewModel> {Title = "Адрес регистрации", Func = r => r.AddressRegistration, Width = 50},
                new ExcelColumn<PreRequestViewModel> {Title = "Доверенность", Func = r => r.Proxy, Width = 50},
				new ExcelColumn<PreRequestViewModel> {Title = "Отказ от билета (в место отдыха)", Func = r => r.ToNotNeedTicketReason, Width = 50},
                new ExcelColumn<PreRequestViewModel> {Title = "Льготная категория по БР", Func = r => r.BaseRegistryRestrictionTypeInfo, Width = 50},
                new ExcelColumn<PreRequestViewModel> {Title = "Отказ от билета (из места отдыха)", Func = r => r.FromNotNeedTicketReason, Width = 50},

				//заявитель
				new ExcelColumn<PreRequestViewModel> {Title = "Фамилия", Func = r => r.ApplicantLastName.FormatEx(false)},
				new ExcelColumn<PreRequestViewModel> {Title = "Имя", Func = r => r.ApplicantFirstName.FormatEx(false)},
				new ExcelColumn<PreRequestViewModel> {Title = "Отчество", Func = r => r.ApplicantMiddleName.FormatEx(false)},
				new ExcelColumn<PreRequestViewModel> {Title = "Документ", Func = r => r.ApplicantDocType.FormatEx(false), Width = 30},
				new ExcelColumn<PreRequestViewModel> {Title = "Серия", Func = r => r.ApplicantDocSeries.FormatEx(false), Width = 10},
				new ExcelColumn<PreRequestViewModel> {Title = "Номер", Func = r => r.ApplicantDocNumber.FormatEx(false), Width = 15},
				new ExcelColumn<PreRequestViewModel> {Title = "Телефон", Func = r => r.ApplicantPhone.FormatEx(false), Width = 17},
				new ExcelColumn<PreRequestViewModel> {Title = "E-mail", Func = r => r.ApplicantEmail.FormatEx(false)},

				// мать
				new ExcelColumn<PreRequestViewModel> {Title = "ФИО", Func = r => r.MotherFio.FormatEx(false)},
				new ExcelColumn<PreRequestViewModel> {Title = "Дата рождения", Func = r => r.MotherBirthDate.FormatEx(false), Width = 15},

				// отец
				new ExcelColumn<PreRequestViewModel> {Title = "ФИО", Func = r => r.FatherFio.FormatEx(false)},
				new ExcelColumn<PreRequestViewModel> {Title = "Дата рождения", Func = r => r.FatherBirthDate.FormatEx(false), Width = 15},
			};

			columns = columns.Select(
				c =>
				{
					c.WordWrap = true;
					c.VerticalAlignment = ExcelVerticalAlignment.Center;
					return c;
				}).ToList();

			using (var excel = new ExcelTable<PreRequestViewModel>(columns))
			{
				const int startRow = 2;
				var excelWorksheet = excel.CreateExcelWorksheet("Реестр отдыхающих");

				var restColumnStart = 17;
				var applicantColumnStart = 38;
				var applicantColumndEnd = 49;
				FormExcelTableHeader(excelWorksheet.Cells[2, restColumnStart, 2, applicantColumnStart-1], "Отдыхающий");
				FormExcelTableHeader(excelWorksheet.Cells[2, applicantColumnStart, 2, applicantColumndEnd - 4], "Заявитель / Родитель");
				FormExcelTableHeader(excelWorksheet.Cells[2, applicantColumndEnd - 3, 2, applicantColumndEnd - 2], "Мать");
				FormExcelTableHeader(excelWorksheet.Cells[2, applicantColumndEnd - 1, 2, applicantColumndEnd], "Отец");

				excel.DataBind(excelWorksheet, data, ExcelBorderStyle.Thin, startRow);
				for (var i = 1; i <= restColumnStart - 1; i++)
				{
					var value = excelWorksheet.Cells[2, i].Value;
					excelWorksheet.Cells[1, i, 2, i].Merge = true;
					excelWorksheet.Cells[1, i, 2, i].Value = value;
					excelWorksheet.Cells[1, i, 2, i].Style.Font.Bold = true;
					excelWorksheet.Cells[1, i, 2, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
					excelWorksheet.Cells[1, i, 2, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
					excelWorksheet.Cells[1, i, 2, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
					excelWorksheet.Cells[1, i, 2, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
					excelWorksheet.Cells[1, i, 2, i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
				}

				for (var i = restColumnStart; i <= applicantColumndEnd; i++)
				{
					excelWorksheet.Cells[2, i].Style.Font.Bold = true;
					excelWorksheet.Cells[2, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
					excelWorksheet.Cells[2, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
					excelWorksheet.Cells[2, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
					excelWorksheet.Cells[2, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
					excelWorksheet.Cells[2, i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
				}

				return excel.CreateFileExcel();
			}
		}

		public static void FormExcelTableHeader(ExcelRange range, string value)
		{
			range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
			range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
			range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
			range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
			range.Style.WrapText = true;
			range.Value = value;
			range.Merge = true;
			range.Style.Font.Bold = true;
			range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
		}
	}
}
